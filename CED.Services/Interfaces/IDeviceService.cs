using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<List<Device>> GetUserDevices(int userId);

        Task<Device> GetDeviceById(int id);
        Task<Device> CreateNewUserDevice(DeviceDTO dto);
        Task<Device> DeactivateUserDevice(DeviceDTO dto);
    }
}
