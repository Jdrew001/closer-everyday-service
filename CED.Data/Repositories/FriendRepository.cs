using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

    private FriendHabit ReadFriendHabit(DataReaderHelper drh)
    {
      return new FriendHabit()
      {
        Id = new Guid(drh.Get<string>("id")),
        FriendId = new Guid(drh.Get<string>("friendId")),
        OwnerId = new Guid(drh.Get<string>("ownerId"))
      };
    }
  }
}
