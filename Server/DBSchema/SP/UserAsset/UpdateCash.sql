USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS update_cash $$
CREATE PROCEDURE update_cash
(
	IN param_account VARCHAR(100),
	IN param_current_cash bigint,
	IN param_update_cash bigint
)
BEGIN
	UPDATE tb_user_asset SET
		cash = param_update_cash
	WHERE account = param_account AND cash = param_current_cash;
END $$

DELIMITER ;
