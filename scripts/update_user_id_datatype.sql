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
  
  
  -- user friends
ALTER TABLE `ceddb`.`user_friends` 
CHANGE COLUMN `iduser_friends` `iduser_friends` BINARY(16) NOT NULL;
;
  