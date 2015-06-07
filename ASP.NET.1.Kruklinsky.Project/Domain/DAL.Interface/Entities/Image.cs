using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string MimeType { get; set; }
    }
}
