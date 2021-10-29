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