CREATE TABLE `ceddb`.`email_template` (
  `id` INT NOT NULL,
  `key` VARCHAR(45) NOT NULL,
  `templateId` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`id`));
  
ALTER TABLE `ceddb`.`email_template` 
CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT ;

