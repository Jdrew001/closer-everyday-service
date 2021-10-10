DROP PROCEDURE IF EXISTS GetHabitFrequencies;

DELIMITER //

CREATE PROCEDURE GetHabitFrequencies(
	IN
    HabitId INT
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