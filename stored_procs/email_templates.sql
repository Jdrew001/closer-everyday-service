-- Drop stored procedure if exists
-- GetTemplateByKey
DROP PROCEDURE IF EXISTS GetTemplateByKey;

DELIMITER //

CREATE PROCEDURE GetTemplateByKey(
	IN
    `Key` VARCHAR(255)
)
BEGIN
	SELECT t.templateId FROM `ceddb`.`email_template` t
    WHERE t.`key` = `Key`;
END //

DELIMITER ;
-- End GetTemplateByKey
-- --------------------------------------