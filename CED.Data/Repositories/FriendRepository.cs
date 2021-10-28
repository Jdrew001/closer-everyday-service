using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;

namespace CED.Data.Repositories
{
    public class FriendRepository : DataProvider, IFriendRepository
    {
        public FriendRepository(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public async Task<List<FriendHabit>> GetFriendsForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendHabit> GetFriendHabitById(int friendHabitId)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendHabit> SaveFriendToHabit(int userId, int habitId, int ownerId)
        {
            FriendHabit friendHabit = null;
            string spName = "SaveFriendHabit";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("HabitId", habitId);
            command.Parameters.AddWithValue("OwnerId", ownerId);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                friendHabit = ReadFriendHabit(drh);

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

        private FriendHabit ReadFriendHabit(DataReaderHelper drh)
        {
            return new FriendHabit()
            {
                Firstname = drh.Get<string>("firstName"),
                Id = drh.Get<int>("Id"),
                LastName = drh.Get<string>("lastName"),
                OwnerId = drh.Get<int>("ownerId")
            };
        }
    }
}
