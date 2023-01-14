USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_skill_by_character_name_and_id $$
CREATE PROCEDURE load_skill_by_character_name_and_id
(
	IN param_character_name VARCHAR(100),
	IN param_id bigint
)
BEGIN
	select * from tb_skill where character_name = param_character_name AND id = param_id;
END $$

DELIMITER ;
