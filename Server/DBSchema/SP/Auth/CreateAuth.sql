USE Game;

DELIMITER $$
DROP PROCEDURE IF EXISTS create_auth$$
CREATE PROCEDURE create_auth
(
	IN param_account VARCHAR(100),
	IN param_password varchar(100),
	IN param_created_time bigint
)
BEGIN
	INSERT INTO auth
	(
		account,
		password,
		created_time
	)
	VALUES
	(
		param_account,
		param_password,
		param_created_time
	);
END $$

DELIMITER ;
