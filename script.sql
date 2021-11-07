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

-- ca4616c6-3b8b-11ec-856e-e86a64f2b202
-- select * from user where iduser = '09f43914-3aca-11ec-856e-e86a64f2b202';
-- select * from user_friends;
-- call AddFriendToUser('09f43914-3aca-11ec-856e-e86a64f2b202', '5acc803e-3a9e-11ec-856e-e86a64f2b202', '2021-10-08 22:59:28');
-- call AddFriendToUser('09f43914-3aca-11ec-856e-e86a64f2b202', 'ca4616c6-3b8b-11ec-856e-e86a64f2b202', '2021-10-08 22:59:28');
-- call AddFriendToUser('ca4616c6-3b8b-11ec-856e-e86a64f2b202', '5acc803e-3a9e-11ec-856e-e86a64f2b202', '2021-10-08 22:59:28');
-- call RemoveFriendToUser('09f43914-3aca-11ec-856e-e86a64f2b202', 'ca4616c6-3b8b-11ec-856e-e86a64f2b202');
-- call GetUserFriends('5acc803e-3a9e-11ec-856e-e86a64f2b202');

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