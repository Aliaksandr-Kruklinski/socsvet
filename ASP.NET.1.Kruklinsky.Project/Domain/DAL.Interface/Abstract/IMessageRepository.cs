using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IMessageRepository : IRepository<Message>
    {
        void AddMessage(Message message);
        int AddDialog(IEnumerable<string> users);
    }
}
