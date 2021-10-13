using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IHabitService
    {
        Task<List<Habit>> GetAllHabits();
        Task<List<Habit>> GetAllUserHabits(int userId);
        Task<Habit> GetHabitById(int id);
        Task<HabitLog> GetHabitLogByIdCurrentDate(int id);
        Task<HabitLog> GetHabitLog(int id);
        Task<Habit> SaveHabit(Habit habit);
        Task<Habit> UpdateHabit(Habit habit);
        Task<HabitLog> SaveHabitLog(char status, int userId, int habitId);
        bool MarkHabitInactive(int id);
    }
}
