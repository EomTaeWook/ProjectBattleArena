USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS update_gacha_skill $$
CREATE PROCEDURE update_gacha_skill
(
	IN param_account VARCHAR(100),
	IN param_current_value int,
	IN param_update_value int
)
BEGIN
	UPDATE tb_user_asset SET
		gacha_skill = param_update_value
	WHERE account = param_account AND gacha_skill = param_current_value;
END $$

DELIMITER ;
