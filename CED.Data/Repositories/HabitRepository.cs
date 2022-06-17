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
  public class HabitRepository : DataProvider, IHabitRepository
  {
    private readonly IFrequencyRepository _frequencyRepository;

    public HabitRepository(
        IOptions<ConnectionStrings> connectionStrings,
        IFrequencyRepository frequencyRepository)
        : base(connectionStrings.Value.CEDDB)
    {
      _frequencyRepository = frequencyRepository;
    }

    #region Habit Methods
    public async Task<List<Habit>> GetAllHabits()
    {
      //GetAllHabits
      List<Habit> habits = new List<Habit>();
      string spName = "GetAllHabits";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;

      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
      {
        var habit = ReadHabit(drh);
        habit.Frequency = await _frequencyRepository.GetHabitFrequency(habit.Id);
        habits.Add(habit);
      }

      return habits;
    }
    public async Task<List<Habit>> GetAllUserHabits(Guid userId)
    {
      //GetAllUserHabits
      List<Habit> habits = new List<Habit>();
      string spName = "GetAllUserHabits";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("UserId", userId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
      {
        var habit = ReadHabit(drh);
        habits.Add(habit);
      }

      return habits;
    }
    public async Task<Habit> GetHabitById(Guid id)
    {
      //GetHabitById
      Habit habit = null;
      string spName = "GetHabitById";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", id.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
      {
        habit = ReadHabit(drh);
        habit.Frequency = await _frequencyRepository.GetHabitFrequency(habit.Id);
      }

      return habit;
    }

    public async Task<Habit> SaveHabit(Habit habit)
    {
      string spName = "CreateHabit";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("Name", habit.Name);
      command.Parameters.AddWithValue("Icon", habit.Icon);
      command.Parameters.AddWithValue("Reminder", habit.Reminder);
      command.Parameters.AddWithValue("ReminderAt", habit.ReminderAt);
      command.Parameters.AddWithValue("VisibleToFriends", habit.VisibleToFriends);
      command.Parameters.AddWithValue("Description", habit.Description);
      command.Parameters.AddWithValue("HabitTypeId", habit.HabitType.Id);
      command.Parameters.AddWithValue("UserId", habit.UserId.ToString());
      command.Parameters.AddWithValue("CreatedAt", new DateTime());
      command.Parameters.AddWithValue("ActiveInd", "A");
      command.Parameters.AddWithValue("ScheduleId", habit.Schedule.Id.ToString());

      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        habit = ReadHabit(drh);

      return habit;
    }

    public async Task<Habit> UpdateHabit(Habit habit)
    {
      string spName = "UpdateHabit";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habit.Id.ToString());
      command.Parameters.AddWithValue("Name", habit.Name);
      command.Parameters.AddWithValue("Icon", habit.Icon);
      command.Parameters.AddWithValue("Reminder", habit.Reminder);
      command.Parameters.AddWithValue("ReminderAt", habit.ReminderAt);
      command.Parameters.AddWithValue("VisibleToFriends", habit.VisibleToFriends);
      command.Parameters.AddWithValue("Description", habit.Description);
      command.Parameters.AddWithValue("HabitTypeId", habit.HabitType.Id);
      command.Parameters.AddWithValue("UserId", habit.UserId.ToString());
      command.Parameters.AddWithValue("CreatedAt", new DateTime());
      command.Parameters.AddWithValue("ActiveInd", "A");
      command.Parameters.AddWithValue("ScheduleId", habit.Schedule.Id.ToString());

      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        habit = ReadHabit(drh);

      return habit;
    }
    public bool DeleteHabitById(Guid id)
    {
      throw new System.NotImplementedException();
    }
    #endregion

    #region Friend Methods
    public async Task<List<FriendHabit>> GetFriendHabits(Guid habitId)
    {
      List<FriendHabit> friendHabits = new List<FriendHabit>();
      string spName = "GetHabitFriends";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        friendHabits.Add(ReadFriendHabit(drh));

      return friendHabits;
    }
    #endregion

    #region Habit Log Methods
    public async Task<HabitLog> SaveHabitLog(char status, Guid userId, Guid habitId)
    {
      HabitLog log = null;
      string spName = "AddHabitLog";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("Value", status);
      command.Parameters.AddWithValue("UserId", userId.ToString());
      command.Parameters.AddWithValue("HabitId", habitId.ToString());

      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        log = ReadHabitLog(drh);

      return log;
    }

    public async Task<HabitLog> UpdateHabitLog(char status, Guid habitId, string date)
    {
      HabitLog log = null;
      string spName = "UpdateHabitLog";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("Value", status);
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      command.Parameters.AddWithValue("DateValue", date);

      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        log = ReadHabitLog(drh);

      return log;
    }

    public async Task<HabitLog> GetHabitLogByIdAndDate(Guid id, string date)
    {
      HabitLog log = null;
      string spName = "GetHabitLogByIdAndDate";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", id.ToString());
      command.Parameters.AddWithValue("DateValue", date);
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        log = ReadHabitLog(drh);

      return log;
    }

    public async Task<List<HabitLog>> GetLogsForHabit(Guid habitId)
    {
      List<HabitLog> habitLogs = new List<HabitLog>();
      string spName = "GetAllLogsForHabit";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        habitLogs.Add(ReadHabitLog(drh));

      return habitLogs;
    }
    public async Task<HabitLog> GetHabitLogById(Guid id)
    {
      HabitLog log = null;
      string spName = "GetHabitLogById";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", id.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        log = ReadHabitLog(drh);

      return log;
    }
    public async Task<List<HabitLog>> GetAllCompletedLogsForUser(Guid userId)
    {
      List<HabitLog> logs = new List<HabitLog>();
      string spName = "GetCompletedLogsForUser";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("UserId", userId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        logs.Add(ReadHabitLog(drh));

      return logs;
    }

    public async Task<List<HabitLog>> GetAllLogsForUser(Guid userId)
    {
      List<HabitLog> logs = new List<HabitLog>();
      string spName = "GetLogsForUser";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("UserId", userId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        logs.Add(ReadHabitLog(drh));

      return logs;
    }
    public async Task<List<HabitLog>> GetAllCompletedLogsForHabit(Guid habitId)
    {
      List<HabitLog> logs = new List<HabitLog>();
      string spName = "GetCompletedLogsForHabit";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        logs.Add(ReadHabitLog(drh));

      return logs;
    }
    #endregion

    #region Private Methods
    private Habit ReadHabit(DataReaderHelper drh)
    {
      return new Habit()
      {
        Id = new Guid(drh.Get<string>("idhabit")),
        Name = drh.Get<string>("name"),
        Icon = drh.Get<byte[]>("icon"),
        Reminder = drh.Get<bool>("reminder"),
        ReminderAt = drh.Get<DateTime>("reminderAt"),
        VisibleToFriends = drh.Get<bool>("visibleToFriends"),
        Description = drh.Get<string>("description"),
        Status = drh.Get<string>("status"),
        Frequency = new Frequency()
        {
          Id = new Guid(drh.Get<string>("frequencyId")),
          Value = drh.Get<int>("frequencyVal").ToString(),
          FrequencyType = new FrequencyType()
          {
            Id = drh.Get<int>("frequencyTypeId"),
            Value = drh.Get<string>("freqTypeValue")
          }
        },
        HabitType = new HabitType()
        {
          Id = drh.Get<int>("habitTypeId"),
          Value = drh.Get<string>("habitType"),
          Description = drh.Get<string>("habitTypeDescription")
        },
        Schedule = new Schedule()
        {
          Id = new Guid(drh.Get<string>("idSchedule")),
          ScheduleTime = drh.Get<DateTime>("schedule_time"),
          ScheduleType = new ScheduleType()
          {
            Id = drh.Get<int>("idschedule_type"),
            Value = drh.Get<string>("scheduleType")
          }
        },
        UserId = new Guid(drh.Get<string>("userId"))
      };
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
    private HabitLog ReadHabitLog(DataReaderHelper drh)
    {
      return new HabitLog()
      {
        Id = new Guid(drh.Get<string>("id")),
        Value = drh.Get<char>("value"),
        UserId = new Guid(drh.Get<string>("userId")),
        HabitId = new Guid(drh.Get<string>("habitId")),
        CreatedAt = drh.Get<DateTime>("createdAt")
      };
    }
    #endregion
  }
}
