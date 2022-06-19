using CED.Models.Core;
using CED.Models.DTO;
using moment.net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CED.Services.Interfaces
{
  public abstract class IDashboardGraphStrategy
  {
    protected int weekDifference = 7;
    protected string dateSelected;
    protected List<HabitLog> logs;

    /// <summary>
    /// Method creates the data for the graph showing the completion
    /// </summary>
    /// <typeparam name="T">Generic type -- DTO from UI</typeparam>
    /// <param name="data">DTO data from UI</param>
    /// <param name="logs">Logs for a given user -- should be all habit's logs</param>
    /// <returns>A list of dictionary key values - start and end dates</returns>
    public abstract List<GraphDataResponseDTO> createGraphData<T>(T data, List<HabitLog> logs);

    public abstract int CalculateWeekMultiple(int index, int startingIndex, int middleIndex, int endingIndex);

    public List<Dictionary<WeekBoundary, string>> GetWeekBoundaries(string date, int limit)
    {
      var data = new List<Dictionary<WeekBoundary, string>>();
      var startingDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
      var firstDayOfWeek = startingDate.FirstDateInWeek();

      // enforce the the limit to be an odd number
      if (limit % 2 == 0) // limit is even
      {
        limit += 1; // increment to an odd number
      }

      var startingIndex = 0;
      var endingIndex = limit - 1;

      // set the middle point -- ((9 - 1) / 2) = 4
      var middleIndex = ((limit - 1) / 2);

      for (int i = startingIndex; i < endingIndex; i++)
      {
        int weekMultiple = CalculateWeekMultiple(i, startingIndex, middleIndex, endingIndex);

        var firstDay = firstDayOfWeek.AddDays(weekMultiple).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        var lastDay = firstDayOfWeek.AddDays(weekMultiple + 6).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        data.Add(new Dictionary<WeekBoundary, string>()
        {
          { WeekBoundary.FirstDay, firstDay},
          { WeekBoundary.LastDay, lastDay }
        });
      }

      return data;
    }

    public GraphDataResponseDTO ConvertToGraphDataDTO(Dictionary<WeekBoundary, string> data)
    {
      GraphDataResponseDTO graphDataResponseDTO = new GraphDataResponseDTO();
      var startDate = data.GetValueOrDefault(WeekBoundary.FirstDay);
      var lastDate = data.GetValueOrDefault(WeekBoundary.LastDay);

      var startDateObject = DateTime.Parse(startDate, CultureInfo.InvariantCulture);
      var endDateObject = DateTime.Parse(lastDate, CultureInfo.InvariantCulture);

      var filteredLogs = this.logs.FindAll(o => o.CreatedAt.IsSameOrAfter(startDateObject) && o.CreatedAt.IsSameOrBefore(endDateObject.AddDays(1)));
      var dates = filteredLogs.Select(o => o.CreatedAt).Distinct().ToList();


      graphDataResponseDTO.SubTitle = $"{startDateObject.ToString("MMM dd")} - {endDateObject.ToString("MMM dd")}";
      graphDataResponseDTO.StartDate = startDate;
      graphDataResponseDTO.EndDate = lastDate;
      graphDataResponseDTO.Selected = selectedDate(startDateObject, endDateObject);
      graphDataResponseDTO.DefaultSelected = graphDataResponseDTO.Selected ? DateTime.Parse(dateSelected, CultureInfo.InvariantCulture).ToString("ddd") : null;
      graphDataResponseDTO.GraphData = GenerateGraphDTO(filteredLogs, dates);

      return graphDataResponseDTO;
    }

    private bool selectedDate(DateTime startDateObject, DateTime endDateObject)
    {
      var dateSelectedObject = DateTime.Parse(dateSelected, CultureInfo.InvariantCulture);
      return dateSelectedObject.IsSameOrAfter(startDateObject) && dateSelectedObject.IsSameOrBefore(endDateObject);
    }

    private List<GraphDataDTO> GenerateGraphDTO(List<HabitLog> logs, List<DateTime> dates)
    {
      List<GraphDataDTO> graphDataDTOs = new List<GraphDataDTO>();

      dates.ForEach(date =>
      {
        var logsForDate = logs.Where(o => o.CreatedAt.Date.IsSame(date.Date));
        var completedCountForDate = logsForDate.Count(o => o.Value.Equals('C') || o.Value.Equals('c'));
        var value = logsForDate.ToList().Count > 0 ? (completedCountForDate / logsForDate.ToList().Count) * 100 : 0;
        graphDataDTOs.Add(new GraphDataDTO()
        {
          Key = date.ToString("ddd"),
          Value = value,
          Date = date.ToString("MM/dd/yyyy")
        });
      });

      return graphDataDTOs;
    }
  }

  /// <summary>
  /// Week boundary is the first/last day of the week
  /// </summary>
  public enum WeekBoundary
  {
    FirstDay, LastDay
  }

  /// <summary>
  /// Week direction would be the previous weeks or the upcoming weeks
  /// </summary>
  public enum WeekDirection
  {
    Previous, Upcoming
  }
}
