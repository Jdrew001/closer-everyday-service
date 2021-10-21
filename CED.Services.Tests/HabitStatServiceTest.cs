using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Core;
using CED.Services.Interfaces;
using Moq;
using Xunit;

namespace CED.Services.Tests
{
    public class HabitStatServiceTest
    {
        #region Habit Max Streak
        [Fact]
        public async void GetMaxStreakHabitDatesInStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));


            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates2Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates1Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates0Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesMultipleHabitsStreak1()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesMultipleHabitsStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Habit Current Streak
        [Fact]
        public async void GetCurrentHabitStreak2()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
            };

            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
            };

            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreak3MultiLogs()
        {
            var habitService = new Mock<IHabitService>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllHabitLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion
    }
}
