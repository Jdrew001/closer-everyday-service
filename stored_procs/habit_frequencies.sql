DROP PROCEDURE IF EXISTS GetHabitFrequencies;

DELIMITER //

CREATE PROCEDURE GetHabitFrequencies(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT
		idfrequency,
		frequency_val as "frequency"
	FROM `ceddb`.`frequency` f
	INNER JOIN habit_frequency hf ON hf.frequency_id = f.idfrequency
    WHERE hf.freq_habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------

DROP PROCEDURE IF EXISTS SaveHabitFrequency;

DELIMITER //

CREATE PROCEDURE SaveHabitFrequency(
	IN
    HabitId VARCHAR(255),
    FrequencyId INT
)
BEGIN
	set @id = UUID();
    INSERT INTO `ceddb`.`habit_frequency` (`idhabit_frequency`, `freq_habit_id`, `frequency_id`)
	VALUES(@id, HabitId, FrequencyId);
    
    SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    WHERE f.idfrequency = FrequencyId;
END //

DELIMITER ;
-- --------------------------------------