using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class WallMap
    {
        public static DAL.Interface.Entities.Wall ToDal(this Wall item)
        {
            return new DAL.Interface.Entities.Wall
            {
                Id = item.Id
            };
        }
        public static Wall ToBll(this DAL.Interface.Entities.Wall item)
        {
            return new Wall
            {
                Id = item.Id,
                Messages =  item.Messages.Value == null ? new List<WallMessage>() : item.Messages.Value.Select(m => m.ToBll()).ToList(),
                User =  item.User.Value == null ? null : item.User.Value.ToBll()
            };
        }

        public static DAL.Interface.Entities.WallMessage ToDal(this WallMessage item)
        {
            return new DAL.Interface.Entities.WallMessage
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                UserId = item.UserId
            };
        }
        public static WallMessage ToBll(this DAL.Interface.Entities.WallMessage item)
        {
            return new WallMessage
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                Comments = item.Comments.Value == null ? new List<WallComment>() : item.Comments.Value.Select(c => c.ToBll()).ToList(),
                UserId = item.UserId
            };
        }

        public static DAL.Interface.Entities.WallComment ToDal(this WallComment item)
        {
            return new DAL.Interface.Entities.WallComment
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                UserId = item.UserId
            };
        }
        public static WallComment ToBll(this DAL.Interface.Entities.WallComment item)
        {
            return new WallComment
            {
                Id = item.Id,
                Text = item.Text,
                Time = item.Time,
                UserId = item.UserId
            };
        }
    }
}
