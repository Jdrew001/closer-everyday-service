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


DROP PROCEDURE IF EXISTS AddFriendToUser;

DELIMITER //

CREATE PROCEDURE AddFriendToUser(
	IN 
		UserId VARCHAR(255),
		FriendId VARCHAR(255),
        CreatedAt DATETIME
)
BEGIN
	SET @id = UUID();
	
    INSERT INTO `ceddb`.`user_friends` (`iduser_friends`, `user_id`, `friend_id`, `created_at`)
	VALUES(@id, UserId, FriendId, CreatedAt);
    
    SET @id2 = UUID();
    
    INSERT INTO `ceddb`.`user_friends` (`iduser_friends`, `user_id`, `friend_id`, `created_at`)
	VALUES(@id2, FriendId, UserId, CreatedAt);
    
    SELECT
		uf.iduser_friends AS 'id',
		u.iduser AS 'userId',
        u.firstname AS 'firstName',
        u.lastname AS 'lastName',
        u.email AS 'email'
    FROM user_friends uf
    JOIN user u ON uf.friend_id=u.iduser
    WHERE uf.iduser_friends = @id;

END //

DELIMITER ;


DROP PROCEDURE IF EXISTS RemoveFriendToUser;

DELIMITER //

CREATE PROCEDURE RemoveFriendToUser(
	IN 
		UserId VARCHAR(255),
        FriendId VARCHAR(255)
)
BEGIN

	SET @id = (SELECT uf.iduser_friends FROM user_friends uf
    WHERE uf.friend_id = FriendId AND uf.user_id = UserId);
    
    SET @id2 = (SELECT uf.iduser_friends FROM user_friends uf
    WHERE uf.friend_id = UserId AND uf.user_id = FriendId);

	DELETE FROM user_friends f WHERE f.iduser_friends = @id;
    DELETE FROM user_friends f WHERE f.iduser_friends = @id2;
    
    SELECT * FROM user_friends uf
    WHERE uf.iduser_friends = UserId;

END //

DELIMITER ;


DROP PROCEDURE IF EXISTS GetUserFriends;

DELIMITER //

CREATE PROCEDURE GetUserFriends(
	IN 
	Id VARCHAR(255)
)
BEGIN
    SELECT
		uf.iduser_friends AS 'id',
		u.iduser AS 'userId',
        u.firstname AS 'firstName',
        u.lastname AS 'lastName',
        u.email AS 'email'
    FROM user_friends uf
    JOIN user u ON uf.friend_id=u.iduser
    WHERE uf.user_id = Id;

END //

DELIMITER ;