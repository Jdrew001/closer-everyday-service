-- --------------------------------------

DROP PROCEDURE IF EXISTS GetScheduleByHabitId;

DELIMITER //

CREATE PROCEDURE GetScheduleByHabitId(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		s.idschedule AS "scheduleId",
        s.userId as "userId",
        s.schedule_time AS "scheduleTime",
        st.idschedule_type AS "idScheduleType",
        st.schedule_value AS "scheduleTypeValue"
    FROM `CEDDB`.`habit` h
	JOIN `CEDDB`.`Schedule` s ON h.scheduleId=s.idschedule
    JOIN `CEDDB`.`ScheduleType` st ON s.schedule_type_id=st.idschedule_type
	WHERE h.habitId = HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS SaveSchedule;

DELIMITER //

CREATE PROCEDURE SaveSchedule(
	IN
    ScheduleTypeId INT,
    UserId VARCHAR(255),
    ScheduleTime DATETIME
)
BEGIN
	SET @id = UUID();
    INSERT INTO `ceddb`.`schedule` (`idschedule`, `schedule_type_id`, `user_id`, `schedule_time`)
	VALUES(@id, ScheduleTypeId, UserId, ScheduleTime);
    
    SELECT
    s.idschedule as "Id",
    s.schedule_time as "ScheduleTime",
    st.idschedule_type as "idschedule_type",
    st.schedule_value as "scheduleType",
    s.user_id as "UserId"
    FROM `ceddb`.`schedule` s 
    INNER JOIN schedule_type st ON st.idschedule_type = schedule_type_id
    WHERE s.`idschedule`=@id;
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
-- --------------------------------------