-- Create auth code
DROP PROCEDURE IF EXISTS CreateAuthCode;

DELIMITER //

CREATE PROCEDURE CreateAuthCode(
	IN
    AuthCode VARCHAR(45),
    UserId VARCHAR(255)
)
BEGIN
SET @id = UUID();

-- delete the user's auth code incase another one exists
DELETE from `ced_dev`.`auth_code` WHERE `user_id` = UserId;

INSERT INTO `ced_dev`.`auth_code`
(`idauth_code`, `code`,`user_id`)
	VALUES(@id, AuthCode, UserId);
    
SELECT * from `ced_dev`.`auth_code` ac
WHERE ac.`idauth_code` = @id;
END //

DELIMITER ;
-- End Create auth code
-- --------------------------------------