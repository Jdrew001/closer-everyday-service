CREATE TABLE `ceddb`.`auth_code` (
  `idauth_code` VARCHAR(255) NOT NULL,
  `code` VARCHAR(45) NOT NULL,
  `user_id` VARCHAR(255) NULL,
  PRIMARY KEY (`idauth_code`),
  UNIQUE INDEX `idauth_code_UNIQUE` (`idauth_code` ASC) VISIBLE,
  INDEX `user_id_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `user_id_fk`
    FOREIGN KEY (`user_id`)
    REFERENCES `ceddb`.`user` (`iduser`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


ALTER TABLE `ceddb`.`user` 
  ADD COLUMN `confirmed` TINYINT NOT NULL DEFAULT 0 AFTER `password`;

-- Drop stored procedure if exists
-- GetAuthCodeByUserId
DROP PROCEDURE IF EXISTS GetAuthCodeByUserId;

DELIMITER //

CREATE PROCEDURE GetAuthCodeByUserId(
	IN UserId VARCHAR(255)
)
BEGIN

  SELECT * FROM `CEDDB`.`auth_code` ac
	WHERE ac.`user_id`=UserId;
    
END //

DELIMITER ;
-- End Get AuthCode by userId

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

INSERT INTO `ceddb`.`auth_code`
(`idauth_code`, `code`,`user_id`)
	VALUES(@id, AuthCode, UserId);
    
SELECT * from `ceddb`.`auth_code` ac
WHERE ac.`idauth_code` = @id;
END //

DELIMITER ;
-- End Create auth code
-- --------------------------------------

-- Delete auth code
DROP PROCEDURE IF EXISTS DeleteAuthCode;

DELIMITER //

CREATE PROCEDURE DeleteAuthCode(
	IN
    UserId VARCHAR(255)
)
BEGIN

DELETE FROM `ceddb`.`auth_code` ac
WHERE ac.`user_id` = UserId;

SELECT * from `ceddb`.`auth_code` authcode
WHERE authcode.`user_id` = UserId;

END //

DELIMITER ;
-- End Create auth code
-- --------------------------------------
