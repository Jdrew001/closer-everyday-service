-- --------------------------------------

DROP PROCEDURE IF EXISTS ClearFrequenciesForHabit;

DELIMITER //

CREATE PROCEDURE ClearFrequenciesForHabit(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	DELETE from habit_frequency hf WHERE hf.freq_habit_id = HabitId;
    
     SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    JOIN habit_frequency h ON f.idfrequency = h.frequency_id
    WHERE h.freq_habit_id=HabitId;
END //

DELIMITER ;
-- --------------------------------------