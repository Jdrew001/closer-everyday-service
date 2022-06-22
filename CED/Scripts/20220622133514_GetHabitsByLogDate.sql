DROP PROCEDURE IF EXISTS GetHabitByLogDate;

create
    definer = dtatkison@`%` procedure GetHabitByLogDate(
        IN
            Date DATETIME,
            UserId VARCHAR(255),
            Schedule VARCHAR(255)
    )
BEGIN

    -- HABITID: habit ids from below declaration statement
    DECLARE HABITID varchar(255) DEFAULT NULL;

    -- done: this will stop the loop if no more results have been found
    DECLARE done INT DEFAULT 0;

    -- habitids: selecting all habit ids where date and user id match params
    DECLARE habitids CURSOR FOR
        SELECT habit_id FROM habit_log
             where
                 date_format(created_at, '%d.%m.%Y') = date_format(Date,'%d.%m.%Y') and
                 user_id = UserId;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    -- Create a temporary table to give the results of the overall store procedure
    DROP TEMPORARY TABLE IF EXISTS habitResults;
    CREATE TEMPORARY TABLE IF NOT EXISTS habitResults  (
        idhabit                 VARCHAR(255),
        name                    VARCHAR(45),
        icon                    BLOB,
        reminder                tinyint,
        reminderAt              datetime,
        visibleToFriends        tinyint,
        description             varchar(4000),
        status                  char,
        userId                  varchar(255),
        createdAt               datetime,
        active_ind              char,
        idSchedule              varchar(255),
        schedule_time           datetime,
        idschedule_type         int,
        scheduleType            varchar(255),
        habitTypeId             int,
        habitType               varchar(45),
        habitTypeDescription    varchar(2000),
        habitLogValue           char,
        habitLogCreatedAt       DateTime
      );

    OPEN habitids;

    habitLoop: LOOP
        FETCH habitids INTO HABITID;

        IF done THEN
            LEAVE habitLoop;
        end if;

        SELECT
            h.idhabit,
            h.name,
            h.icon,
            h.reminder,
            h.reminderAt,
            h.visibleToFriends,
            h.description,
            h.status,
            h.userId,
            h.createdAt,
            h.active_ind,
            s.idSchedule,
            s.schedule_time,
            st.idschedule_type,
            st.schedule_value as "scheduleType",
            ht.habitTypeId,
            ht.habitTypeValue as "habitType",
            ht.description as "habitTypeDescription",
            hl.log_value as "habitLogValue",
            hl.created_at as "habitLogCreatedAt"
        INTO
            @idhabit,
            @name,
            @icon,
            @reminder,
            @reminderAt,
            @visibleToFriends,
            @description,
            @status,
            @userId,
            @createdAt,
            @activeInd,
            @idSchedule,
            @scheduleTime,
            @idScheduleType,
            @scheduleType,
            @habitTypeId,
            @habitType,
            @habitTypeDescription,
            @habitLogValue,
            @habitLogCreatedAt
        FROM habit h
        JOIN Schedule s ON h.scheduleId=s.idschedule
        JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
        JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
        JOIN habit_log hl ON h.idhabit = hl.habit_id AND date_format(hl.created_at, '%d.%m.%Y') = date_format(Date,'%d.%m.%Y')
        WHERE h.idhabit = HABITID;

        INSERT INTO habitResults
        VALUES
            (@idhabit,
            @name,
            @icon,
            @reminder,
            @reminderAt,
            @visibleToFriends,
            @description,
            @status,
            @userId,
            @createdAt,
            @activeInd,
            @idSchedule,
            @scheduleTime,
            @idScheduleType,
            @scheduleType,
            @habitTypeId,
            @habitType,
            @habitTypeDescription,
            @habitLogValue,
            @habitLogCreatedAt);

    end loop;

    CLOSE habitids;

    IF UPPER(Schedule) != 'ANYTIME' THEN
        select * from habitResults hr
        where UPPER(hr.scheduleType)  = UPPER(Schedule);
    end if;

    IF UPPER(Schedule) = 'ANYTIME' THEN
        select * from habitResults;
    end if;


END;