-- --------------------------------------

DROP PROCEDURE IF EXISTS ClearFrequenciesForHabit;

DELIMITER //

CREATE PROCEDURE ClearFrequenciesForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	DELETE from habit_frequency hf WHERE hf.freq_habit_id = HabitId;
    
     SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    JOIN habit_frequency h ON f.idfrequency = h.frequency_id
    WHERE h.freq_habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS ClearFriendHabit;

DELIMITER //

CREATE PROCEDURE ClearFriendHabit(
	IN
    FriendId VARCHAR(255),
    HabitId VARCHAR(255),
    OwnerId VARCHAR(255)
)
BEGIN
	DELETE from friend_habit fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
    
    SELECT * FROM `ceddb`.`friend_habit` fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateSchedule;

DELIMITER //

CREATE PROCEDURE UpdateSchedule(
	IN
    Id VARCHAR(255),
    ScheduleTypeId INT,
    UserId VARCHAR(255),
    ScheduleTime DATETIME
)
BEGIN
    UPDATE `ceddb`.`schedule` s SET
		`schedule_type_id`= ScheduleTypeId,
        user_id = UserId,
        schedule_time = ScheduleTime
	WHERE s.idschedule = Id;
    
    SELECT
		s.idschedule as "Id",
		s.schedule_time as "ScheduleTime",
		st.idschedule_type as "idschedule_type",
		st.schedule_value as "scheduleType",
		s.user_id as "UserId"
    FROM `ceddb`.`schedule` s 
    INNER JOIN schedule_type st ON st.idschedule_type = schedule_type_id
    WHERE s.`idschedule`=Id;
END //

DELIMITER ;
DROP PROCEDURE IF EXISTS UpdateHabit;

DELIMITER //

CREATE PROCEDURE UpdateHabit(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1),
    HabitId VARCHAR(255)
)
BEGIN
    UPDATE `ceddb`.`habit` h SET
		`name`= Name,
        icon = Icon,
        reminder = Reminder,
        reminderAt = ReminderAt,
        visibleToFriends = VisibleToFriends,
        description = Description,
        userid = UserId,
        scheduleId = ScheduleId,
        habitTypeId = HabitTypeId,
        createdAt = CreatedAt,
        active_ind = ActiveInd
	WHERE h.idhabit = HabitId;
    
    SELECT
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM `ceddb`.`habit` h
    JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.idhabit = HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS CreateHabit;

DELIMITER //

CREATE PROCEDURE CreateHabit(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1)
)
BEGIN
	SET @id = UUID();

	INSERT INTO `ceddb`.`habit` (`idhabit`, `name`, `icon`, `reminder`, `reminderAt`, `visibleToFriends`, `description`, `status`, `userId`, `scheduleId`, `habitTypeId`, `createdAt`, `active_ind`)
	VALUES(@id, Name, Icon, Reminder, ReminderAt, VisibleToFriends, Description, 'P', UserId, ScheduleId, HabitTypeId, CreatedAt, ActiveInd);
    
    SELECT
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM `ceddb`.`habit` h
    JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.idhabit = @id;
END //

DELIMITER ;
-- --------------------------------------