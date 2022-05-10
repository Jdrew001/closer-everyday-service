using System;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CED.Data.Repositories
{
    public class DeviceRepository : DataProvider, IDeviceRepository
    {
        public DeviceRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<Device> CreateDevice(DeviceDTO dto)
        {
            Device device = null;
            string spName = "CreateUserDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UUID", dto.UUID);
            command.Parameters.AddWithValue("Model", dto.Model);
            command.Parameters.AddWithValue("Platform", dto.Platform);
            command.Parameters.AddWithValue("Manufacturer", dto.Manufacturer);
            command.Parameters.AddWithValue("UserId", dto.UserId.ToString());
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }
        public async Task<Device> GetDeviceByUUID(string UUID)
        {
            Device device = null;
            string spName = "GetDeviceByUUID";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UUID", UUID);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }
        public async Task<List<Device>> GetUserDevices(Guid userId)
        {
            List<Device> devices = new List<Device>();
            string spName = "GetUsersDevices";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId.ToString());
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                devices.Add(ReadDevice(drh));

            return devices;
        }
        public async Task<Device> ActivateDevice(string UUID)
        {
            Device device = null;
            string spName = "ActivateDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UUID", UUID);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }
        public async Task<Device> DeactivateDevice(string uuid)
        {
            Device device = null;
            string spName = "DeactiveateDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UUID", uuid);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }
        private Device ReadDevice(DataReaderHelper drh)
        {
            return new Device()
            {
                DeviceId = new Guid(drh.Get<string>("iddevice")),
                Model = drh.Get<string>("model"),
                Platform = drh.Get<string>("platform"),
                UUID = drh.Get<string>("uuid"),
                Manufacturer = drh.Get<string>("manufacturer"),
                User = null
            };
        }
    }
}
