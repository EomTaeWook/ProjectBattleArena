USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS insert_skill $$
CREATE PROCEDURE insert_skill
(
	IN param_character_name VARCHAR(100),
	IN param_template_id int,
	IN param_create_time bigint,
	OUT param_new_id bigint
)
BEGIN
	INSERT INTO tb_skill
	(
		character_name,
		template_id,
		create_time
	)
	VALUES
	(
		param_character_name,
		param_template_id,
		param_create_time
	);

	SET param_new_id:= last_insert_id();

END $$

DELIMITER ;
