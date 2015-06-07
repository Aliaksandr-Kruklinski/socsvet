using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public static class ImageMap
    {
        public static ORM.Model.Image ToOrm(this Image item)
        {
            return new ORM.Model.Image
            {
                ImageId = item.Id,
                ImageData = item.Data,
                ImageMimeType = item.MimeType
            };
        }
    }
}
