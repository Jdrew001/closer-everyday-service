using System;
using CED.Models.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IHabitRepository
    {
        Task<List<Habit>> GetAllHabits();
        Task<List<Habit>> GetAllUserHabits(int userId, string date);
        Task<Habit> GetHabitById(int id);
        Task<List<FriendHabit>> GetFriendHabits(int habitId);
        Task<HabitLog> GetHabitLogById(int id);
        Task<HabitLog> GetHabitLogByIdAndDate(int id, string date);
        Task<List<HabitLog>> GetLogsForHabit(int habitId);
        Task<Habit> SaveHabit(Habit habit);
        Task<HabitLog> SaveHabitLog(char status, int userId, int habitId);
        Task<HabitLog> UpdateHabitLog(char status, int habitId, string date);
        Task<Habit> UpdateHabit(Habit habit);
        bool DeleteHabitById(int id);
    }
}
