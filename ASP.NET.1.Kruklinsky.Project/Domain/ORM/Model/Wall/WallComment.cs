using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class WallComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        public string CommentText { get; set; }
        [Required]
        public DateTime Time { get; set; }

       [Required]
        public string UserId { get; set; }
        public virtual WallMessage Message { get; set; }
    }
}
