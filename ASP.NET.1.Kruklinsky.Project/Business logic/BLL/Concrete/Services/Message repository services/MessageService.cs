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
    public class MessageService : RepositoryService<IMessageRepository>, IMessageService
    {
        public MessageService(IMessageRepository messageRepository, IDbContextScopeFactory dbContextScopeFactory) : base(messageRepository, dbContextScopeFactory) { }

        public void AddMessage(Message message)
        {
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.AddMessage(message.ToDal());
                context.SaveChanges();
            }
        }

        public int AddDialog(IEnumerable<string> users)
        {
            var result = -1;
            using (var context = dbContextScopeFactory.Create())
            {
                result = this.repository.AddDialog(users);
                context.SaveChanges();
            }
            return result;
        }
    }
}
