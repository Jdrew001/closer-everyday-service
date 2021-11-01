using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> GetDeviceByUUID(string UUID);
        Task<List<Device>> GetUserDevices(Guid userId);
        Task<Device> CreateDevice(DeviceDTO dto);
        Task<Device> ActivateDevice(string UUID);
        Task<Device> DeactivateDevice(string uuid);
    }
}
