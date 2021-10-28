using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Core;

namespace CED.Services.Core
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly ILogger<FriendHabit> _log;
        public FriendService(
            ILogger<FriendHabit> log,
            IFriendRepository friendRepository)
        {
            _log = log;
            _friendRepository = friendRepository;
        }

        public async Task<List<FriendHabit>> GetFriendsForHabit(int habitId)
        {
            List<FriendHabit> friendHabits = new List<FriendHabit>();
            try
            {
                friendHabits = await _friendRepository.GetFriendsForHabit(habitId);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception inside (Get Friends For Habit) habit id : {Habit Id}", habitId);
                throw;
            }

            return friendHabits;
        }

        public async Task<FriendHabit> GetFriendHabitById(int friendHabitId)
        {
            FriendHabit friendHabit;
            try
            {
                friendHabit = await _friendRepository.GetFriendHabitById(friendHabitId);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception inside (GetFriendHabitById) Friend Habit Id : {Friend Habit Id}", friendHabitId);
                throw;
            }

            return friendHabit;
        }

        public async Task<FriendHabit> SaveFriendToHabit(int userId, int habitId, int ownerId)
        {
            FriendHabit friendHabit;
            try
            {
                friendHabit = await _friendRepository.SaveFriendToHabit(userId, habitId, ownerId);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception inside (SaveFriendToHabit)" +
                                    "User I: {User}, Habit Id {Habit Id}, Owner Id {ownerId}", userId, habitId, ownerId);
                throw;
            }

            return friendHabit;
        }

        public async Task<User> GetFriendById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> RemoveFriendById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddFriendById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUserFriends(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
