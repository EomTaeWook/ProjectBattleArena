USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_character_by_unique_id $$
CREATE PROCEDURE load_character_by_unique_id
(
	IN param_unique_id VARCHAR(50)
)
BEGIN
	select * from tb_character where unique_id = param_unique_id;
END $$

DELIMITER ;
