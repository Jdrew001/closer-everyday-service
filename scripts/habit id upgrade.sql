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
  ON DELETE CASCADE
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`habit_frequency` 
ADD INDEX `freq_habit_id_idx` (`freq_habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD CONSTRAINT `freq_habit_id`
  FOREIGN KEY (`freq_habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`habit_log` 
ADD INDEX `history_habit_id_idx` (`habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_log` 
ADD CONSTRAINT `history_habit_id`
  FOREIGN KEY (`habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;
  
ALTER TABLE `ceddb`.`milestone` 
ADD INDEX `milestone_habit_id_idx` (`habit_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`milestone` 
ADD CONSTRAINT `milestone_habit_id`
  FOREIGN KEY (`habit_id`)
  REFERENCES `ceddb`.`habit` (`idhabit`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;