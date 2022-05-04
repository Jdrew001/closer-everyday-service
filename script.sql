-- CREATE TABLE `ced_dev`.`email_template` (
--   `id` INT NOT NULL,
--   `key` VARCHAR(45) NOT NULL,
--   `templateId` VARCHAR(255) NOT NULL,
--   PRIMARY KEY (`id`));
  
-- ALTER TABLE `ced_dev`.`email_template` 
-- CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT ;



-- -- Drop stored procedure if exists
-- -- GetTemplateByKey
-- DROP PROCEDURE IF EXISTS GetTemplateByKey;

-- DELIMITER //

-- CREATE PROCEDURE GetTemplateByKey(
-- 	IN
--     `Key` VARCHAR(255)
-- )
-- BEGIN
-- 	SELECT t.templateId FROM `ced_dev`.`email_template` t
--     WHERE t.`key` = `Key`;
-- END //

-- DELIMITER ;
-- -- End GetTemplateByKey
-- -- --------------------------------------

-- INSERT INTO `ced_dev`.`email_template`
-- (`key`,
-- `templateId`)
-- VALUES
-- ("VALIDATION_EMAIL",
-- "d-d336182397714541874da8d70a480139");
