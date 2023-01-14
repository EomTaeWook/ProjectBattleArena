USE GameDB;

DELIMITER $$
DROP PROCEDURE IF EXISTS update_mounting_skill $$
CREATE PROCEDURE update_mounting_skill
(
	IN param_character_name VARCHAR(100),
	IN param_current_skill_id bigint,
	IN param_update_skill_id bigint,
	IN param_slot_index int,
	IN param_update_time bigint
)
BEGIN
	IF param_slot_index = 1 THEN
		UPDATE tb_mounting_skill SET
			slot_1 = param_update_skill_id,
			modify_time = param_update_time
		WHERE character_name = 	param_character_name and param_slot_index = param_current_skill_id;	
	ELSE IF param_slot_index = 2 THEN
		UPDATE tb_mounting_skill SET
			slot_2 = param_update_skill_id,
			modify_time = param_update_time
		WHERE character_name = 	param_character_name and param_slot_index = param_current_skill_id;	
	ELSE IF param_slot_index = 3 THEN
		UPDATE tb_mounting_skill SET
			slot_3 = param_update_skill_id,
			modify_time = param_update_time
		WHERE character_name = 	param_character_name and param_slot_index = param_current_skill_id;	
	ELSE IF param_slot_index = 4 THEN
		UPDATE tb_mounting_skill SET
			slot_4 = param_update_skill_id,
			modify_time = param_update_time
		WHERE character_name = 	param_character_name and param_slot_index = param_current_skill_id;	
	ELSE 
	END IF;
	
END $$

DELIMITER ;
