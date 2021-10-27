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
        Task<List<Habit>> GetAllUserHabits(int userId, string date);
        Task<Habit> GetHabitById(int id);
        Task<HabitLog> GetHabitLogByIdDate(int id, string date);
        Task<HabitLog> GetHabitLog(int id);
        Task<List<HabitLog>> GetUserHabitLogs(int userId);
        Task<Habit> SaveHabit(Habit habit);
        Task<Habit> UpdateHabit(Habit habit);
        Task<HabitLog> SaveHabitLog(char status, int userId, int habitId, string date);
        Task<List<HabitLog>> GetAllCompletedLogsForUser(int userId);
        Task<List<HabitLog>> GetAllCompletedLogsForHabit(int habitId);
        bool MarkHabitInactive(int id);

        Task<List<HabitLog>> GetLogsForHabit(int habitId);
    }
}
