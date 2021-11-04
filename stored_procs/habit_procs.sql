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
    
    SELECT * FROM `ceddb`.`habit` h
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
    
    SELECT * FROM `ceddb`.`habit` h
    JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.idhabit = @id;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllUserHabits;

DELIMITER //

CREATE PROCEDURE GetAllUserHabits(
	IN
    UserId VARCHAR(255)
)
BEGIN
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
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.userId = UserId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllHabits;

DELIMITER //

CREATE PROCEDURE GetAllHabits()
BEGIN
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
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId;
END //

DELIMITER ;
-- --------------------------------------


DROP PROCEDURE IF EXISTS GetHabitById;

DELIMITER //

CREATE PROCEDURE GetHabitById(
	IN
    HabitId VARCHAR(255)
)
BEGIN
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
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
	WHERE h.idhabit = HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetHabitFriends;

DELIMITER //

CREATE PROCEDURE GetHabitFriends(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		fh.idfriend_habit as "id",
        u.iduser as "friendId",
        u.firstname as "FirstName",
        u.lastname as "LastName",
        u.email as "Email",
        fh.ownerId
    FROM `CEDDB`.`friend_habit` fh
	JOIN User u ON fh.friendId=u.iduser
	WHERE fh.habitId = HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetHabitLogById;

DELIMITER //

CREATE PROCEDURE GetHabitLogById(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE hl.habit_id = HabitId);

    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`idhabit_log` = @id;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetHabitLogByIdAndDate;

DELIMITER //

CREATE PROCEDURE GetHabitLogByIdAndDate(
	IN
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.`habit_id` = HabitId;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetAllLogsForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`habit_id` = HabitId
    ORDER BY hl.created_at ASC;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS AddHabitLog;

DELIMITER //

CREATE PROCEDURE AddHabitLog(
	IN
    HabitId VARCHAR(255),
    UserId VARCHAR(255),
    `Value` CHAR(1)
)
BEGIN
	SET @id = UUID();
    
    INSERT INTO `ceddb`.`habit_log`
		(`idhabit_log`,
        `log_value`,
		`user_id`,
		`habit_id`)
	VALUES
		(@id,
        `Value`,
		UserId,
		HabitId);
        
	SELECT 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    FROM `ceddb`.`habit_log` hl WHERE hl.`idhabit_log`=@id;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateHabitLog;

DELIMITER //

CREATE PROCEDURE UpdateHabitLog(
	IN
    `Value` CHAR(1),
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId);

	UPDATE `ceddb`.`habit_log` SET
		`log_value` = `Value`
		WHERE `idhabit_log` = @id;
        
	select 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    from `ceddb`.`habit_log` hl WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetCompletedLogsForUser;

DELIMITER //

CREATE PROCEDURE GetCompletedLogsForUser(
	IN
    UserId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.user_id= UserId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetCompletedLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetCompletedLogsForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.habit_id = HabitId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAvgSuccessLogsForUser;

DELIMITER //

CREATE PROCEDURE GetAvgSuccessLogsForUser(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT (
		(SELECT COUNT(*) FROM HABIT_LOG WHERE user_id = UserId and log_value = 'C') /
		(SELECT COUNT(*) FROM HABIT_LOG WHERE user_id = UserId) * 100) 
	AS 'COMPLETED_PERCENTAGE';
END //

DELIMITER ;
-- --------------------------------------


DROP PROCEDURE IF EXISTS GetLogsForUser;

DELIMITER //

CREATE PROCEDURE GetLogsForUser(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT * FROM `ceddb`.`habit_log` hl
    WHERE hl.user_id = UserId ORDER BY hl.created_at ASC;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetUserFriendHabitStats;

DELIMITER //

CREATE PROCEDURE GetUserFriendHabitStats(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT (
		(SELECT COUNT(*) FROM `ceddb`.friend_habit WHERE user_id = UserId))
	AS 'FRIEND_STAT';
END //

DELIMITER ;
-- --------------------------------------