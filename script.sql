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
        s.userId as "userId",
        s.schedule_time AS "scheduleTime",
        st.idschedule_type AS "idScheduleType",
        st.schedule_value AS "scheduleTypeValue"
    FROM `CEDDB`.`habit` h
	JOIN `CEDDB`.`Schedule` s ON h.scheduleId=s.idschedule
    JOIN `CEDDB`.`ScheduleType` st ON s.schedule_type_id=st.idschedule_type
	WHERE h.habitId = HabitId;
END //

DELIMITER ;
-- --------------------------------------


-- --------------------------------------

DROP PROCEDURE IF EXISTS SaveSchedule;

DELIMITER //

CREATE PROCEDURE SaveSchedule(
	IN
    ScheduleTypeId INT,
    UserId BIGINT,
    ScheduleTime DATETIME
)
BEGIN
    INSERT INTO `ceddb`.`schedule` (`schedule_type_id`, `user_id`, `schedule_time`)
	VALUES(ScheduleTypeId, UserId, ScheduleTime);
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=(SELECT last_insert_id());
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateSchedule;

DELIMITER //

CREATE PROCEDURE UpdateSchedule(
	IN
    Id INT,
    ScheduleTypeId INT,
    UserId BIGINT,
    ScheduleTime DATETIME
)
BEGIN
    UPDATE `ceddb`.`schedule` s SET
		`schedule_type_id`= ScheduleTypeId,
        user_id = UserId,
        schedule_time = ScheduleTime
	WHERE s.idschedule = Id;
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=Id;
END //

DELIMITER ;
-- --------------------------------------
-- --------------------------------------
-- --------------------------------------
-- Milestone

ALTER TABLE `ceddb`.`like` 
DROP FOREIGN KEY `like_milestone_id`;
ALTER TABLE `ceddb`.`like` 
DROP INDEX `like_milestone_id_idx`;

ALTER TABLE `ceddb`.`comment` 
DROP FOREIGN KEY `milestone_comment_id`;
ALTER TABLE `ceddb`.`comment` 
DROP INDEX `milestone_comment_id_idx`;

ALTER TABLE `ceddb`.`milestone` 
CHANGE COLUMN `idmilestone` `idmilestone` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `milestone_id` `milestone_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`comment` 
CHANGE COLUMN `milestone_id` `milestone_id` VARCHAR(255) NOT NULL;
--

ALTER TABLE `ceddb`.`like` 
ADD INDEX `like_milestone_id_idx` (`milestone_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`like` 
ADD CONSTRAINT `like_milestone_id`
  FOREIGN KEY (`milestone_id`)
  REFERENCES `ceddb`.`milestone` (`idmilestone`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`comment` 
ADD INDEX `milestone_comment_id_idx` (`milestone_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`comment` 
ADD CONSTRAINT `milestone_comment_id`
  FOREIGN KEY (`milestone_id`)
  REFERENCES `ceddb`.`milestone` (`idmilestone`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  
  -- subscription
ALTER TABLE `ceddb`.`subscription` 
CHANGE COLUMN `idsubscription` `idsubscription` VARCHAR(255) NOT NULL;
 -- ----- schedule
 
ALTER TABLE `ceddb`.`habit` 
DROP FOREIGN KEY `schedule_habit_id`;
ALTER TABLE `ceddb`.`habit` 
DROP INDEX `habit_schedule_idx`;
 
ALTER TABLE `ceddb`.`schedule` 
CHANGE COLUMN `idschedule` `idschedule` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`habit` 
CHANGE COLUMN `scheduleId` `scheduleId` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`habit` 
ADD INDEX `habit_schedule_idx` (`scheduleId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit` 
ADD CONSTRAINT `schedule_habit_id`
  FOREIGN KEY (`scheduleId`)
  REFERENCES `ceddb`.`schedule` (`idschedule`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`friend_habit` 
DROP FOREIGN KEY `friend_habit_id`;
ALTER TABLE `ceddb`.`friend_habit` 
DROP INDEX `friend_habit_id_idx`;

ALTER TABLE `ceddb`.`habit_frequency` 
DROP FOREIGN KEY `freq_habit_id`;
ALTER TABLE `ceddb`.`habit_frequency` 
DROP INDEX `freq_habit_id_idx`;

ALTER TABLE `ceddb`.`habit_log` 
DROP FOREIGN KEY `history_habit_id`;
ALTER TABLE `ceddb`.`habit_log` 
DROP INDEX `history_habit_id_idx`;

ALTER TABLE `ceddb`.`milestone` 
DROP FOREIGN KEY `milestone_habit_id`;
ALTER TABLE `ceddb`.`milestone` 
DROP INDEX `milestone_habit_id_idx`;

ALTER TABLE `ceddb`.`habit` 
CHANGE COLUMN `idhabit` `idhabit` VARCHAR(255) NOT NULL UNIQUE;
--
ALTER TABLE `ceddb`.`friend_habit` 
CHANGE COLUMN `habitId` `habitId` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`habit_frequency` 
CHANGE COLUMN `freq_habit_id` `freq_habit_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`habit_log` 
CHANGE COLUMN `habit_id` `habit_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`milestone` 
CHANGE COLUMN `habit_id` `habit_id` VARCHAR(255) NOT NULL;
--
ALTER TABLE `ceddb`.`friend_habit` 
ADD INDEX `friend_habit_id_idx` (`habitId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`friend_habit` 
ADD CONSTRAINT `friend_habit_id`
  FOREIGN KEY (`habitId`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`habit_frequency` 
ADD INDEX `freq_habit_id_idx` (`freq_habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD CONSTRAINT `freq_habit_id`
  FOREIGN KEY (`freq_habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`habit_log` 
ADD INDEX `history_habit_id_idx` (`habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_log` 
ADD CONSTRAINT `history_habit_id`
  FOREIGN KEY (`habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`milestone` 
ADD INDEX `milestone_habit_id_idx` (`habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`milestone` 
ADD CONSTRAINT `milestone_habit_id`
  FOREIGN KEY (`habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`friend_habit` 
CHANGE COLUMN `idfriend_habit` `idfriend_habit` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`habit_frequency` 
CHANGE COLUMN `idhabit_frequency` `idhabit_frequency` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`habit_log` 
CHANGE COLUMN `idhabit_log` `idhabit_log` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`historyhabit` 
CHANGE COLUMN `idhabit` `idhabit` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`historyhabit` 
CHANGE COLUMN `userId` `userId` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`historyhabit` 
CHANGE COLUMN `scheduleId` `scheduleId` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`historyhabit` 
CHANGE COLUMN `habitTypeid` `habitTypeid` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`user_friends` 
CHANGE COLUMN `iduser_friends` `iduser_friends` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`habit_frequency` 
DROP FOREIGN KEY `freq_id`;
ALTER TABLE `ceddb`.`habit_frequency` 
DROP INDEX `freq_id_idx`;
;

ALTER TABLE `ceddb`.`frequency` 
CHANGE COLUMN `idfrequency` `idfrequency` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`habit_frequency` 
CHANGE COLUMN `frequency_id` `frequency_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD INDEX `freq_id_idx` (`frequency_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD CONSTRAINT `freq_id`
  FOREIGN KEY (`frequency_id`)
  REFERENCES `ceddb`.`frequency` (`idfrequency`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
-- device and refresh token upgrade

ALTER TABLE `ceddb`.`refresh_token` 
DROP FOREIGN KEY `token_device_id`;
ALTER TABLE `ceddb`.`refresh_token` 
DROP INDEX `user_device_id_idx`;

ALTER TABLE `ceddb`.`device` 
CHANGE COLUMN `iddevice` `iddevice` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`refresh_token` 
CHANGE COLUMN `deviceId` `deviceId` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`refresh_token` 
ADD INDEX `user_device_id_idx` (`deviceId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`refresh_token` 
ADD CONSTRAINT `token_device_id`
  FOREIGN KEY (`deviceId`)
  REFERENCES `ceddb`.`device` (`iddevice`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
-- COMMENT UPDATE

ALTER TABLE `ceddb`.`like` 
DROP FOREIGN KEY `like_comment_id`;
ALTER TABLE `ceddb`.`like` 
DROP INDEX `like_comment_id_idx`;
;

ALTER TABLE `ceddb`.`comment` 
CHANGE COLUMN `idcomment` `idcomment` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `idlike` `idlike` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `comment_id` `comment_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`like` 
ADD INDEX `like_comment_id_idx` (`comment_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`like` 
ADD CONSTRAINT `like_comment_id`
  FOREIGN KEY (`comment_id`)
  REFERENCES `ceddb`.`comment` (`idcomment`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
-- drop foreign key
ALTER TABLE `ceddb`.`comment` 
DROP FOREIGN KEY `user_comment_id`;
ALTER TABLE `ceddb`.`comment` 
DROP INDEX `user_comment_id_idx`;
;


ALTER TABLE `ceddb`.`device` 
DROP FOREIGN KEY `user_device_id`;
ALTER TABLE `ceddb`.`device` 
DROP INDEX `user_device_id_idx`;
;


ALTER TABLE `ceddb`.`friend_habit` 
DROP FOREIGN KEY `friend_id`,
DROP FOREIGN KEY `owner_id`;
ALTER TABLE `ceddb`.`friend_habit` 
DROP INDEX `friendId_idx`,
DROP INDEX `owner_id_idx`;
;


ALTER TABLE `ceddb`.`habit` 
DROP FOREIGN KEY `habitUserId`;
ALTER TABLE `ceddb`.`habit` 
DROP INDEX `userId_idx`;
;


ALTER TABLE `ceddb`.`habit_log` 
DROP FOREIGN KEY `history_user_id`;
ALTER TABLE `ceddb`.`habit_log` 
DROP INDEX `history_user_id_idx`;
;


ALTER TABLE `ceddb`.`like` 
DROP FOREIGN KEY `like_user_id`;
ALTER TABLE `ceddb`.`like` 
DROP INDEX `like_user_id_idx`;
;


ALTER TABLE `ceddb`.`milestone` 
DROP FOREIGN KEY `milestone_user_id`;
ALTER TABLE `ceddb`.`milestone` 
DROP INDEX `milestone_user_id_idx`;
;


ALTER TABLE `ceddb`.`refresh_token` 
DROP FOREIGN KEY `userId`;
ALTER TABLE `ceddb`.`refresh_token` 
DROP INDEX `userId_idx`;
;


ALTER TABLE `ceddb`.`schedule` 
DROP FOREIGN KEY `schedule_user_id`;
ALTER TABLE `ceddb`.`schedule` 
DROP INDEX `schedule_user_id_idx`;
;


ALTER TABLE `ceddb`.`subscription` 
DROP FOREIGN KEY `sub_user_id`;
ALTER TABLE `ceddb`.`subscription` 
DROP INDEX `sub_user_id_idx`;
;


ALTER TABLE `ceddb`.`user_friends` 
DROP FOREIGN KEY `friend_user_id`,
DROP FOREIGN KEY `user_friend_id`;
ALTER TABLE `ceddb`.`user_friends` 
DROP INDEX `friend_user_id_idx`,
DROP INDEX `user_friend_id_idx`;
;

-- update user - iduser
ALTER TABLE `ceddb`.`user` 
CHANGE COLUMN `iduser` `iduser` VARCHAR(255) NOT NULL UNIQUE;

-- update all user id references to big int
-- comment
ALTER TABLE `ceddb`.`comment` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`device` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`friend_habit` 
CHANGE COLUMN `friendId` `friendId` VARCHAR(255) NOT NULL,
CHANGE COLUMN `ownerId` `ownerId` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`habit` 
CHANGE COLUMN `userId` `userId` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`habit_log` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`milestone` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`refresh_token` 
CHANGE COLUMN `userId` `userId` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`schedule` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`subscription` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`user_friends` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL,
CHANGE COLUMN `friend_id` `friend_id` VARCHAR(255) NOT NULL;
;


ALTER TABLE `ceddb`.`user_friends` 
CHANGE COLUMN `user_id` `user_id` VARCHAR(255) NOT NULL;

-- add foreign keys----------------------------------------------------
ALTER TABLE `ceddb`.`comment` 
ADD INDEX `user_comment_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`comment` 
ADD CONSTRAINT `user_comment_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;
;


ALTER TABLE `ceddb`.`device` 
ADD INDEX `user_device_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`device` 
ADD CONSTRAINT `user_device_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;
;

ALTER TABLE `ceddb`.`friend_habit` 
ADD INDEX `friendId_idx` (`friendId` ASC) VISIBLE,
ADD INDEX `owner_id_idx` (`ownerId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`friend_habit` 
ADD CONSTRAINT `friend_id`
  FOREIGN KEY (`friendId`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `owner_id`
  FOREIGN KEY (`ownerId`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;
;


ALTER TABLE `ceddb`.`habit` 
ADD INDEX `userId_idx` (`userId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit` 
ADD CONSTRAINT `habitUserId`
  FOREIGN KEY (`userId`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;


ALTER TABLE `ceddb`.`habit_log` 
ADD INDEX `history_user_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_log` 
ADD CONSTRAINT `history_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;



ALTER TABLE `ceddb`.`like` 
ADD INDEX `like_user_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`like` 
ADD CONSTRAINT `like_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;


ALTER TABLE `ceddb`.`milestone` 
ADD INDEX `milestone_user_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`milestone` 
ADD CONSTRAINT `milestone_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;


ALTER TABLE `ceddb`.`refresh_token` 
ADD INDEX `userId_idx` (`userId` ASC) VISIBLE;

ALTER TABLE `ceddb`.`refresh_token` 
ADD CONSTRAINT `userId`
  FOREIGN KEY (`userId`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;



ALTER TABLE `ceddb`.`schedule` 
ADD INDEX `schedule_user_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`schedule` 
ADD CONSTRAINT `schedule_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;


ALTER TABLE `ceddb`.`subscription` 
ADD INDEX `sub_user_id_idx` (`user_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`subscription` 
ADD CONSTRAINT `sub_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
;


ALTER TABLE `ceddb`.`user_friends` 
ADD INDEX `friend_user_id_idx` (`user_id` ASC) VISIBLE,
ADD INDEX `user_friend_id_idx` (`friend_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`user_friends` 
ADD CONSTRAINT `friend_user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `user_friend_id`
  FOREIGN KEY (`friend_id`)
  REFERENCES `ceddb`.`user` (`iduser`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  -- --- STORED PROCS!! ------
  -- AUTH
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
	IN UserId VARCHAR(255), Token VARCHAR(255), IsExpired Boolean, Expires DATETIME, Created DATETIME, Revoked DATETIME, IsRevoked Boolean, DeviceId INT
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

-- DEVICE --------------
-- Drop stored procedure if exists
-- Create new user device
-- should create a new device and then select and return it
DROP PROCEDURE IF EXISTS CreateUserDevice;

DELIMITER //

CREATE PROCEDURE CreateUserDevice(
	IN
    DeviceGUID VARCHAR(100),
    DeviceModel VARCHAR(100),
    DevicePlatform VARCHAR(100),
    Manufacturer VARCHAR(100),
    UserId VARCHAR(255)
)
BEGIN
	SET @id = UUID();
	INSERT INTO `ceddb`.`device`
	(`iddevice`, `model`,`platform`,`uuid`,`manufacturer`, `user_id`, `active`)
		VALUES(@id, DeviceModel, DevicePlatform, DeviceGUID, Manufacturer, UserId, true);
        
	SELECT * FROM `ceddb`.`device` d WHERE d.`iddevice`=@id;
END //

DELIMITER ;
-- End Create User Device
-- --------------------------------------

-- Drop stored procedure if exists
-- Get a device by UUID
DROP PROCEDURE IF EXISTS GetDeviceByUUID;

DELIMITER //

CREATE PROCEDURE GetDeviceByUUID(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.uuid = DeviceUUID;
END //

DELIMITER ;
-- End Get device by UUID
-- --------------------------------------

-- Drop stored procedure if exists
-- Get user device
DROP PROCEDURE IF EXISTS GetUsersDevices;

DELIMITER //

CREATE PROCEDURE GetUsersDevices(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.user_id = UserId;
END //

DELIMITER ;
-- End Get user device
-- --------------------------------------

-- Drop stored procedure if exists
-- Deactivate user device
DROP PROCEDURE IF EXISTS GetDeviceIdByUUID;

DELIMITER //

CREATE PROCEDURE GetDeviceIdByUUID(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
    SELECT de.iddevice FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END //

DELIMITER ;
-- End Get user device
-- --------------------------------------

-- Drop stored procedure if exists
-- Deactivate user device
DROP PROCEDURE IF EXISTS DeactiveateDevice;

DELIMITER //

CREATE PROCEDURE DeactiveateDevice(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = DeviceUUID);
                
	UPDATE `ceddb`.`device` SET `active` = false WHERE @id = `ceddb`.`device`.`iddevice`;  
    
    SELECT * FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END //

DELIMITER ;
-- End Get user device
-- --------------------------------------


-- Drop stored procedure if exists
-- Activate user device
DROP PROCEDURE IF EXISTS ActivateDevice;

DELIMITER //

CREATE PROCEDURE ActivateDevice(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = DeviceUUID);
                
	UPDATE `ceddb`.`device` SET `active` = true WHERE @id = `ceddb`.`device`.`iddevice`;  
    
    SELECT * FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END //

DELIMITER ;
-- End Get Activate device
-- --------------------------------------

-- HABIT FREQUENCIES ---------------
DROP PROCEDURE IF EXISTS GetHabitFrequencies;

DELIMITER //

CREATE PROCEDURE GetHabitFrequencies(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT
		idfrequency,
		frequency_val as "frequency"
	FROM `ceddb`.`frequency` f
	INNER JOIN habit_frequency hf ON hf.frequency_id = f.idfrequency
    WHERE hf.freq_habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------

-- HABIT ------------------
DROP PROCEDURE IF EXISTS UpdateHabit;

DELIMITER //

CREATE PROCEDURE UpdateHabit(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    Status char(1),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1),
    HabitId VARCHAR(255)
)
BEGIN
    UPDATE `ceddb`.`habit` h SET
		`name`= Name,
        icon = Icon,
        reminder = Reminder,
        reminderAt = ReminderAt,
        visibleToFriends = VisibleToFriends,
        description = Description,
        status = Status,
        userid = UserId,
        scheduleId = ScheduleId,
        habitTypeId = HabitTypeId,
        createdAt = CreatedAt,
        active_ind = ActiveInd
	WHERE h.idhabit = HabitId;
    
    SELECT * FROM `ceddb`.`habit` h WHERE h.`idhabit`=HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS CreateHabit;

DELIMITER //

CREATE PROCEDURE CreateHabit(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    Status char(1),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1)
)
BEGIN
	SET @id = UUID();

	INSERT INTO `ceddb`.`habit` (`idhabit`, `name`, `icon`, `reminder`, `reminderAt`, `visibleToFriends`, `description`, `status`, `userId`, `scheduleId`, `habitTypeId`, `createdAt`, `active_ind`)
	VALUES(@id, Name, Icon, Reminder, ReminderAt, VisibleToFriends, Description, Status, UserId, ScheduleId, HabitTypeId, CreatedAt, ActiveInd);
    
    SELECT * FROM `ceddb`.`habit` d WHERE d.`idhabit`=@id;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllUserHabits;

DELIMITER //

CREATE PROCEDURE GetAllUserHabits(
	IN
    UserId VARCHAR(255)
)
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.userId = UserId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllHabits;

DELIMITER //

CREATE PROCEDURE GetAllHabits()
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId;
END //

DELIMITER ;
-- --------------------------------------


DROP PROCEDURE IF EXISTS GetHabitById;

DELIMITER //

CREATE PROCEDURE GetHabitById(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
	WHERE h.idhabit = HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetHabitFriends;

DELIMITER //

CREATE PROCEDURE GetHabitFriends(
	IN
    HabitId VARCHAR(255)
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

DROP PROCEDURE IF EXISTS GetHabitLogById;

DELIMITER //

CREATE PROCEDURE GetHabitLogById(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE hl.habit_id = HabitId);

    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`idhabit_log` = @id;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetHabitLogByIdAndDate;

DELIMITER //

CREATE PROCEDURE GetHabitLogByIdAndDate(
	IN
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.`habit_id` = HabitId;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetAllLogsForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`habit_id` = HabitId
    ORDER BY hl.created_at ASC;

END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS AddHabitLog;

DELIMITER //

CREATE PROCEDURE AddHabitLog(
	IN
    HabitId VARCHAR(255),
    UserId VARCHAR(255),
    `Value` CHAR(1)
)
BEGIN
	SET @id = UUID();
    
    INSERT INTO `ceddb`.`habit_log`
		(`idhabit_log`,
        `log_value`,
		`user_id`,
		`habit_id`)
	VALUES
		(@id,
        `Value`,
		UserId,
		HabitId);
        
	SELECT 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    FROM `ceddb`.`habit_log` hl WHERE hl.`idhabit_log`=@id;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS UpdateHabitLog;

DELIMITER //

CREATE PROCEDURE UpdateHabitLog(
	IN
    `Value` CHAR(1),
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId);

	UPDATE `ceddb`.`habit_log` SET
		`log_value` = `Value`
		WHERE `idhabit_log` = @id;
        
	select 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    from `ceddb`.`habit_log` hl WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetCompletedLogsForUser;

DELIMITER //

CREATE PROCEDURE GetCompletedLogsForUser(
	IN
    UserId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.user_id= UserId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetCompletedLogsForHabit;

DELIMITER //

CREATE PROCEDURE GetCompletedLogsForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.habit_id = HabitId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END //

DELIMITER ;
-- --------------------------------------


-- REFERENCE DATA ------------------
DROP PROCEDURE IF EXISTS GetAllHabitTypes;

DELIMITER //

CREATE PROCEDURE GetAllHabitTypes()
BEGIN
    SELECT 
		habitTypeId,
        habitTypeValue as "value",
        description
	FROM `CEDDB`.`habit_type`;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetAllScheduleTypes;

DELIMITER //

CREATE PROCEDURE GetAllScheduleTypes()
BEGIN
    SELECT 
		idschedule_type,
        schedule_value as "value"
	FROM `CEDDB`.`schedule_type`;
END //

DELIMITER ;
-- --------------------------------------


-- SCHEDULE ------------------------
  
-- --------------------------------------

DROP PROCEDURE IF EXISTS GetScheduleByHabitId;

DELIMITER //

CREATE PROCEDURE GetScheduleByHabitId(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		s.idschedule AS "scheduleId",
        s.userId as "userId",
        s.schedule_time AS "scheduleTime",
        st.idschedule_type AS "idScheduleType",
        st.schedule_value AS "scheduleTypeValue"
    FROM `CEDDB`.`habit` h
	JOIN `CEDDB`.`Schedule` s ON h.scheduleId=s.idschedule
    JOIN `CEDDB`.`ScheduleType` st ON s.schedule_type_id=st.idschedule_type
	WHERE h.habitId = HabitId;
END //

DELIMITER ;
-- --------------------------------------


-- --------------------------------------

DROP PROCEDURE IF EXISTS SaveSchedule;

DELIMITER //

CREATE PROCEDURE SaveSchedule(
	IN
    ScheduleTypeId INT,
    UserId BIGINT,
    ScheduleTime DATETIME
)
BEGIN
	SET @id = UUID();
    INSERT INTO `ceddb`.`schedule` (`idschedule`, `schedule_type_id`, `user_id`, `schedule_time`)
	VALUES(@id, ScheduleTypeId, UserId, ScheduleTime);
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=@id;
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
    
    SELECT * FROM `ceddb`.`schedule` s WHERE s.`idschedule`=Id;
END //

DELIMITER ;
-- --------------------------------------


