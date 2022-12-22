USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_characters $$
CREATE PROCEDURE load_characters
(
	IN param_character_name VARCHAR(100)
)
BEGIN
	select * from tb_character where account = param_account;
END $$

DELIMITER ;
