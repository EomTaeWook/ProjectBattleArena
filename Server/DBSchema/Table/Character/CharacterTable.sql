USE GameDB;

SET collation_connection = @@collation_database;
SET foreign_key_checks = 0;
DROP TABLE IF EXISTS tb_character;
CREATE TABLE tb_character (
	unique_id varchar(50),
	character_name varchar(100) not null,
	account varchar(100) not null,
	template_id int default 0,
	exp int default 0,
	atk int default 0,
	con int default 0,
	dex int default 0,
	stat_point int default 0,
	
	create_time bigint null default 0,
	modify_time bigint null default 0,
	PRIMARY KEY (unique_id),
	INDEX(character_name),
	INDEX(account)
)
COLLATE='utf8mb4_general_ci';
SET foreign_key_checks = 1;
