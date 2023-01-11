USE GameDB;

SET collation_connection = @@collation_database;
DROP TABLE IF EXISTS tb_equipment_skill;
CREATE TABLE tb_equipment_skill (
	character_name varchar(100) not null,
	slot_1 int DEFAULT -1,
	slot_2 int DEFAULT -1,
	slot_3 int DEFAULT -1,
	slot_4 int DEFAULT -1,
	slot1_exp int DEFAULT 0,
	slot2_exp int DEFAULT 0,
	slot3_exp int DEFAULT 0,
	slot4_exp int DEFAULT 0,
	create_time bigint null default 0,
	modify_time bigint null default 0,
	PRIMARY KEY (character_name),
	INDEX(character_name)
)
COLLATE='utf8mb4_general_ci'