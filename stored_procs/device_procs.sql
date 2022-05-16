-- Drop stored procedure if exists
-- Create new user device
-- should create a new device and then select and return it
DROP PROCEDURE IF EXISTS CreateUserDevice;

DELIMITER //

CREATE PROCEDURE CreateUserDevice(
	IN
    UUID VARCHAR(100),
    Model VARCHAR(100),
    Platform VARCHAR(100),
    Manufacturer VARCHAR(100),
    UserId VARCHAR(255)
)
BEGIN
	SET @id = UUID();
    SET @userId = (SELECT `ceddb`.`user`.`iduser` FROM `ceddb`.`user` WHERE `ceddb`.`user`.`iduser`=UserId);
	INSERT INTO `ceddb`.`device`
	(`iddevice`, `model`,`platform`,`uuid`,`manufacturer`, `user_id`, `active`)
		VALUES(@id, Model, Platform, UUID, Manufacturer, @userId, true);
        
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
    UUID VARCHAR(100)
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.uuid = UUID;
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
    UUID VARCHAR(100)
)
BEGIN
    SELECT de.iddevice FROM `ceddb`.`device` de
    WHERE de.uuid = UUID;
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
    UUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = UUID);
                
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
    UUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = UUID);
                
	UPDATE `ceddb`.`device` SET `active` = true WHERE @id = `ceddb`.`device`.`iddevice`;  
    
    SELECT * FROM `ceddb`.`device` de
    WHERE de.uuid = UUID;
END //

DELIMITER ;
-- End Get Activate device
-- --------------------------------------