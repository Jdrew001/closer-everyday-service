DROP PROCEDURE IF EXISTS SaveFriendHabit;

DELIMITER //

CREATE PROCEDURE SaveFriendHabit(
	IN
    FriendId VARCHAR(255),
    HabitId VARCHAR(255),
    OwnerId VARCHAR(255)
)
BEGIN
	set @id = UUID();
    INSERT INTO `ceddb`.`friend_habit` (`idfriend_habit`, `friendId`, `habitId`, `ownerId`)
	VALUES(@id, FriendId, HabitId, OwnerId);
    
    SELECT
		fh.idfriend_habit AS "id",
        fh.friendId AS "friendId",
        u.firstname AS "FirstName",
        u.lastname AS "LastName",
        u.email AS "Email",
        fh.ownerId AS "ownerId"
	FROM `ceddb`.`friend_habit` fh
    JOIN `ceddb`.`user` u ON fh.friendId = u.iduser
    WHERE f.idfrequency = FrequencyId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS ClearFriendHabit;

DELIMITER //

CREATE PROCEDURE ClearFriendHabit(
	IN
    FriendId VARCHAR(255),
    HabitId VARCHAR(255),
    OwnerId VARCHAR(255)
)
BEGIN
	DELETE from friend_habit fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
    
    SELECT * FROM `ceddb`.`friend_habit` fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
END //

DELIMITER ;
-- --------------------------------------