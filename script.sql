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
    
	DELETE FROM `CEDDB`.`refresh_token` re
	WHERE re.`token` = refreshToken;
    
    INSERT INTO `ceddb`.`blacklisted_token`
	(`id`, `token`, `expiry`) 
	VALUES (UUID(), appToken, appTokenExpiry);

END //

DELIMITER ;
-- End RevokeToken
-- --------------------------------------