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
        public async void GetMaxStreakHabitDatesInStreak0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesInStreak1Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesInStreak3()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetMaxStreakHabitDatesInStreaknull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-09T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreak(Guid.NewGuid());
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Habit Current Streak
        [Fact]
        public async void GetCurrentHabitStreak0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreak1Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetCurrentHabitStreak2()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetCurrentHabitStreakNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetCurrentStreak(Guid.NewGuid());
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
            habitStatRepo.Setup(o => o.GetGlobalSuccessRate(Guid.NewGuid())).Returns(Task.FromResult(zero));

            HabitStatService statService = new HabitStatService(habitService.Object, habitStatRepo.Object);
            double expectedResult = 0;
            double result = await statService.GetAverageSuccessRate(Guid.NewGuid());

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Global habit perfect days tests
        [Fact]
        public async void GetPerfectDays0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetPerfectDays1Log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetPerfectDays2()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(Guid.NewGuid());
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetPerfectDays4()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(Guid.NewGuid());
            var expectedResult = 4;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetPerfectDaysnull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDays(Guid.NewGuid());
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
            habitRepo.Setup(o => o.GetFriendStat(Guid.NewGuid())).Returns(Task.FromResult(2));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetTotalFriendsSupporting(Guid.NewGuid());
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Monthly success reate tests
        [Fact]
        public async void GetMonthlySuccessRate0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(Guid.NewGuid(), 2021);
            var janExpectedResultRate = 0;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 0;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }
        [Fact]
        public async void GetMonthlySuccessRate1Log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(Guid.NewGuid(), 2021);
            var janExpectedResultRate = 100;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 0;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }
        [Fact]
        public async void GetMonthlySuccessRate()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(Guid.NewGuid(), 2021);
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
            habitService.Setup(o => o.GetUserHabitLogs(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRate(Guid.NewGuid(), 2021);
            var expectedResult = 0;
            Assert.Equal(expectedResult, result.Count);
        }
        #endregion

        #region Get Total Completions
        [Fact]
        public async void GetTotalCompletions0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletions(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }
        [Fact]
        public async void GetTotalCompletions1Log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletions(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(result, expectedResult);
        }
        [Fact]
        public async void GetTotalCompletions6()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForUser(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletions(Guid.NewGuid());
            var expectedResult = 6;

            Assert.Equal(result, expectedResult);
        }
        #endregion

        #region Current Streak for habits
        [Fact]
        public async void GetCurrentStreakForHabit0()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();

            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }
        [Fact]
        public async void GetCurrentStreakForHabit1()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-07T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(Guid.NewGuid());
            var expectedResult = 1;

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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-07T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-08T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-09T22:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(Guid.NewGuid());
            var expectedResult = 6;

            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async void GetCurrentStreakForHabitNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;

            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetCurrentStreakForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }
        #endregion

        #region Get Max streak for habit
        [Fact]
        public async void GetMaxStreakForHabitDates1StreakZeroLogs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetMaxStreakForHabitDates1StreakOneLog()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetMaxStreakForHabitDates1Streak()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(Guid.NewGuid());
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
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));

            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetMaxStreakForHabit(Guid.NewGuid());
            var expectedResult = 3;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Get Monthly Success Rate for Habit
        [Fact]
        public async void GetMonthlySuccessRateForHabit0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRateForHabit(Guid.NewGuid(), 2021);
            var janExpectedResultRate = 0;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 0;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }
        [Fact]
        public async void GetMonthlySuccessRateForHabit1log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRateForHabit(Guid.NewGuid(), 2021);
            var janExpectedResultRate = 100;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 0;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }
        [Fact]
        public async void GetMonthlySuccessRateForHabit()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-01-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-02-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-03-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-04-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRateForHabit(Guid.NewGuid(), 2021);
            var janExpectedResultRate = 100;
            var decExpectedResultRate = 0;
            var marchExpectedResultRate = 50;

            Assert.Equal(janExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[1]]);
            Assert.Equal(marchExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[3]]);
            Assert.Equal(decExpectedResultRate, result[ServiceConstants.MONTHS_OF_YEAR[12]]);
        }

        [Fact]
        public async void GetMonthlySuccessRateForHabitNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();

            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetMonthlySuccessRateForHabit(Guid.NewGuid(), 2021);
            var expectedResult = 0;

            Assert.Equal(expectedResult, result.Count);
        }
        #endregion

        #region Get Perfect Days for Habit
        [Fact]
        public async void GetHabitPerfectDays0Log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDaysForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetHabitPerfectDays1Log()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDaysForHabit(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async void GetHabitPerfectDays2()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'F'
                }
            };
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDaysForHabit(Guid.NewGuid());
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetHabitPerfectDaysNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;
            habitService.Setup(o => o.GetLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);

            var result = await service.GetPerfectDaysForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
        }
        #endregion

        #region Get Total Completions for habit
        [Fact]
        public async void GetTotalCompletionsForHabit0Logs()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>();

            habitService.Setup(o => o.GetAllCompletedLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletionsForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }
        [Fact]
        public async void GetTotalCompletionsForHabit1()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletionsForHabit(Guid.NewGuid());
            var expectedResult = 1;

            Assert.Equal(result, expectedResult);
        }
        [Fact]
        public async void GetTotalCompletionsForHabit6()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            var habitLogs = new List<HabitLog>()
            {
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-13T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                },
                new HabitLog()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T23:13:44"),
                    HabitId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Value = 'C'
                }
            };

            habitService.Setup(o => o.GetAllCompletedLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletionsForHabit(Guid.NewGuid());
            var expectedResult = 6;

            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async void GetTotalCompletionsForHabitNull()
        {
            var habitService = new Mock<IHabitService>();
            var habitRepo = new Mock<IHabitStatRepository>();
            List<HabitLog> habitLogs = null;

            habitService.Setup(o => o.GetAllCompletedLogsForHabit(Guid.NewGuid())).Returns(Task.FromResult(habitLogs));
            HabitStatService service = new HabitStatService(habitService.Object, habitRepo.Object);
            var result = await service.GetTotalCompletionsForHabit(Guid.NewGuid());
            var expectedResult = 0;

            Assert.Equal(result, expectedResult);
        }
        #endregion
    }
}
