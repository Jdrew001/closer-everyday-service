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