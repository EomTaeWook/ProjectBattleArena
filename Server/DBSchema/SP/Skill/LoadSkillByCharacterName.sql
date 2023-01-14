USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_skill_by_character_name $$
CREATE PROCEDURE load_skill_by_character_name
(
	IN param_character_name VARCHAR(100)
)
BEGIN
	select * from tb_skill where character_name = param_character_name;
END $$

DELIMITER ;
