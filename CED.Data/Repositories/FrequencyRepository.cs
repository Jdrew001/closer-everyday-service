using System;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;
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
        public Task<List<Frequency>> GetAllFrequencies()
        {
            throw new System.NotImplementedException();
        }

        public Task<Frequency> GetFrequencyById(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Frequency>> GetHabitFrequencies(Guid habitId)
        {
            //GetHabitFrequencies
            List<Frequency> frequencies = new List<Frequency>();
            string spName = "GetHabitFrequencies";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", habitId.ToString());
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                frequencies.Add(ReadFrequency(drh));

            return frequencies;
        }

        public async Task<Frequency> SaveHabitFrequency(int frequencyId, Guid habitId)
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

        public async Task<List<Frequency>> ClearHabitFrequencies(Guid habitId)
        {
            List<Frequency> frequencies = new List<Frequency>();
            string spName = "ClearFrequenciesForHabit";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", habitId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                frequencies.Add(ReadFrequency(drh));

            return frequencies;
        }

        private Frequency ReadFrequency(DataReaderHelper drh)
        {
            return new Frequency()
            {
                Id = drh.Get<int>("idfrequency"),
                Value = drh.Get<string>("frequency")
            };
        }
    }
}
