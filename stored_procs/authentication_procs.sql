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
INSERT INTO `ceddb`.`user`
(`firstname`,`lastname`,`email`,`passwordSalt`, `locked`, `password`)
	VALUES(Firstname, Lastname, Email, Salt, false, UserHash);
    
call CreateUserDevice(
	DeviceGUID,
    DeviceModel,
    DevicePlatform,
    Manufacturer,
    (SELECT `ceddb`.`user`.`iduser` FROM `ceddb`.`user` WHERE `ceddb`.`user`.`iduser`=(SELECT last_insert_id())));
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
		u.`password`
	FROM `ceddb`.`user` u 
    WHERE u.email = Email;
END //

DELIMITER ;
-- End Get User By Email
-- --------------------------------------


-- Drop stored procedure if exists
-- Get User By Email
DROP PROCEDURE IF EXISTS GetUserRefreshTokenById;

DELIMITER //

CREATE PROCEDURE GetUserRefreshTokenById(
	IN UserId VARCHAR(256)
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
	IN UserId INT, Token VARCHAR(255), IsExpired Boolean, Expires DATETIME, Created DATETIME, Revoked DATETIME, IsRevoked Boolean, DeviceId INT
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