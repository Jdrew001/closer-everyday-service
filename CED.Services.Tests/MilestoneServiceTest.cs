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
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CED.Services.Tests
{
    public class MilestoneServiceTest
    {
        private readonly Mock<IHabitService> _habitService = new();
        private readonly Mock<INotificationService> _notificationService = new();
        private readonly Mock<IUserService> _userService = new();
        private readonly Mock<IMilestoneRepository> _milestoneRepository = new();
        private readonly MilestoneService _milestoneService;
        private readonly Mock<ILogger<MilestoneService>> _log = new();

        private readonly Guid _userId = Guid.NewGuid();
        private readonly Guid _habitId = Guid.NewGuid();

        private readonly int[] _milestones = MilestoneConstants.VALUES;

        public MilestoneServiceTest()
        {
            
            _milestoneService = new MilestoneService(
                _habitService.Object, _notificationService.Object, _userService.Object, _milestoneRepository.Object, _log.Object);
        }

        [Fact]
        public async void CreateGlobalMilestoneNegativeCase()
        {
            MilestoneType nullType = null;
            Milestone milestone = null;
            _milestoneRepository.Setup(o =>
               o.CreateGlobalMilestone(1, _userId, "1"))
               .Returns(Task.FromResult(milestone));

            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.Completion))
                .Returns(Task.FromResult(nullType));

            var result = await _milestoneService.CreateGlobalMilestone(MileStoneSubType.Completion, _userId, "1");

            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, "1"), Times.Never);
        }

        #region First Global Completed habit
        [Fact]
        public void FirstCompletedHabitMilestoneExists()
        {
            #region Setup Data
            Milestone nullMilestone = null;
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "COMPLETIONS",
                Scope = "GLOBAL"
            };
            var milestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "1"
            };
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.Completion))
                .Returns(Task.FromResult(milestoneType));

            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Completion, "1"))
                .Returns(Task.FromResult(nullMilestone));

            _milestoneRepository.Setup(o =>
                o.CreateGlobalMilestone(milestoneType.Id, _userId, milestone.Value))
                .Returns(Task.FromResult(milestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Once);
            #endregion
        }

        [Fact]
        public void FirstCompletedHabitMilestoneDoesnotExists()
        {
            #region Setup Data
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "COMPLETIONS",
                Scope = "GLOBAL"
            };
            var milestoneScope = MileStoneScope.Global;
            var type = MileStoneSubType.Completion;
            Milestone m = null;
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o => 
                o.GetMilestoneByType(milestoneScope, type, _milestones[0].ToString()))
                .Returns(Task.FromResult(m));
            _milestoneRepository.Setup(o => o.GetMilestoneType(milestoneScope, type))
                .Returns(Task.FromResult(milestoneType));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            #endregion

            #region Assertions
            _milestoneRepository.Verify(o => o.GetMilestoneType(milestoneScope, type), Times.Once);
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                milestoneType.Id, _userId, _milestones[0].ToString()), Times.Once);
            #endregion
        }

        [Fact]
        public void FirstCompletedHabitMilestoneDoesnotExists7CompletedLogs()
        {
            #region Setup Data
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "COMPLETIONS",
                Scope = "GLOBAL"
            };
            var milestoneScope = MileStoneScope.Global;
            var type = MileStoneSubType.Completion;
            Milestone m = null;
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(milestoneScope, type, _milestones[1].ToString()))
                .Returns(Task.FromResult(m));
            _milestoneRepository.Setup(o => o.GetMilestoneType(milestoneScope, type))
                .Returns(Task.FromResult(milestoneType));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            #endregion

            #region Assertions
            _milestoneRepository.Verify(o => o.GetMilestoneType(milestoneScope, type), Times.Once);
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                milestoneType.Id, _userId, _milestones[1].ToString()), Times.Once);
            #endregion
        }

        [Fact]
        public void FirstCompletedHabitFalse()
        {
            #region Setup Data
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                }
            };
            var milestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = null,
                UserId = Guid.NewGuid(),
                Value = "1"
            };

            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Perfect, _milestones[0].ToString()))
                .Returns(Task.FromResult(milestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            #endregion

            #region Assertions
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Never);
            #endregion
        }
        #endregion

        #region Global Perfect Days
        [Fact]
        public void PerfectDaysAchieved1()
        {
            #region Setup
            Milestone nullMilestone = null;
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "PERFECT",
                Scope = "GLOBAL"
            };
            var milestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "1"
            };
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.Perfect))
                .Returns(Task.FromResult(milestoneType));

            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Perfect, "3"))
                .Returns(Task.FromResult(nullMilestone));

            _milestoneRepository.Setup(o =>
               o.CreateGlobalMilestone(milestoneType.Id, _userId, milestone.Value))
               .Returns(Task.FromResult(milestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalPerfectDays(_userId, habitLogs);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Once);
            #endregion
        }

        [Fact]
        public void PerfectDaysAchieved2()
        {
            #region Setup
            Milestone nullMilestone = null;
            var habitLogs = new List<HabitLog>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-10T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-11T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'C'
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Parse("2021-10-12T22:59:28"),
                    HabitId = _habitId,
                    UserId = _userId,
                    Value = 'F'
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "PERFECT",
                Scope = "GLOBAL"
            };
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.Perfect))
                .Returns(Task.FromResult(milestoneType));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalPerfectDays(_userId, habitLogs);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.GetMilestoneByType(
                MileStoneScope.Global, MileStoneSubType.Perfect, "2"), Times.Never);

            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Never);
            #endregion
        }
        #endregion

        #region Global Friends supported
        [Fact]
        public void FriendsSupported5()
        {
            #region Setup
            var friendId1 = Guid.NewGuid();
            var friendId2 = Guid.NewGuid();
            var habitId = Guid.NewGuid();
            var habits = new List<Habit>()
            {
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>()
                    {
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew",
                            FriendId = friendId1,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        },
                        new FriendHabit()
                        {
                            FriendEmail = "elatkison@gmail.com",
                            FriendFirstName = "Elizabeth",
                            FriendId = friendId2,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        },
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew",
                            FriendId = friendId1,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        },
                        new FriendHabit()
                        {
                            FriendEmail = "elatkison@gmail.com",
                            FriendFirstName = "Elizabeth",
                            FriendId = friendId2,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        }
                    },
                    habitLog = null
                },
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>()
                    {
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew1",
                            FriendId = Guid.NewGuid(),
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        },
                        new FriendHabit()
                        {
                            FriendEmail = "elatkison@gmail.com",
                            FriendFirstName = "Elizabeth2",
                            FriendId = Guid.NewGuid(),
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        },
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew2",
                            FriendId = Guid.NewGuid(),
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        }
                    },
                    habitLog = null
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "FRIEND",
                Scope = "GLOBAL"
            };
            Milestone milestone = null;
            var newMilestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "2"
            };
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.FriendsSupported, "5"));

            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.FriendsSupported))
                .Returns(Task.FromResult(milestoneType));

            _milestoneRepository.Setup(o =>
               o.CreateGlobalMilestone(milestoneType.Id, _userId, "5"))
               .Returns(Task.FromResult(newMilestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalFriendsSupported(_userId, habits);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, "5"));
            #endregion
        }

        [Fact]
        public void FriendsSupported1()
        {
            #region Setup
            var friendId1 = Guid.NewGuid();
            var friendId2 = Guid.NewGuid();
            var habitId = Guid.NewGuid();
            var habits = new List<Habit>()
            {
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>()
                    {
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew",
                            FriendId = friendId1,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        }
                    },
                    habitLog = null
                },
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>()
                    {
                        new FriendHabit()
                        {
                            FriendEmail = "dtatkison@gmail.com",
                            FriendFirstName = "Drew1",
                            FriendId = friendId1,
                            FriendLastName = "Atkison",
                            HabitId = habitId,
                            Id = new Guid(),
                            OwnerId = _userId
                        }
                    },
                    habitLog = null
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "FRIEND",
                Scope = "GLOBAL"
            };
            Milestone milestone = null;
            var newMilestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "2"
            };
            #endregion

            #region Setup Mocks
            _milestoneRepository.Setup(o =>
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.FriendsSupported, "1"));

            _milestoneRepository.Setup(o =>
                o.GetMilestoneType(MileStoneScope.Global, MileStoneSubType.FriendsSupported))
                .Returns(Task.FromResult(milestoneType));

            _milestoneRepository.Setup(o =>
               o.CreateGlobalMilestone(milestoneType.Id, _userId, "1"))
               .Returns(Task.FromResult(newMilestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalFriendsSupported(_userId, habits);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, "1"));
            #endregion
        }

        [Fact]
        public void FriendsSupportedNone()
        {
            #region Setup
            var friendId1 = Guid.NewGuid();
            var friendId2 = Guid.NewGuid();
            var habitId = Guid.NewGuid();
            var habits = new List<Habit>()
            {
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>(),
                    habitLog = null
                },
                new Habit()
                {
                    Description = "Description",
                    Frequencies = null,
                    friendHabits = new List<FriendHabit>(),
                    habitLog = null
                }
            };
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "FRIEND",
                Scope = "GLOBAL"
            };
            Milestone milestone = null;
            var newMilestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "2"
            };
            #endregion

            #region Act
            _milestoneService.CheckForGlobalFriendsSupported(_userId, habits);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, "1"), Times.Never);
            #endregion
        }

        [Fact]
        public void FriendsSupportedHabitNull()
        {
            #region Setup
            var friendId1 = Guid.NewGuid();
            var friendId2 = Guid.NewGuid();
            var habitId = Guid.NewGuid();
            List<Habit> habits = null;
            var milestoneType = new MilestoneType()
            {
                Id = 1,
                Name = "FRIEND",
                Scope = "GLOBAL"
            };
            Milestone milestone = null;
            var newMilestone = new Milestone()
            {
                Habit = null,
                Id = Guid.NewGuid(),
                MilestoneType = milestoneType,
                UserId = Guid.NewGuid(),
                Value = "2"
            };
            #endregion

            #region Act
            _milestoneService.CheckForGlobalFriendsSupported(_userId, habits);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, "1"), Times.Never);
            #endregion
        }
        #endregion
    }
}
