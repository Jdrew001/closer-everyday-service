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
    public class MilestoneServiceTest
    {
        private readonly Mock<IHabitService> _habitService = new();
        private readonly Mock<INotificationService> _notificationService = new();
        private readonly Mock<IUserService> _userService = new();
        private readonly Mock<IMilestoneRepository> _milestoneRepository = new();
        private readonly MilestoneService _milestoneService;

        private readonly Guid _userId = Guid.NewGuid();
        private readonly Guid _habitId = Guid.NewGuid();

        private readonly int[] _milestones = MilestoneConstants.VALUES;

        public MilestoneServiceTest()
        {
            
            _milestoneService = new MilestoneService(
                _habitService.Object, _notificationService.Object, _userService.Object, _milestoneRepository.Object);
        }

        #region First Global Completed habit
        [Fact]
        public void FirstCompletedHabitMilestoneExists()
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
                o.GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Perfect, _milestones[0].ToString()))
                .Returns(Task.FromResult(milestone));
            #endregion

            #region Act
            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            #endregion

            #region Assert
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Never);
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
        public void FirstCompletedHabitMilestoneDoesnotExists5CompletedLogs()
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

            _milestoneService.CheckForGlobalCompletions(_userId, habitLogs);
            _milestoneRepository.Verify(o => o.CreateGlobalMilestone(
                1, _userId, _milestones[0].ToString()), Times.Never);
        }
        #endregion

        #region Global Perfect Days

        #endregion
    }
}
