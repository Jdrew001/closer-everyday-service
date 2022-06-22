using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
  public interface IHabitService
  {
    Task<List<Habit>> GetAllHabits();
    Task<List<Habit>> GetAllUserHabits(Guid userId);
    Task<List<Habit>> GetHabitsByLogDate(DateTime date, Guid userId, string schedule);
    Task<Habit> GetHabitById(Guid id);
    Task<HabitLog> GetHabitLogByIdDate(Guid id, string date);
    Task<HabitLog> GetHabitLog(Guid id);
    Task<List<HabitLog>> GetUserHabitLogs(Guid userId);
    Task<Habit> SaveHabit(Habit habit);
    Task<Habit> UpdateHabit(Habit habit);
    Task<HabitLog> SaveHabitLog(char status, Guid userId, Guid habitId, string date);
    Task<List<HabitLog>> GetAllCompletedLogsForUser(Guid userId);
    Task<List<HabitLog>> GetAllCompletedLogsForHabit(Guid habitId);
    bool MarkHabitInactive(Guid id);
    Task<List<HabitLog>> GetLogsForHabit(Guid habitId);
    Task<List<GraphDataResponseDTO>> GetGraphData<T, K>(Guid userId, T request);
  }
}
