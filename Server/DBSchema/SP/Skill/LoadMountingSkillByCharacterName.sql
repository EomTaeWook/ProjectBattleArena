USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_mounting_skill_by_character_name $$
CREATE PROCEDURE load_mounting_skill_by_character_name
(
	IN param_character_name VARCHAR(100)
)
BEGIN
	SELECT * FROM tb_mounting_skill WHERE param_character_name = param_character_name;
END $$

DELIMITER ;
