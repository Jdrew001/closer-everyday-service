using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CED.Services.Core
{
  public class DashboardGraphContextService : IDashboardGraphContext
  {
    private IDashboardGraphStrategy _strategy;

    public DashboardGraphContextService() { }

    public DashboardGraphContextService(IDashboardGraphStrategy strategy)
    {
      _strategy = strategy;
    }

    public List<GraphDataResponseDTO> CreateDataForGraph<T>(T data, List<HabitLog> logs)
    {
      return _strategy.createGraphData(data, logs);
    }

    public Type GetStrategy()
    {
      return _strategy.GetType();
    }

    public void SetStrategy<T>()
    {
      _strategy = (IDashboardGraphStrategy)Activator.CreateInstance(typeof(T));
    }
  }
}
