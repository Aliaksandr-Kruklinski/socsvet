using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ImageService : RepositoryService<IUserRepository>, IImageService
    {
        public ImageService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }

        public int LoadImage(string id, Interface.Entities.Image image)
        {
            int result = -1;
            UserExceptionsHelper.GetIdExceptions(id);
            if (image == null)
            {
                throw new System.ArgumentNullException("image", "Image is null.");
            }
            using (var context = dbContextScopeFactory.Create())
            {
                result = this.repository.LoadImage(id, image.ToDal());
                context.SaveChanges();
            }
            return result;
        }
    }
}
