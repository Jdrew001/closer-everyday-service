DROP PROCEDURE IF EXISTS SearchForUser;

DELIMITER //

CREATE PROCEDURE SearchForUser(
	IN Param VARCHAR(256)
)
BEGIN

  SELECT
	u.iduser,
    u.firstname,
    u.lastname,
    u.email
  FROM user u 
  WHERE u.firstname LIKE CONCAT('%', Param, '%') OR u.lastname LIKE CONCAT('%', Param, '%') OR u.email LIKE CONCAT('%', Param, '%');
    
END //

DELIMITER ;

DROP PROCEDURE IF EXISTS AddHabitLog;

DELIMITER //

CREATE PROCEDURE AddHabitLog(
	IN
    HabitId VARCHAR(255),
    UserId VARCHAR(255),
    `Value` CHAR(1),
    CreatedAt DATETIME
)
BEGIN
	SET @id = UUID();
    
    INSERT INTO `ceddb`.`habit_log`
		(`idhabit_log`,
        `log_value`,
		`user_id`,
		`habit_id`,
        `created_at`)
	VALUES
		(@id,
        `Value`,
		UserId,
		HabitId,
        CreatedAt);
        
	SELECT 
		hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    FROM `ceddb`.`habit_log` hl WHERE hl.`idhabit_log`=@id;
END //

DELIMITER ;
-- --------------------------------------

ALTER TABLE `ceddb`.`milestone` 
ADD COLUMN `value` VARCHAR(45) NOT NULL AFTER `created_at`;
