USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS insert_user_log $$
CREATE PROCEDURE insert_user_log
(
	IN param_account VARCHAR(100),
	IN param_logged_time bigint,
	IN param_log varchar(1000),
	IN param_path varchar(100)
)
BEGIN
	INSERT INTO tb_user_log
	(
		account,
		path,
		log,
		logged_time
	)
	VALUES
	(
		param_account,
		param_path,
		param_log,
		param_logged_time
	);
END $$

DELIMITER ;
