USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_auth$$
CREATE PROCEDURE load_auth
(
	IN param_account VARCHAR(100)
)
BEGIN
	select * from tb_auth where account = param_account;
END $$

DELIMITER ;
