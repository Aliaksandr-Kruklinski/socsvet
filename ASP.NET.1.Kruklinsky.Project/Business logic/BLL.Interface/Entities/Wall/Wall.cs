using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class Wall
    {
        public int Id { get; set; }


        public User User { get; set; }
        public IEnumerable<WallMessage> Messages { get; set; }
    }
}
