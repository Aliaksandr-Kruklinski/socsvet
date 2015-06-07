using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{

    public class MessagePagingModel
    {
        public IEnumerable<BLL.Interface.Entities.Message> Messages { get; set; }
        public Paginglnfo Paginglnfo { get; set; }
        public int DilogId { get; set; }
    }

    public class WallMessagePagingModel
    {
        public IEnumerable<WallMessage> WallMessages { get; set; }
        public Paginglnfo Paginglnfo { get; set; }
        public string userId { get; set; }
    }

    public class Paginglnfo
    {
        public string ControllerName { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }


    public class WallMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public string UserId { get; set; }
        public IEnumerable<WallComment> Comments { get; set; }
    }

    public class WallComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public string UserId { get; set; }
    }

}