USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS insert_security_key $$
CREATE PROCEDURE insert_security_key
(
	IN param_private_key VARCHAR(3000),
	IN param_public_key varchar(3000),
	IN param_created_time bigint
)
BEGIN
	INSERT INTO tb_security
	(
		private_key,
		public_key,
		created_time
	)
	VALUES
	(
		param_private_key,
		param_public_key,
		param_created_time
	);
END $$

DELIMITER ;
