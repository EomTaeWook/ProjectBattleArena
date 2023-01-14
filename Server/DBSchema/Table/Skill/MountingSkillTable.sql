USE GameDB;

SET collation_connection = @@collation_database;
DROP TABLE IF EXISTS tb_mounting_skill;
CREATE TABLE tb_mounting_skill (
	character_name varchar(100) not null,
	slot_1 bigint DEFAULT -1,
	slot_2 bigint DEFAULT -1,
	slot_3 bigint DEFAULT -1,
	slot_4 bigint DEFAULT -1,
	slot1_exp int DEFAULT 0,
	slot2_exp int DEFAULT 0,
	slot3_exp int DEFAULT 0,
	slot4_exp int DEFAULT 0,
	create_time bigint null default 0,
	modify_time bigint null default 0,
	PRIMARY KEY (character_name),
	FOREIGN KEY (character_name) REFERENCES tb_character(character_name) ON DELETE CASCADE ON UPDATE CASCADE,
	INDEX(character_name)
)
COLLATE='utf8mb4_general_ci'