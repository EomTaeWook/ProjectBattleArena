USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS create_character $$
CREATE PROCEDURE create_character
(
	IN param_character_name VARCHAR(100),
	IN param_account VARCHAR(100),
	IN param_template_id int,
	IN param_create_time bigint
)
BEGIN
	INSERT INTO tb_character
	(
		unique_id,
		character_name,
		account,
		template_id,
		create_time
	)
	VALUES
	(
		REPLACE(UUID(),'-',''),
		param_character_name,
		param_account,
		param_template_id,
		param_create_time
	);
END $$

DELIMITER ;
