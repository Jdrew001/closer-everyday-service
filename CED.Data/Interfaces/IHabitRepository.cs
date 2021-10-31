using System;
using CED.Models.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IHabitRepository
    {
        Task<List<Habit>> GetAllHabits();
        Task<List<Habit>> GetAllUserHabits(Guid userId, string date);
        Task<Habit> GetHabitById(Guid id);
        Task<List<FriendHabit>> GetFriendHabits(Guid habitId);
        Task<HabitLog> GetHabitLogById(Guid id);
        Task<HabitLog> GetHabitLogByIdAndDate(Guid id, string date);
        Task<List<HabitLog>> GetLogsForHabit(Guid habitId);
        Task<List<HabitLog>> GetAllCompletedLogsForUser(Guid userId);
        Task<List<HabitLog>> GetAllLogsForUser(Guid userId);
        Task<List<HabitLog>> GetAllCompletedLogsForHabit(Guid habitId);
        Task<Habit> SaveHabit(Habit habit);
        Task<HabitLog> SaveHabitLog(char status, Guid userId, Guid habitId);
        Task<HabitLog> UpdateHabitLog(char status, Guid habitId, string date);
        Task<Habit> UpdateHabit(Habit habit);
        bool DeleteHabitById(Guid id);
    }
}
