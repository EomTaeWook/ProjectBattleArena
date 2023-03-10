USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS create_mounting_skill $$
CREATE PROCEDURE create_mounting_skill
(
	IN param_character_name VARCHAR(100),
	IN param_create_time bigint
)
BEGIN
	INSERT INTO tb_mounting_skill
	(
		character_name,
		create_time
	)
	VALUES
	(
		param_character_name,
		param_create_time
	);
END $$

DELIMITER ;
