using AmbientDbContext.Interface;
using DAL.Interface.Abstract;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete.Mapping;
using DAL.Interface.Entities;

namespace DAL.Concrete
{
    public class WallRepository : IWallRepository
    {
        #region IRepository

        private readonly IAmbientDbContextLocator ambientDbContextLocator;
        private DbContext context
        {
            get
            {
                var dbContext = this.ambientDbContextLocator.Get<EFDbContext>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("It is impossible to use repository because DbContextScope has not been created.");
                }
                return dbContext;
            }
        }

        public WallRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null)
            {
                throw new System.ArgumentNullException("ambientDbContextLocator", "Ambient dbContext locator is null.");
            }
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public IEnumerable<Wall> Data
        {
            get
            {
                IEnumerable<ORM.Model.Wall> result = this.context.Set<ORM.Model.Wall>();
                return result.Select(u => u.ToDal()).ToList();
            }
        }

        public void Add(Wall item)
        {
            var result = item.ToOrm();
            this.context.Set<ORM.Model.Wall>().Add(result);
            this.context.SaveChanges();
        }

        public void Delete(Wall item)
        {
            var result = this.GetOrmWall(item.Id);
            if (result != null)
            {
                this.context.Set<ORM.Model.Wall>().Remove(result);
                this.context.SaveChanges();
            }
        }

        public void Update(Wall item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWallRepository

        public IEnumerable<WallMessage> GetAllMessages(int wallId)
        {
            var result = new List<WallMessage>();
            var wall = this.GetOrmWall(wallId);
            if(wall != null && wall.Messages != null)
            {
                var messages = wall.Messages;
                result = messages.Select(m => m.ToDal()).ToList();
            }
            return result;
        }
        public void AddMessage(int wallId, WallMessage message)
        {
            var wall = this.GetOrmWall(wallId);
            if (wall != null)
            {
                if (wall.Messages == null) wall.Messages = new List<ORM.Model.WallMessage>();

                var result = message.ToOrm();
                wall.Messages.Add(message.ToOrm());
                this.context.SaveChanges();
            }
        }
        public void DeleteMessage(int wallId, int messageId)
        {
            var wall = this.GetOrmWall(wallId);
            if (wall != null && wall.Messages != null)
            {
                var result = this.GetOrmMessage(messageId);
                if(result != null)
                {
                    wall.Messages.Remove(result);
                    this.context.SaveChanges();
                }
            }
        }
        public void UpdateMessage(int wallId, int messageId, string text)
        {
            var wall = this.GetOrmWall(wallId);
            if (wall != null && wall.Messages != null)
            {
                var result = this.GetOrmMessage(messageId);
                if (result != null)
                {
                    result.MessageText = text;
                    this.context.SaveChanges();
                }
            }
        }

        public void AddComment(int messageId, WallComment comment)
        {
            var message = this.GetOrmMessage(messageId);
            if (message != null)
            {
                if (message.Comments == null) message.Comments = new List<ORM.Model.WallComment>();
                message.Comments.Add(comment.ToOrm(messageId));
                this.context.SaveChanges();
            }
        }
        public void DeleteComment(int messageId, int commentId)
        {
            var message = this.GetOrmMessage(messageId);
            if (message != null && message.Comments != null)
            {
                var result = this.GetOrmComment(commentId);
                if (result != null)
                {
                    message.Comments.Remove(result);
                    this.context.SaveChanges();
                }
            }
        }
        public void UpdateComment(int messageId, int commentId, string text)
        {
            var message = this.GetOrmMessage(messageId);
            if (message != null && message.Comments != null)
            {
                var result = this.GetOrmComment(commentId);
                if (result != null)
                {
                    result.CommentText = text;
                    this.context.SaveChanges();
                }
            }
        }

        #endregion

        #region Private methods

        private ORM.Model.Wall GetOrmWall(int id)
        {
            ORM.Model.Wall result = null;
            var query = this.context.Set<ORM.Model.Wall>().Where(w => w.WallId == id);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
        private ORM.Model.WallMessage GetOrmMessage(int id)
        {
            ORM.Model.WallMessage result = null;
            var query = this.context.Set<ORM.Model.WallMessage>().Where(m => m.MessageId == id);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
        private ORM.Model.WallComment GetOrmComment(int id)
        {
            ORM.Model.WallComment result = null;
            var query = this.context.Set<ORM.Model.WallComment>().Where(c => c.CommentId == id);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.User GetOrmUser(Guid userId)
        {
            ORM.Model.User result = null;
            var query = this.context.Set<ORM.Model.User>().Where(u => u.UserId == userId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion
    }
}
