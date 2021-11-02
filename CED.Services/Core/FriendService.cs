using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;

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

        public async Task<List<FriendHabit>> GetFriendsForHabit(Guid habitId)
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

        public async Task<FriendHabit> GetFriendHabitById(Guid friendHabitId)
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

        public async Task<FriendHabit> SaveFriendToHabit(Guid userId, Guid habitId, Guid ownerId)
        {
            FriendHabit friendHabit;
            try
            {
                friendHabit = await _friendRepository.SaveFriendToHabit(userId, habitId, ownerId);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception inside (SaveFriendToHabit)" +
                                    "User Id: {User}, Habit Id {Habit Id}, Owner Id {ownerId}", userId, habitId, ownerId);
                throw;
            }

            return friendHabit;
        }

        public async Task<FriendHabit> UpdateFriendToHabit(Guid userId, Guid habitId, Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetFriendById(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> RemoveFriendById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddFriendById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUserFriends(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
