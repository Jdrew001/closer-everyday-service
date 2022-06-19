using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;

namespace CED.Services.Interfaces
{
  public interface IDashboardGraphContext
  {
    public void SetStrategy<T>();
    public Type GetStrategy();
    public List<GraphDataResponseDTO> CreateDataForGraph<T>(T data, List<HabitLog> logs);
  }
}
