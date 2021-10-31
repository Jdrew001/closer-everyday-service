using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models;
using CED.Models.Core;

namespace CED.Services.Interfaces
{
    public interface IFriendService
    {
        public Task<List<FriendHabit>> GetFriendsForHabit(Guid habitId);
        public Task<FriendHabit> GetFriendHabitById(Guid friendHabitId);
        public Task<FriendHabit> SaveFriendToHabit(Guid userId, Guid habitId, Guid ownerId);
        public Task<User> GetFriendById(Guid id);
        public Task<bool> RemoveFriendById(Guid id);
        public Task<User> AddFriendById(Guid id);
        public Task<List<User>> GetUserFriends(Guid userId);

    }
}
