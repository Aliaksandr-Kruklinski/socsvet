using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.Mapping
{
    public static class WallMap
    {
        public static ORM.Model.Wall ToOrm(this Wall item)
        {
            return new ORM.Model.Wall
            {
                  WallId = item.Id
            };
        }
        public static Wall ToDal (this ORM.Model.Wall item)
        {
            return new Wall
            {
                 Id = item.WallId,
                 Messages = new Lazy<IEnumerable<WallMessage>>(() => item.Messages == null ? new List<WallMessage>(): item.Messages.Select(m => m.ToDal()).ToList()),
                 User = new Lazy<User>(() => item.User == null ? null : item.User.ToDal())
            };
        }
        
        public static ORM.Model.WallMessage ToOrm (this WallMessage item)
        {
            return new ORM.Model.WallMessage
            {
                MessageId = item.Id,
                MessageText = item.Text,
                Time = item.Time,
                UserId = item.UserId
            };
        }
        public static WallMessage ToDal (this ORM.Model.WallMessage item)
        {
            return new WallMessage
            {
                 Id = item.MessageId,
                 Text = item.MessageText,
                 Time = item.Time,
                 Comments = new Lazy<IEnumerable<WallComment>>(() => item.Comments == null ? new List<WallComment>() : item.Comments.Select(c => c.ToDal()).ToList()),
                 UserId = item.UserId
            };
        }

        public static ORM.Model.WallComment ToOrm(this WallComment item, int messageId)
        {
            return new ORM.Model.WallComment
            {
                CommentId = item.Id,
                CommentText = item.Text,
                Time = item.Time,
                Message = new ORM.Model.WallMessage { MessageId = messageId }
            };
        }
        public static WallComment ToDal (this ORM.Model.WallComment item)
        {
            return new WallComment
            {
                 Id = item.CommentId,
                 Text = item.CommentText,
                 Time = item.Time,
                 UserId = item.UserId
            };
        }
    }
}
