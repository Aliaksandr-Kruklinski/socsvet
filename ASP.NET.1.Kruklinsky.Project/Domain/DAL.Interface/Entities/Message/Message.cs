using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime Time { get; set; }

        public User User { get; set; }
        public Dialog Dialog { get; set; }
    }
}
