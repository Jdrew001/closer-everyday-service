using CED.Data;
using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Core
{
    public class DeviceService : DataProvider, IDeviceService
    {

        private readonly ILogger<DeviceService> _logs;

        public DeviceService(
            ILogger<DeviceService> log,
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
            _logs = log;
        }

        public Task<Device> CreateNewUserDevice(DeviceDTO dto)
        {
            return createDevice(dto);
        }

        public Task<Device> GetDeviceById(int id)
        {
            return GetDeviceById(id);
        }

        public Task<List<Device>> GetUserDevices(int userId)
        {
            return getUserDevices(userId);
        }

        public Task<Device> DeactivateUserDevice(DeviceDTO dto)
        {
            return deactivateDevice(dto.UUID);
        }

        private async Task<Device> createDevice(DeviceDTO dto)
        {
            Device device = null;
            string spName = "CreateUserDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Model", dto.Model);
            command.Parameters.AddWithValue("Platform", dto.Platform);
            command.Parameters.AddWithValue("UUID", dto.UUID);
            command.Parameters.AddWithValue("Manufacturer", dto.Manufacturer);
            command.Parameters.AddWithValue("UserId", dto.UserId);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }

        private async Task<Device> getDeviceByUUID(string UUID)
        {
            Device device = null;
            string spName = "GetDeviceByUUID";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("DeviceUUID", UUID);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }

        private async Task<List<Device>> getUserDevices(int userId)
        {
            List<Device> devices = new List<Device>();
            string spName = "GetUsersDevices";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                devices.Add(ReadDevice(drh));

            return devices;
        }

        private async Task<Device> activateDevice(string UUID)
        {
            Device device = null;
            string spName = "ActivateDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("DeviceUUID", UUID);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }

        private async Task<Device> deactivateDevice(string UUID)
        {
            Device device = null;
            string spName = "DeactiveateDevice";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("DeviceUUID", UUID);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                device = ReadDevice(drh);

            return device;
        }

        private Device ReadDevice(DataReaderHelper drh)
        {
            return new Device()
            {
                DeviceId = drh.Get<int>("iddevice"),
                Model = drh.Get<string>("model"),
                Platform = drh.Get<string>("platform"),
                UUID = drh.Get<string>("uuid"),
                Manufacturer = drh.Get<string>("manufacturer"),
                User = null
            };
        }

        private User ReadUser(DataReaderHelper drh)
        {
            return new User()
            {
                Id = drh.Get<int>("iduser"),
                Email = drh.Get<string>("email"),
                Username = drh.Get<string>("username"),
                FirstName = drh.Get<string>("firstname"),
                LastName = drh.Get<string>("lastname"),
                LastLogin = drh.Get<DateTime?>("lastLogin"),
                Locked = drh.Get<bool>("locked"),
                DateLocked = drh.Get<DateTime?>("datelocked")
            };
        }
    }
}
