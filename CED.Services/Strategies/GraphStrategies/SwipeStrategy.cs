using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CED.Services.Strategies.GraphStrategies
{
    public class SwipeStrategy : IDashboardGraphStrategy
    {
        public override List<GraphDataResponseDTO> createGraphData<T>(T data, List<HabitLog> logs)
        {
            var swipeDTO = data as SwipeDashboardGraphDTO;
            dateSelected = swipeDTO.DateSelected;
            this.logs = logs;

            var result = GetWeekBoundaries(dateSelected, swipeDTO.Limit);
            var dto = result.ConvertAll(new Converter<Dictionary<WeekBoundary, string>, GraphDataResponseDTO>(ConvertToGraphDataDTO));
            foreach (var item in dto)
                item.Selected = false;

            if (swipeDTO.Boundary.ToUpper() == "START")
                dto[0].Selected = true;
            else
            {
                dto.Reverse();
                dto[0].Selected = true;
            }

            return dto;
        }

        public override int CalculateWeekMultiple(int index, int startingIndex, int middleIndex, int endingIndex)
        {
            return (index * -1) * weekDifference;
        }
    }
}
