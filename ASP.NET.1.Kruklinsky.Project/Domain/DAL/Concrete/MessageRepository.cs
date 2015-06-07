using AmbientDbContext.Interface;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class MessageRepository : IMessageRepository
    {

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

        public MessageRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null)
            {
                throw new System.ArgumentNullException("ambientDbContextLocator", "Ambient dbContext locator is null.");
            }
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public IEnumerable<Message> Data
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Message item)
        {
            var result = item.ToOrm();
            result.Dialog = this.GetOrmDialog(result.Dialog.DialogId);
            result.User = this.GetOrmUser(result.User.UserId);
            this.context.Set<ORM.Model.Message>().Add(result);
            this.context.SaveChanges();
        }

        public void Delete(Message item)
        {
            throw new NotImplementedException();
        }

        public void Update(Message item)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(Message message)
        {
            this.Add(message);
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

        private ORM.Model.Dialog GetOrmDialog(int id)
        {
            ORM.Model.Dialog result = null;
            var query = this.context.Set<ORM.Model.Dialog>().Where(u => u.DialogId == id);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }


        public int AddDialog(IEnumerable<string> users)
        {
            ORM.Model.Dialog result = null;
            var query = this.context.Set<ORM.Model.Dialog>().Where(
                d => d.Users.Count(u => users.Where(u2 => u2 == u.UserId.ToString()).Count() != 0) == users.Count());
            if (query.Count() != 0)
            {
                result = query.First();
            }
            else
            {
                result = new ORM.Model.Dialog();
                result.Users = new List<ORM.Model.User>();
                foreach (var user in users)
                {
                    var ormUser = this.GetOrmUser(new Guid(user));
                    result.Users.Add(ormUser);
                }
                result = this.context.Set<ORM.Model.Dialog>().Add(result);
                this.context.SaveChanges();
            }
            return result.DialogId;
        }
    }
}
