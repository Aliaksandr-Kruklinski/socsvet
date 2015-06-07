using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Providers.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string MimeType { get; set; }
    }
}