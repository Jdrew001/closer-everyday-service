using CED.Data;
using CED.Data.Interfaces;
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

        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceService> _logs;

        public DeviceService(
            ILogger<DeviceService> log,
            IOptions<ConnectionStrings> connectionStrings,
            IDeviceRepository deviceRepository)
            : base(connectionStrings.Value.CEDDB)
        {
            _logs = log;
            _deviceRepository = deviceRepository;
        }

        public Task<Device> CreateNewUserDevice(DeviceDTO dto)
        {
            return _deviceRepository.CreateDevice(dto);
        }

        public Task<Device> GetDeviceById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetUserDevices(Guid userId)
        {
            return _deviceRepository.GetUserDevices(userId);
        }

        public Task<Device> DeactivateUserDevice(DeviceDTO dto)
        {
            return _deviceRepository.DeactivateDevice(dto.UUID);
        }
    }
}
