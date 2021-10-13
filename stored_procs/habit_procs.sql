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
    Status char(1),
    UserId INT,
    ScheduleId INT,
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1),
    HabitId INT
)
BEGIN
    UPDATE `ceddb`.`habit` h SET
		`name`= Name,
        icon = Icon,
        reminder = Reminder,
        reminderAt = ReminderAt,
        visibleToFriends = VisibleToFriends,
        description = Description,
        status = Status,
        userid = UserId,
        scheduleId = ScheduleId,
        habitTypeId = HabitTypeId,
        createdAt = CreatedAt,
        active_ind = ActiveInd
	WHERE h.idhabit = HabitId;
    
    SELECT * FROM `ceddb`.`habit` h WHERE h.`idhabit`=HabitId;
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
    Status char(1),
    UserId INT,
    ScheduleId INT,
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1)
)
BEGIN
	INSERT INTO `ceddb`.`habit` (`name`, `icon`, `reminder`, `reminderAt`, `visibleToFriends`, `description`, `status`, `userId`, `scheduleId`, `habitTypeId`, `createdAt`, `active_ind`)
	VALUES(Name, Icon, Reminder, ReminderAt, VisibleToFriends, Description, Status, UserId, ScheduleId, HabitTypeId, CreatedAt, ActiveInd);
    
    SELECT * FROM `ceddb`.`habit` d WHERE d.`idhabit`=(SELECT last_insert_id());
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllUserHabits;

DELIMITER //

CREATE PROCEDURE GetAllUserHabits(
	IN
    UserId INT
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
    HabitId INT
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
    HabitId INT
)
BEGIN
    SELECT 
		fh.idfriend_habit as "id",
        u.firstname,
        u.lastname,
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
    HabitId INT
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

DROP PROCEDURE IF EXISTS GetHabitLogByIdAndCurrentDate;

DELIMITER //

CREATE PROCEDURE GetHabitLogByIdAndCurrentDate(
	IN
    HabitId INT
)
BEGIN

    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE Date(hl.`created_at`)=curdate();

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetAllLogsForHabit(
	IN
    HabitId INT
)
BEGIN
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`habit_id` = HabitId;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS AddHabitLog;

DELIMITER //

CREATE PROCEDURE AddHabitLog(
	IN
    HabitId INT,
    UserId INT,
    `Value` CHAR(1),
    `CreatedAt` DateTime
)
BEGIN
    INSERT INTO `ceddb`.`habit_log`
		(`log_value`,
		`user_id`,
		`habit_id`,
		`created_at`)
	VALUES
		(`Value`,
		UserId,
		HabitId,
		CreatedAt);
        
	SELECT * FROM `ceddb`.`habit_log` h WHERE h.`idhabit_log`=(SELECT last_insert_id());
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateHabitLog;

DELIMITER //

CREATE PROCEDURE UpdateHabitLog(
	IN
    `Value` CHAR(1),
    HabitId INT
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE hl.habit_id = HabitId);

	UPDATE `ceddb`.`habit_log` SET
		`log_value` = `Value`
		WHERE `idhabit_log` = @id;
        
	select * from `ceddb`.`habit_log` h where h.idhabit_log = @id; 
END //

DELIMITER ;
-- --------------------------------------