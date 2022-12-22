USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS insert_user_asset $$
CREATE PROCEDURE insert_user_asset
(
	IN param_account VARCHAR(100),
	IN param_create_time bigint
)
BEGIN
	INSERT INTO tb_user_asset
	(
		account,
		create_time
	)
	VALUES
	(
		param_account,
		param_create_time
	);
END $$

DELIMITER ;
