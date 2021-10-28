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
        public Task<List<FriendHabit>> GetFriendsForHabit(int habitId);
        public Task<FriendHabit> GetFriendHabitById(int friendHabitId);
        public Task<FriendHabit> SaveFriendToHabit(int userId, int habitId, int ownerId);
        public Task<User> GetFriendById(int id);
        public Task<bool> RemoveFriendById(int id);
        public Task<User> AddFriendById(int id);
        public Task<List<User>> GetUserFriends(int userId);
    }
}
