-- --------------------------------------

DROP PROCEDURE IF EXISTS ClearFrequenciesForHabit;

DELIMITER //

CREATE PROCEDURE ClearFrequenciesForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	DELETE from habit_frequency hf WHERE hf.freq_habit_id = HabitId;
    
     SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    JOIN habit_frequency h ON f.idfrequency = h.frequency_id
    WHERE h.freq_habit_id=HabitId;
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

DROP PROCEDURE IF EXISTS UpdateSchedule;

DELIMITER //

CREATE PROCEDURE UpdateSchedule(
	IN
    Id VARCHAR(255),
    ScheduleTypeId INT,
    UserId VARCHAR(255),
    ScheduleTime DATETIME
)
BEGIN
    UPDATE `ceddb`.`schedule` s SET
		`schedule_type_id`= ScheduleTypeId,
        user_id = UserId,
        schedule_time = ScheduleTime
	WHERE s.idschedule = Id;
    
    SELECT
		s.idschedule as "Id",
		s.schedule_time as "ScheduleTime",
		st.idschedule_type as "idschedule_type",
		st.schedule_value as "scheduleType",
		s.user_id as "UserId"
    FROM `ceddb`.`schedule` s 
    INNER JOIN schedule_type st ON st.idschedule_type = schedule_type_id
    WHERE s.`idschedule`=Id;
END //

DELIMITER ;
-- --------------------------------------