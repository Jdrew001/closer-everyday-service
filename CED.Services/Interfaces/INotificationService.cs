using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface INotificationService
    {
        public void ScheduleNotification(string message);
    }
}
