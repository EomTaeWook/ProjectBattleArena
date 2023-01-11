USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS equipment_skill $$
CREATE PROCEDURE equipment_skill
(
	IN param_character_name VARCHAR(100),
	IN param_current_template_id int,
	IN param_update_template_id int,
	IN param_slot_index VARCHAR(100),
	IN param_update_time bigint
)
BEGIN
	UPDATE tb_equipment_skill SET
		param_slot_index = param_update_template_id,
		modify_time = param_update_time
	WHERE character_name = 	param_character_name and param_slot_index = param_current_template_id;
END $$

DELIMITER ;
