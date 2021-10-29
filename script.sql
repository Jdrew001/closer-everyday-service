-- Script to run in deployment
-- --------------------------------------
-- Habit Friends
-- --------------------------------------
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
        u.firstname as "FirstName",
        u.lastname as "LastName",
        u.email as "Email",
        fh.ownerId
    FROM `CEDDB`.`friend_habit` fh
	JOIN User u ON fh.friendId=u.iduser
	WHERE fh.habitId = HabitId;
END //

DELIMITER ;

-- --------------------------------------
-- schedule
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetScheduleByHabitId;

DELIMITER //

CREATE PROCEDURE GetScheduleByHabitId(
	IN
    HabitId INT
)
BEGIN
    SELECT 
		s.idschedule AS "scheduleId",
        s.user_id as "userId",
        st.idschedule_type AS "idScheduleType",
        st.schedule_value AS "scheduleTypeValue",
        s.schedule_time AS "scheduleTime"
    FROM `CEDDB`.`habit` h
	JOIN `CEDDB`.`Schedule` s ON h.scheduleId=s.idschedule
    JOIN `CEDDB`.`schedule_type` st ON s.schedule_type_id=st.idschedule_type
	WHERE h.idhabit = HabitId;
END //

DELIMITER ;
-- --------------------------------------