using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Core;
using CED.Services.Interfaces;
using CED.Services.utils;
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
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));


            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesInStreaknull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));


            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates2Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates1Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDates0Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesMultipleHabitsStreak1()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(20);
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesMultipleHabitsStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
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
            var habitRepo = new Mock<IHabitStatRepository>();
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

            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreakNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;

            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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

            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreak3MultiLogs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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

            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Global Habit Success rate tests
        [Fact]
        public async void GetGlobalHabitSuccessRate()
        {
            var habitService = new Mock<IHabitService>();
            var habitStatRepo = new Mock<IHabitStatRepository>();
            double zero = 0;
            habitStatRepo.Setup(o => o.GetGlobalSuccessRate(20)).Returns(Task.FromResult(zero));

            HabitStatService statService = new HabitStatService(habitService.Object, habitStatRepo.Object);
            double expectedResult = 0;
            double result = await statService.GetAverageSuccessRate(20);

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Global habit perfect days tests
        [Fact]
        public async void GetHabitPerfectDays2()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
                    Id = 9,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 2,
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
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public async void GetHabitPerfectDays4()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
                    Id = 9,
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = 2,
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
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = 1,
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
            habitService.Setup(o => o.GetUserHabitLogs(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(20);
            var expectedResult = 4;

            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public async void GetHabitPerfectDaysnull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetUserHabitLogs(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(20);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);

        }
        #endregion

        #region Friends user supports tests
        [Fact]
        public async void GetFriendsSupporing2()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            habitRepo.Setup(o => o.GetFriendStat(20)).Returns(Task.FromResult(2));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetTotalFriendsSupporting(20);
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);

        }
        #endregion

        #region Monthly success reate tests

        [Fact]
        public async void GetMonthlySuccessRate()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 9,
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = 2,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(20, 2021);
            var janExpectedResultRate = 100;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 50;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }

        [Fact]
        public async void GetMonthlySuccessRateNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetUserHabitLogs(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(20, 2021);
            var expectedResult = 0;
            Assert.Equal(expectedResult, result.Count);
        }
        #endregion

        [Fact]
        public async void GetTotalCompletions6()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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

            habitService.Setup(o => o.GetAllCompletedLogsForUser(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletions(20);
            var expectedResult = 6;

            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async void GetCurrentStreakForHabit6()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = 3,
                    CreatedAt = DateTime.Parse("2021-10-07T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 2,
                    CreatedAt = DateTime.Parse("2021-10-08T22:59:28"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = 12,
                    CreatedAt = DateTime.Parse("2021-10-09T22:13:44"),
                    HabitId = 1,
                    UserId = 20,
                    Value = 'C'
                },
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

            habitService.Setup(o => o.GetLogsForHabit(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(20);
            var expectedResult = 6;

            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async void GetCurrentStreakForHabitNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;

            habitService.Setup(o => o.GetLogsForHabit(20)).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(20);
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }

        #region Get Max streak for habit
        [Fact]
        public async void GetMaxStreakForHabitDates1Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetLogsForHabit(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(20);
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakForHabitDates3Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
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
            habitService.Setup(o => o.GetLogsForHabit(20)).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(20);
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion
    }
}
