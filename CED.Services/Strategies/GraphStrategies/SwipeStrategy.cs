using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CED.Services.Strategies.GraphStrategies
{
  public class SwipeStrategy : IDashboardGraphStrategy
  {
    private string boundary = "";

    public override List<GraphDataResponseDTO> createGraphData<T>(T data, List<HabitLog> logs)
    {
      var swipeDTO = data as SwipeDashboardGraphDTO;
      var date = DateTime.Parse(swipeDTO.DateSelected, CultureInfo.InvariantCulture);

      if (swipeDTO.Boundary.ToUpper() == "START")
      {
        dateSelected = date.AddDays(-7).ToString();
      }
      else
      {
        dateSelected = date.AddDays(7).ToString();
      }

      boundary = swipeDTO.Boundary;
      //dateSelected = swipeDTO.DateSelected;
      this.logs = logs;

      var result = GetWeekBoundaries(dateSelected, swipeDTO.Limit);
      var dto = result.ConvertAll(new Converter<Dictionary<WeekBoundary, string>, GraphDataResponseDTO>(ConvertToGraphDataDTO));
      foreach (var item in dto)
        item.Selected = false;

      if (swipeDTO.Boundary.ToUpper() == "START")
      {
        dto[0].Selected = true;
        dto[0].DefaultSelected = "Sat";
        dto.Reverse();
      }
      else
      {
        dto[0].Selected = true;
        dto[0].DefaultSelected = "Sun";
      }

      return dto;
    }

    public override int CalculateWeekMultiple(int index, int startingIndex, int middleIndex, int endingIndex)
    {
      return (index * (boundary.ToUpper() == "START" ? -1 : 1)) * weekDifference;
    }
  }
}
