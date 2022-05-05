CREATE TABLE `ced_dev`.`blacklisted_token` (
  `id` VARCHAR(255) NOT NULL,
  `token` blob NOT NULL,
  `expiry` DATETIME NOT NULL,
  PRIMARY KEY (`id`));

  
-- Drop stored procedure if exists
-- Revoke token
DROP PROCEDURE IF EXISTS RevokeToken;

DELIMITER //

CREATE PROCEDURE RevokeToken(
	IN 
		appToken VARCHAR(255),
        appTokenExpiry datetime,
        refreshToken VARCHAR(255)
)
BEGIN
	SET @id = UUID();
    
	DELETE FROM `ced_dev`.`refresh_token` re
	WHERE re.`token` = refreshToken;
    
    INSERT INTO `ced_dev`.`blacklisted_token`
	(`id`, `token`, `expiry`) 
	VALUES (id, appToken, appTokenExpiry);

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------

-- Drop stored procedure if exists
-- CheckForTokenInBlacklist
DROP PROCEDURE IF EXISTS CheckForTokenInBlacklist;

DELIMITER //

CREATE PROCEDURE CheckForTokenInBlacklist(
	IN 
		appToken BLOB
)
BEGIN

	select * from `blacklisted_token` bt
    where bt.token = appToken;

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------