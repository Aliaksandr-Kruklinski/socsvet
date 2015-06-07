using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class MessageMap
    {
        public static DAL.Interface.Entities.Message ToDal(this Message item)
        {
            return new DAL.Interface.Entities.Message
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                User = new DAL.Interface.Entities.User { Id = item.User.Id},
                Dialog = new DAL.Interface.Entities.Dialog {  Id = item.Dialog.Id}
            };
        }
        public static Message ToBll(this DAL.Interface.Entities.Message item)
        {
            return new Message
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                User = new User { Id = item.User.Id},
                Dialog = new Dialog {  Id = item.Dialog.Id}
            };
        }

        public static Dialog ToBll(this DAL.Interface.Entities.Dialog item)
        {
            return new Dialog
            {
                 Id = item.Id,
                 Messages = item.Messages.Select(m => m.ToBll()),
                 Users = item.Users.Select(u => u.ToBll())
            };
        }
    }
}
