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
  public class FrequencyRepository : DataProvider, IFrequencyRepository
  {
    public FrequencyRepository(
        IOptions<ConnectionStrings> connectionStrings)
        : base(connectionStrings.Value.CEDDB)
    {

    }

    // TODO: COMPLETED
    public async Task<Frequency> GetHabitFrequency(Guid habitId)
    {
      Frequency frequency = null;
      string spName = "GetHabitFrequency";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        frequency = ReadFrequency(drh);

      return frequency;
    }

    // TODO:
    public async Task<Frequency> SaveHabitFrequency(Guid frequencyId, Guid habitId)
    {
      Frequency frequency = null;
      string spName = "SaveHabitFrequency";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());
      command.Parameters.AddWithValue("FrequencyId", frequencyId);
      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        frequency = ReadFrequency(drh);

      return frequency;
    }

    public async Task<Frequency> ClearHabitFrequency(Guid habitId)
    {
      Frequency frequency = null;
      string spName = "ClearHabitFrequency";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("HabitId", habitId.ToString());

      using DataReaderHelper drh = await command.ExecuteReaderAsync();

      while (drh.Read())
        frequency = ReadFrequency(drh);

      return frequency;
    }

    public async Task<List<Day>> GetFrequencyDays(Guid frequencyId)
    {
      List<Day> days = new List<Day>();
      string spName = "GetFrequencyDays";
      using DataConnectionProvider dcp = CreateConnection();
      await using var command = dcp.CreateCommand(spName);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue("FrequencyId", frequencyId.ToString());

      using DataReaderHelper drh = await command.ExecuteReaderAsync();
      while (drh.Read())
        days.Add(ReadDay(drh));

      return days;
    }

    private Frequency ReadFrequency(DataReaderHelper drh)
    {

      return new Frequency()
      {
        Id = new Guid(drh.Get<string>("idfrequency")),
        Value = drh.Get<string>("frequency"),
        // get frequency type
        FrequencyType = new FrequencyType()
        {
          Id = drh.Get<int>("frequencyTypeId"),
          Value = drh.Get<string>("habitFrequency")
        }
      };
    }

    private Day ReadDay(DataReaderHelper drh)
    {
      return new Day()
      {
        Id = drh.Get<int>("dayid"),
        Value = drh.Get<string>("value"),
        Detail = drh.Get<string>("dayName")
      };
    }
  }
}
