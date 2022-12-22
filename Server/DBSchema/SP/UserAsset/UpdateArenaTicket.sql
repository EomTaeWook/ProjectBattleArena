USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS update_arena_ticket $$
CREATE PROCEDURE update_arena_ticket
(
	IN param_account VARCHAR(100),
	IN param_current_ticket bigint,
	IN param_update_ticket bigint,
	IN param_latest_update_time bigint
)
BEGIN
	UPDATE tb_user_asset SET
		arena_ticket = param_update_ticket,
		arean_ticket_latest_time = param_latest_update_time 
	WHERE account = param_account AND arena_ticket = param_current_ticket;
END $$

DELIMITER ;
