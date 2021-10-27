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
    WHERE hl.`habit_id` = HabitId
    ORDER BY hl.created_at ASC;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetCompletedLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetCompletedLogsForHabit(
	IN
    HabitId INT
)
BEGIN
	select * 
    from habit_log hl 
    where hl.habit_id = HabitId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END //

DELIMITER ;
-- --------------------------------------