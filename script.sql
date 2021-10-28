-- Script to run in deployment

DROP PROCEDURE IF EXISTS GetHabitFriends;

DELIMITER //

CREATE PROCEDURE GetHabitFriends(
	IN
    HabitId INT
)
BEGIN
    SELECT 
		fh.idfriend_habit as "id",
        u.iduser as "friendId",
        fh.ownerId
    FROM `CEDDB`.`friend_habit` fh
	JOIN User u ON fh.friendId=u.iduser
	WHERE fh.habitId = HabitId;
END //

DELIMITER ;
-- --------------------------------------