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
DROP PROCEDURE IF EXISTS GetAuthCodeByEmail;

DELIMITER //

CREATE PROCEDURE GetAuthCodeByEmail(
	IN Email VARCHAR(255)
)
BEGIN
	set @id = (select u.iduser from user u where u.email=Email);

	SELECT * FROM `CEDDB`.`auth_code` ac
	WHERE ac.`user_id`=@id;
    
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
    Email VARCHAR(255)
)
BEGIN

set @id = (select u.iduser from user u where u.email=Email);

DELETE FROM `ceddb`.`auth_code` ac
WHERE ac.`user_id` = @id;

SELECT * from `ceddb`.`auth_code` authcode
WHERE authcode.`user_id` = @id;

END //

DELIMITER ;
-- End Delete Auth Code
-- --------------------------------------

-- Drop stored procedure if exists
-- Get User By Email
DROP PROCEDURE IF EXISTS GetUserByEmail;

DELIMITER //

CREATE PROCEDURE GetUserByEmail(
	IN Email VARCHAR(256)
)
BEGIN
	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`,
        u.`confirmed`
	FROM `ceddb`.`user` u 
    WHERE u.email = Email;
END //

DELIMITER ;
-- End Get User By Email
-- --------------------------------------

-- Drop stored procedure if exists
-- Confirm new user
DROP PROCEDURE IF EXISTS ConfirmNewUser;

DELIMITER //

CREATE PROCEDURE ConfirmNewUser(
	IN Email VARCHAR(255)
)
BEGIN
	SET @id = (SELECT u.iduser FROM `ceddb`.`user` u
		WHERE u.email=Email);
        
	UPDATE `ceddb`.`user` u SET
		u.`confirmed`= true
	WHERE u.iduser = @id;

	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`,
        u.`confirmed`
	FROM `ceddb`.`user` u
    WHERE u.iduser = @id;
END //

DELIMITER ;
-- End Confirm User
-- --------------------------------------

-- Drop stored procedure if exists
-- UpdateUserPassword
DROP PROCEDURE IF EXISTS UpdateUserPassword;

DELIMITER //

CREATE PROCEDURE UpdateUserPassword(
	IN 
		UserId VARCHAR(255), 
		UserHash BLOB,
		Salt BLOB
)
BEGIN
        
	UPDATE `ceddb`.`user` u SET
		u.`passwordSalt`= Salt,
		u.`password`= UserHash
	WHERE u.iduser = UserId;

	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`,
        u.`confirmed`
	FROM `ceddb`.`user` u
    WHERE u.iduser = userId;

END //

DELIMITER ;
-- End UpdateUserPassword
-- --------------------------------------

-- Drop stored procedure if exists
-- GetUserById
DROP PROCEDURE IF EXISTS GetUserById;

DELIMITER //

CREATE PROCEDURE GetUserById(
	IN 
		UserId VARCHAR(255)
)
BEGIN

	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`,
        u.`confirmed`
	FROM `ceddb`.`user` u
    WHERE u.iduser = userId;

END //

DELIMITER ;
-- End GetUserById
-- --------------------------------------
