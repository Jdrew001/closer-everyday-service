CREATE TABLE `ceddb`.`frequency_type` (
  `idfrequency_type` INT NOT NULL,
  `freq_type` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idfrequency_type`));

CREATE TABLE `ceddb`.`day` (
  `idDay` INT NOT NULL,
  `value` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idDay`));

CREATE TABLE `ceddb`.`frequency_day` (
  `frequency_dayid` VARCHAR(255) NOT NULL,
  `frequency_id` INT NOT NULL,
  `day_id` INT NOT NULL,
  PRIMARY KEY (`frequency_dayid`));

ALTER TABLE `ceddb`.`frequency` 
CHANGE COLUMN `idfrequency` `idfrequency` INT NOT NULL ;


ALTER TABLE `ceddb`.`habit_frequency` 
CHANGE COLUMN `frequency_id` `frequency_id` INT NOT NULL ;


ALTER TABLE `ceddb`.`habit_frequency` 
ADD INDEX `freq_idx` (`frequency_id` ASC) VISIBLE;
;
ALTER TABLE `ceddb`.`habit_frequency` 
ADD CONSTRAINT `freq`
  FOREIGN KEY (`frequency_id`)
  REFERENCES `ceddb`.`frequency` (`idfrequency`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


CREATE TABLE `ceddb`.`frequency_day` (
  `idfrequency_day` VARCHAR(255) NOT NULL,
  `day_id` INT NOT NULL,
  `frequency_id` INT NOT NULL,
  PRIMARY KEY (`idfrequency_day`));

ALTER TABLE `ceddb`.`frequency_day` 
ADD INDEX `frequency_day_idx` (`frequency_id` ASC) VISIBLE,
ADD INDEX `day_freq_idx` (`day_id` ASC) VISIBLE;
;
ALTER TABLE `ceddb`.`frequency_day` 
ADD CONSTRAINT `frequency_day`
  FOREIGN KEY (`frequency_id`)
  REFERENCES `ceddb`.`frequency` (`idfrequency`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `day_freq`
  FOREIGN KEY (`day_id`)
  REFERENCES `ceddb`.`day` (`idDay`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `ceddb`.`frequency` 
ADD COLUMN `frequency_type` INT NOT NULL AFTER `frequency_val`,
ADD INDEX `freq_type_idx` (`frequency_type` ASC) VISIBLE;
;
ALTER TABLE `ceddb`.`frequency` 
ADD CONSTRAINT `freq_type`
  FOREIGN KEY (`frequency_type`)
  REFERENCES `ceddb`.`frequency_type` (`idfrequency_type`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `ceddb`.`day` 
ADD COLUMN `detail` VARCHAR(45) NOT NULL AFTER `value`;


-- Inserting reference data for DAYS
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(1, 0, "Sunday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(2, 1, "Monday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(3, 2, "Tuesday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(4, 3, "Wednesday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(5, 4, "Thursday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(6, 5, "Friday");
    
INSERT INTO `ceddb`.`day`
	(`idDay`, `value`, `detail`) 
VALUES 
	(7, 6, "Saturday");


-- frequency types

INSERT INTO `ceddb`.`frequency_type`
(`idfrequency_type`, `freq_type`)
VALUES
(1, "day");

INSERT INTO `ceddb`.`frequency_type`
(`idfrequency_type`, `freq_type`)
VALUES
(2, "week");

INSERT INTO `ceddb`.`frequency_type`
(`idfrequency_type`, `freq_type`)
VALUES
(3, "month");

INSERT INTO `ceddb`.`frequency_type`
(`idfrequency_type`, `freq_type`)
VALUES
(4, "year");

-- Schedule type

INSERT INTO `ceddb`.`schedule_type`
(`idschedule_type`, `schedule_value`)
VALUES
(1, "Anytime");

INSERT INTO `ceddb`.`schedule_type`
(`idschedule_type`, `schedule_value`)
VALUES
(2, "Morning");

INSERT INTO `ceddb`.`schedule_type`
(`idschedule_type`, `schedule_value`)
VALUES
(3, "Afternoon");

INSERT INTO `ceddb`.`schedule_type`
(`idschedule_type`, `schedule_value`)
VALUES
(4, "Evening");