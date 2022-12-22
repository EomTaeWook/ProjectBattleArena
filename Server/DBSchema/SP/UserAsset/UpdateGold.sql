USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS update_gold $$
CREATE PROCEDURE update_gold
(
	IN param_account VARCHAR(100),
	IN param_current_gold bigint,
	IN param_update_gold bigint
)
BEGIN
	UPDATE tb_user_asset SET
		gold = param_update_gold
	WHERE account = param_account AND gold = param_current_gold;
END $$

DELIMITER ;
