using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models;
using CED.Models.Core;

namespace CED.Data.Interfaces
{
    public interface IFriendRepository
    {
        public Task<List<FriendHabit>> GetFriendsForHabit(Guid habitId);
        public Task<FriendHabit> GetFriendHabitById(Guid friendHabitId);
        public Task<FriendHabit> SaveFriendToHabit(Guid userId, Guid habitId, Guid ownerId);
        public Task<FriendHabit> ClearFriendToHabit(Guid userId, Guid habitId, Guid ownerId);
        public Task<FriendUser> GetFriendById(Guid id);
        public Task<bool> RemoveFriendById(Guid id);
        public Task<FriendUser> AddFriendById(Guid userId, Guid friendId);
        public Task<List<FriendUser>> GetUserFriends(Guid userId);
    }
}
