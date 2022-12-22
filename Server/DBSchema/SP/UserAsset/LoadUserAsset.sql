USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_user_asset $$
CREATE PROCEDURE load_user_asset
(
	IN param_account VARCHAR(100)
)
BEGIN
	
	SELECT * FROM tb_user_asset WHERE account = param_account;

END $$

DELIMITER ;
