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