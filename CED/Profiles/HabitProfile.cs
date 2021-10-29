using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CED.Models.Core;
using CED.Models.DTO;

namespace CED.Profiles
{
    public class HabitProfile : Profile
    {
        public HabitProfile()
        {
            CreateMap<Habit, HabitDTO>();
            CreateMap<HabitDTO, Habit>();
            CreateMap<HabitSaveDTO, Habit>()
                .ForPath(o => o.HabitType.Id, opt => opt.MapFrom(src => src.HabitType))
                .ForPath(o => o.Schedule.ScheduleType.Id, opt => opt.MapFrom(src => src.Schedule.ScheduleType))
                .ForPath(o => o.Schedule.ScheduleTime, opt => opt.MapFrom(src => src.Schedule.ScheduleTime))
                .ForPath(o => o.Schedule.UserId, opt => opt.MapFrom(src => src.Schedule.UserId))
                .ForMember(o => o.Frequencies, opt => opt.MapFrom(src => src.Frequencies))
                .ForMember(o => o.friendHabits, opt => opt.MapFrom(src => src.FriendHabits));

            CreateMap<ScheduleDTO, Schedule>().ReverseMap();
            CreateMap<FrequencyDTO, Frequency>().ReverseMap();
            CreateMap<FriendHabitDTO, FriendHabit>().ReverseMap();
        }
    }
}
