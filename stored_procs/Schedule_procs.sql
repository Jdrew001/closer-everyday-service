-- --------------------------------------

DROP PROCEDURE IF EXISTS GetScheduleByHabitId;

DELIMITER //

CREATE PROCEDURE GetScheduleByHabitId(
	IN
    HabitId INT
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


-- --------------------------------------

DROP PROCEDURE IF EXISTS SaveSchedule;

DELIMITER //

CREATE PROCEDURE SaveSchedule(
	IN
    ScheduleTypeId INT,
    UserId BIGINT,
    ScheduleTime DATETIME
)
BEGIN
    INSERT INTO `ceddb`.`schedule` (`schedule_type_id`, `user_id`, `schedule_time`)
	VALUES(ScheduleTypeId, UserId, ScheduleTime);
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=(SELECT last_insert_id());
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateSchedule;

DELIMITER //

CREATE PROCEDURE UpdateSchedule(
	IN
    Id INT,
    ScheduleTypeId INT,
    UserId BIGINT,
    ScheduleTime DATETIME
)
BEGIN
    UPDATE `ceddb`.`schedule` s SET
		`schedule_type_id`= ScheduleTypeId,
        user_id = UserId,
        schedule_time = ScheduleTime
	WHERE s.idschedule = Id;
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=Id;
END //

DELIMITER ;
-- --------------------------------------