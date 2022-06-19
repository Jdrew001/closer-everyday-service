using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CED.Services.Strategies.GraphStrategies
{
  public class SelectStrategy : IDashboardGraphStrategy
  {
    public override List<GraphDataResponseDTO> createGraphData<T>(T data, List<HabitLog> logs)
    {
      var selectRequestDTO = data as DashboardGraphSelectRequest;
      dateSelected = selectRequestDTO.DateSelected;
      this.logs = logs;

      // get start date for a given week and end date DateTime.Parse(date, CultureInfo.InvariantCulture);

      var result = GetWeekBoundaries(selectRequestDTO.DateSelected, selectRequestDTO.Limit);
      var dto = result.ConvertAll(new Converter<Dictionary<WeekBoundary, string>, GraphDataResponseDTO>(ConvertToGraphDataDTO));
      return dto;
    }
  }
}
