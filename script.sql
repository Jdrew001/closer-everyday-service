DROP PROCEDURE IF EXISTS GetAvgSuccessLogsForUser;

DELIMITER //

CREATE PROCEDURE GetAvgSuccessLogsForUser(
	IN
    UserId INT
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
    UserId INT
)
BEGIN
	SELECT * FROM `ceddb`.`habit_log` hl
    WHERE hl.user_id = UserId ORDER BY hl.created_at ASC;
END //

DELIMITER ;
-- --------------------------------------