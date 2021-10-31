-- COMMENT UPDATE

ALTER TABLE `ceddb`.`like` 
DROP FOREIGN KEY `like_comment_id`;
ALTER TABLE `ceddb`.`like` 
DROP INDEX `like_comment_id_idx`;
;

ALTER TABLE `ceddb`.`comment` 
CHANGE COLUMN `idcomment` `idcomment` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `idlike` `idlike` VARCHAR(255) NOT NULL UNIQUE;

ALTER TABLE `ceddb`.`like` 
CHANGE COLUMN `comment_id` `comment_id` VARCHAR(255) NOT NULL;

ALTER TABLE `ceddb`.`like` 
ADD INDEX `like_comment_id_idx` (`comment_id` ASC) VISIBLE;

ALTER TABLE `ceddb`.`like` 
ADD CONSTRAINT `like_comment_id`
  FOREIGN KEY (`comment_id`)
  REFERENCES `ceddb`.`comment` (`idcomment`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;