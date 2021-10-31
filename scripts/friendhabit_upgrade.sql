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