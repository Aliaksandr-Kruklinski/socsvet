using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IWallRepository : IRepository<Wall>
    {
        IEnumerable<WallMessage> GetAllMessages(int wallId);

        void AddMessage(int wallId, WallMessage message);
        void DeleteMessage(int wallId, int messageId);
        void UpdateMessage(int wallId, int messageId, string text);

        void AddComment(int messageId, WallComment comment);
        void DeleteComment(int messageId, int commentId);
        void UpdateComment(int messageId, int commentId, string text);
    }
}
