USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS create_character $$
CREATE PROCEDURE create_character
(
	IN param_character_name VARCHAR(100),
	IN param_account VARCHAR(100),
	IN param_job int,
	IN param_create_time bigint
)
BEGIN
	INSERT INTO tb_character
	(
		character_name,
		account,
		job,
		create_time
	)
	VALUES
	(
		param_character_name,
		param_account,
		param_job,
		param_create_time
	);
END $$

DELIMITER ;
