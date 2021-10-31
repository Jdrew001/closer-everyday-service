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