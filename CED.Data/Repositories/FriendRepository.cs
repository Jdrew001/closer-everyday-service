using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;

namespace CED.Data.Repositories
{
    public class FriendRepository : DataProvider, IFriendRepository
    {
        public FriendRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<List<FriendHabit>> GetFriendsForHabit(Guid habitId)
        {
            List<FriendHabit> habitFriends = new List<FriendHabit>();
            string spName = "GetHabitFriends";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", habitId.ToString());
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                habitFriends.Add(ReadFriendHabit(drh));

            return habitFriends;
        }

        public async Task<FriendHabit> GetFriendHabitById(Guid friendHabitId)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendHabit> SaveFriendToHabit(Guid userId, Guid habitId, Guid ownerId)
        {
            FriendHabit friendHabit = null;
            string spName = "SaveFriendHabit";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("FriendId", userId.ToString());
            command.Parameters.AddWithValue("HabitId", habitId.ToString());
            command.Parameters.AddWithValue("OwnerId", ownerId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                friendHabit = ReadFriendHabit(drh);

            return friendHabit;
        }

        public async Task<FriendHabit> ClearFriendToHabit(Guid userId, Guid habitId, Guid ownerId)
        {
            FriendHabit friendHabit = null;
            string spName = "ClearFriendHabit";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("FriendId", userId.ToString());
            command.Parameters.AddWithValue("HabitId", habitId.ToString());
            command.Parameters.AddWithValue("OwnerId", ownerId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                friendHabit = ReadFriendHabit(drh);

            return friendHabit;
        }

        public async Task<bool> RemoveFriendById(Guid userId, Guid friendId)
        {
            string addedId = null;
            string spName = "RemoveFriendToUser";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId.ToString());
            command.Parameters.AddWithValue("FriendId", friendId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                addedId = drh.Get<string>("iduser_friends");

            return addedId == null;
        }

        public async Task<FriendUser> AddFriendById(Guid userId, Guid friendId)
        {
            FriendUser friendUser = null;
            string spName = "AddFriendToUser";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId.ToString());
            command.Parameters.AddWithValue("FriendId", friendId.ToString());
            command.Parameters.AddWithValue("CreatedAt", new DateTime());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                friendUser = ReadFriendUser(drh);

            return friendUser;
        }

        public async Task<List<FriendUser>> GetUserFriends(Guid userId)
        {
            List<FriendUser> friendUsers = new List<FriendUser>();
            string spName = "GetUserFriends";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", userId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                friendUsers.Add(ReadFriendUser(drh));

            return friendUsers;
        }

        private FriendHabit ReadFriendHabit(DataReaderHelper drh)
        {
            return new FriendHabit()
            {
                Id = new Guid(drh.Get<string>("id")),
                FriendId = new Guid(drh.Get<string>("friendId")),
                FriendFirstName = drh.Get<string>("FirstName"),
                FriendLastName = drh.Get<string>("LastName"),
                FriendEmail = drh.Get<string>("Email"),
                OwnerId = new Guid(drh.Get<string>("ownerId"))
            };
        }

        private FriendUser ReadFriendUser(DataReaderHelper drh)
        {
            return new FriendUser()
            {
                Id = new Guid(drh.Get<string>("id")),
                UserId = new Guid(drh.Get<string>("userId")),
                FirstName = drh.Get<string>("firstName"),
                LastName = drh.Get<string>("lastName"),
                Email = drh.Get<string>("email")
            };
        }
    }
}
