DROP PROCEDURE IF EXISTS UpdateUserPassword;

CREATE DEFINER=`dtatkison`@`%` PROCEDURE `UpdateUserPassword`(
	IN 
		UserId VARCHAR(255), 
		UserHash BLOB,
		Salt BLOB
)
BEGIN
        
	UPDATE `ced_dev`.`user` u SET
		u.`passwordSalt`= Salt,
		u.`password`= UserHash
	WHERE u.iduser = UserId;

	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`,
        u.`confirmed`
	FROM `ced_dev`.`user` u
    WHERE u.iduser = userId;

END