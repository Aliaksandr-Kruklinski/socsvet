using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class MembershipMap
    {
        public static DAL.Interface.Entities.User ToDal (this User item, string password)
        {
            return new DAL.Interface.Entities.User
            {
                 Id = item.Id,
                 Email = item.Email,
                 Password = password,
                 IsApproved = item.IsApproved,
                 CreateDate = item.CreateDate,
            };
        }
        public static DAL.Interface.Entities.User ToDal(this User item)
        {
            return new DAL.Interface.Entities.User
            {
                Id = item.Id,
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate,
            };
        }
        public static User ToBll(this DAL.Interface.Entities.User item)
        {
            return new User
            {
                Id = item.Id,
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate,
                Profile = item.Profile == null ? null : item.Profile.Value.ToBll(),
                Roles = item.Roles == null ? new List<Role>() : item.Roles.Value.Select(r => r.ToBll()).ToList(),
                Images = item.Images == null ? new List<Image> () : item.Images.Value.Select( i => i.ToBll()).ToList(),
                Wall = item.Wall,
                PrivateWall = item.PrivateWall
            };
        }

        public static Role ToBll(this DAL.Interface.Entities.Role item)
        {
            return new Role
            {
                 Id = item.Id,
                 Name = item.Name,
                 Description = item.Description
            };
        }

        public static DAL.Interface.Entities.Profile ToDal(this Profile item, DAL.Interface.Entities.Image image)
        {
            return new DAL.Interface.Entities.Profile
            {
                 Id = item.Id,
                 FirstName = item.FirstName,
                 SecondName = item.SecondName,
                 Birthday = item.Birthday,
                 Avatar = image == null ? null : image
            };
        }
        public static Profile ToBll (this DAL.Interface.Entities.Profile item)
        {
            return new Profile
            {
                Id = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday,
                Avatar = item.Avatar == null ? -1: item.Avatar.ToBll().Id
            };
        }

        public static DAL.Interface.Entities.Image ToDal(this Image item)
        {
            return new DAL.Interface.Entities.Image
            {
                Id = item.Id,
                Data = item.Data,
                MimeType = item.MimeType
            };
        }
        public static Image ToBll(this DAL.Interface.Entities.Image item)
        {
            return new Image
            {
                Id = item.Id,
                Data = item.Data,
                MimeType = item.MimeType
            };
        }
    }
}
