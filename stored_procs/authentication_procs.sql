-- Drop stored procedure if exists
-- Get User By Email
DROP PROCEDURE IF EXISTS RegisterAccount;

DELIMITER //

CREATE PROCEDURE RegisterAccount(
	IN
    Firstname VARCHAR(100),
    Lastname VARCHAR(100),
    Email VARCHAR(265),
    UserHash BLOB,
    Salt BLOB,
    DeviceGUID VARCHAR(100),
    DeviceModel VARCHAR(100),
    DevicePlatform VARCHAR(100),
    Manufacturer VARCHAR(100)
)
BEGIN
SET @id = UUID();

INSERT INTO `ceddb`.`user`
(`iduser`, `firstname`,`lastname`,`email`,`passwordSalt`, `locked`, `password`)
	VALUES(@id, Firstname, Lastname, Email, Salt, false, UserHash);
    
call CreateUserDevice(
	DeviceGUID,
    DeviceModel,
    DevicePlatform,
    Manufacturer,
    (SELECT `ceddb`.`user`.`iduser` FROM `ceddb`.`user` WHERE `ceddb`.`user`.`iduser`=@id));
END //

DELIMITER ;
-- End Get User By Email
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
	IN UserId VARCHAR(255)
)
BEGIN
	SET @id = (SELECT u.iduser FROM `ceddb`.`user` u
		WHERE u.iduser=UserId);
        
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
-- Get User By Email
DROP PROCEDURE IF EXISTS GetUserRefreshTokenById;

DELIMITER //

CREATE PROCEDURE GetUserRefreshTokenById(
	IN UserId VARCHAR(255)
)
BEGIN
	SELECT re.token, re.expires, re.created, re.isExpired, re.revoked, re.deviceId
	FROM `ceddb`.`refresh_token` re 
    WHERE re.userId = UserId;
END //

DELIMITER ;
-- End Get User By Email
-- --------------------------------------


-- Drop stored procedure if exists
-- SaveRefreshToken
DROP PROCEDURE IF EXISTS SaveRefreshToken;

DELIMITER //

CREATE PROCEDURE SaveRefreshToken(
	IN UserId VARCHAR(255), Token VARCHAR(255), IsExpired Boolean, Expires DATETIME, Created DATETIME, Revoked DATETIME, IsRevoked Boolean, DeviceId VARCHAR(255)
)
BEGIN
  INSERT INTO refresh_token(`token`, `expires`, `isExpired`, `created`, `revoked`, `is_revoked`, `userId`, `deviceId`)
  VALUES(
	Token, Expires, IsExpired, Created, Revoked, IsRevoked, UserId, DeviceId
  );
  
END //

DELIMITER ;
-- End Get User By Email
-- --------------------------------------

-- Drop stored procedure if exists
-- SaveRefreshToken
DROP PROCEDURE IF EXISTS DeleteRefreshToken;

DELIMITER //

CREATE PROCEDURE DeleteRefreshToken(
	IN Token VARCHAR(255)
)
BEGIN
  
  DELETE FROM `CEDDB`.`refresh_token` re
  WHERE re.`token` = Token;
  
  SELECT * FROM `CEDDB`.`refresh_token` re
  WHERE re.`token` = Token;
  
END //

DELIMITER ;
-- End Get User By Email
-- --------------------------------------


-- Drop stored procedure if exists
-- SaveRefreshToken
DROP PROCEDURE IF EXISTS GetUserByRefreshToken;

DELIMITER //

CREATE PROCEDURE GetUserByRefreshToken(
	IN Token VARCHAR(256)
)
BEGIN

  SELECT * FROM `CEDDB`.`user` u 
  WHERE u.iduser = (SELECT userId FROM `CEDDB`.`refresh_token` rt
	WHERE rt.token = Token);
    
END //

DELIMITER ;
-- End Get User By Email

-- Drop stored procedure if exists
-- SaveRefreshToken
DROP PROCEDURE IF EXISTS GetRefreshToken;

DELIMITER //

CREATE PROCEDURE GetRefreshToken(
	IN Token VARCHAR(256)
)
BEGIN

  SELECT * FROM `CEDDB`.`refresh_token` rt
	WHERE rt.token = Token;
    
END //

DELIMITER ;
-- End Get User By Email

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
-- End Get User By Email

-- Drop stored procedure if exists
-- GetAuthCodeByEmail
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

  
-- Drop stored procedure if exists
-- Revoke token
DROP PROCEDURE IF EXISTS RevokeToken;

DELIMITER //

CREATE PROCEDURE RevokeToken(
	IN 
		appToken longtext,
        appTokenExpiry datetime,
        refreshToken VARCHAR(255)
)
BEGIN
	SET @id = UUID();
    
	DELETE FROM `CEDDB`.`refresh_token` re
	WHERE re.`token` = refreshToken;
    
    INSERT INTO `ceddb`.`blacklisted_token`
	(`id`, `token`, `expiry`) 
	VALUES (UUID(), appToken, appTokenExpiry);

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------

call RevokeToken("tplem", "2022-05-04 21:17:33", "token");

-- Drop stored procedure if exists
-- CheckForTokenInBlacklist
DROP PROCEDURE IF EXISTS CheckForTokenInBlacklist;

DELIMITER //

CREATE PROCEDURE CheckForTokenInBlacklist(
	IN 
		appToken BLOB
)
BEGIN

	select * from `blacklisted_token` bt
    where bt.token = appToken;

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------