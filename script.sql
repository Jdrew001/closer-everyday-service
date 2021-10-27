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