using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class Wall
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WallId { get; set; }


        public virtual User User { get; set; }
        public virtual ICollection<WallMessage> Messages { get; set; }
    }
}
