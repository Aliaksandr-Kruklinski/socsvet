using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MvcUI.Providers.Entities;

namespace MvcUI.Providers
{
    public static class MembershipMap
    {
        public static BLL.Interface.Entities.User ToBll(this User item)
        {
            return new BLL.Interface.Entities.User
            {
                Id = item.Id,
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate,
            };
        }
        public static BLL.Interface.Entities.User ToBll(this MembershipUser item)
        {
            return new BLL.Interface.Entities.User
            {
                Id = item.ProviderUserKey.ToString(),
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreationDate
            };
        }
        public static User ToWeb(this BLL.Interface.Entities.User item)
        {
            return new User
            {
                 Id = item.Id,
                 Email = item.Email,
                 IsApproved = item.IsApproved,
                 CreateDate = item.CreateDate,
                 Profile = new Lazy<Profile>(() => item.Profile.ToWeb()),
                 Roles = new Lazy<IEnumerable<Role>>( () => item.Roles.Select(r => r.ToWeb()).ToList())
            };
        }

        public static BLL.Interface.Entities.Profile ToBll (this Profile item)
        {
            return new BLL.Interface.Entities.Profile
            {
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday,
                Avatar =  item.Avatar
            };
        }
        public static Profile ToWeb(this BLL.Interface.Entities.Profile item)
        {
            return new Profile
            {
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday,
                Avatar = item.Avatar
            };
        }

        public static Role ToWeb(this BLL.Interface.Entities.Role item)
        {
            return new Role
            {

            };
        }

        public static BLL.Interface.Entities.Image ToBll(this Image item)
        {
            return new BLL.Interface.Entities.Image
            {
                 Id = item.Id,
                 Data = item.Data,
                 MimeType = item.MimeType
            };
        }
        public static Image ToWeb(this BLL.Interface.Entities.Image item)
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