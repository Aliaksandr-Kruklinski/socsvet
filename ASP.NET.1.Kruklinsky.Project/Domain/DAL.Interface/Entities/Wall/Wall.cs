using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class Wall
    {
        public int Id { get; set; }


        public Lazy<User> User { get; set; }
        public Lazy<IEnumerable<WallMessage>> Messages { get; set; }
    }
}
