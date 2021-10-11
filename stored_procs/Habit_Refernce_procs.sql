DROP PROCEDURE IF EXISTS GetAllHabitTypes;

DELIMITER //

CREATE PROCEDURE GetAllHabitTypes()
BEGIN
    SELECT 
		habitTypeId,
        habitTypeValue as "value",
        description
	FROM `CEDDB`.`habit_type`;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllScheduleTypes;

DELIMITER //

CREATE PROCEDURE GetAllScheduleTypes()
BEGIN
    SELECT 
		idschedule_type,
        schedule_value as "value"
	FROM `CEDDB`.`schedule_type`;
END //

DELIMITER ;
-- --------------------------------------