using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public static class WallMap
    {
        public static WallMessage ToWeb(this BLL.Interface.Entities.WallMessage item)
        {
            return new WallMessage
            {
                Text = item.Text,
                Time = item.Time,
                UserId = item.UserId,
                Comments = item.Comments == null ? new List<WallComment> () : item.Comments.Select(c => c.ToWeb()).ToList()
            };
        }

        public static WallComment ToWeb(this BLL.Interface.Entities.WallComment item)
        {
            return new WallComment
            {
                Text = item.Text,
                Time = item.Time,
                UserId = item.UserId
            };
        }
    }
}