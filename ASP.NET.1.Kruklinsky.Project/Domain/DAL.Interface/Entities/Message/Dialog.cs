using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class Dialog
    {
        public int Id { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
