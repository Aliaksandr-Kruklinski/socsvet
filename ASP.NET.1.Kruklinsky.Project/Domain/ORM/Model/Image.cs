using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class Image
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public virtual User User { get; set; }
    }
}
