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
 
  
  
  