using AmbientDbContext.Interface;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class WallService : RepositoryService<IWallRepository>, IWallService
    {
        public WallService(IWallRepository wallRepository, IDbContextScopeFactory dbContextScopeFactory) : base(wallRepository, dbContextScopeFactory) { }

        #region IWallService

        public IEnumerable<WallMessage> GetAllMessages(int wallId)
        {
            var result = new List<WallMessage>();
            using(var context = dbContextScopeFactory.CreateReadOnly())
            {
                result = this.repository.GetAllMessages(wallId).Select(m => m.ToBll()).ToList();
            }
            return result;
        }

        public void AddMessage(int wallId, WallMessage message)
        {
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.AddMessage(wallId, message.ToDal());
                context.SaveChanges();
            }
        }

        public void DeleteMessage(int wallId, int messageId)
        {
            throw new NotImplementedException();
        }
        public void UpdateMessage(int wallId, int messageId, string text)
        {
            throw new NotImplementedException();
        }

        public void AddComment(int messageId, WallComment comment)
        {
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.AddComment(messageId, comment.ToDal());
                context.SaveChanges();
            }
        }

        public void DeleteComment(int messageId, int commentId)
        {
            throw new NotImplementedException();
        }
        public void UpdateComment(int messageId, int commentId, string text)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
