ALTER TABLE `ceddb`.`habit_frequency` 
DROP FOREIGN KEY `freq_id`;
ALTER TABLE `ceddb`.`habit_frequency` 
DROP INDEX `freq_id_idx`;
;

ALTER TABLE `ceddb`.`frequency` 
CHANGE COLUMN `idfrequency` `idfrequency` INT NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`habit_frequency` 
CHANGE COLUMN `frequency_id` `frequency_id` INT NOT NULL;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD INDEX `freq_id_idx` (`frequency_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`habit_frequency` 
ADD CONSTRAINT `freq_id`
  FOREIGN KEY (`frequency_id`)
  REFERENCES `ceddb`.`frequency` (`idfrequency`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;