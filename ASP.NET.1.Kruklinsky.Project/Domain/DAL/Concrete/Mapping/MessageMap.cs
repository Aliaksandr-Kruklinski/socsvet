using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public static class MessageMap
    {
        public static ORM.Model.Message ToOrm(this Message item)
        {
            return new ORM.Model.Message
            {
                MessageId = item.Id,
                MessageText = item.Text,
                Time = item.Time,
                User = new ORM.Model.User { UserId = new Guid(item.User.Id) },
                Dialog = new ORM.Model.Dialog {  DialogId = item.Dialog.Id}
            };
        }
        public static Message ToDal(this ORM.Model.Message item)
        {
            return new Message
            {
                Id = item.MessageId,
                Text = item.MessageText,
                Time = item.Time,
                User = new User { Id = item.User.UserId.ToString() },
                Dialog = new Dialog { Id = item.Dialog.DialogId }
            };
        }

        public static Dialog ToDal (this ORM.Model.Dialog item,string userId)
        {
            return new Dialog
            {
                Id = item.DialogId,
                Messages = item.Messages.Select(m => m.ToDal()),
                Users = item.Users.Where(u => u.UserId.ToString() !=  userId).Select(u => new DAL.Interface.Entities.User { Id = u.UserId.ToString() }).ToList()
            };
        }
    }
}
