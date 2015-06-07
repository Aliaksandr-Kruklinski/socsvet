using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class WallMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ICollection<WallComment> Comments { get; set; }
        public virtual Wall Wall { get; set; }
    }
}
