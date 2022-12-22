USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS load_latest_security_key $$
CREATE PROCEDURE load_latest_security_key
(
)
BEGIN
	select * from tb_security ORDER BY id DESC LIMIT 1;
END $$

DELIMITER ;
