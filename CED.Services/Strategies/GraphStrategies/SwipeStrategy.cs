using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using System.Collections.Generic;

namespace CED.Services.Strategies.GraphStrategies
{
  public class SwipeStrategy : IDashboardGraphStrategy
  {
    public override List<GraphDataResponseDTO> createGraphData<T>(T data, List<HabitLog> logs)
    {
      var swipeDTO = data as SwipeDashboardGraphDTO;

      return null;
    }
  }
}
