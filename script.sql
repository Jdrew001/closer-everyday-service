-- Drop stored procedure if exists
-- Revoke token
DROP PROCEDURE IF EXISTS RevokeToken;

DELIMITER //

CREATE PROCEDURE RevokeToken(
	IN 
		appToken longtext,
        appTokenExpiry datetime,
        refreshToken VARCHAR(255)
)
BEGIN
	SET @id = UUID();
    
	DELETE FROM `ced_dev`.`refresh_token` re
	WHERE re.`token` = refreshToken;
    
    INSERT INTO `ced_dev`.`blacklisted_token`
	(`id`, `token`, `expiry`) 
	VALUES (UUID(), appToken, appTokenExpiry);

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------