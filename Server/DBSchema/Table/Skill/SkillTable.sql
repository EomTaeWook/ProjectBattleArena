USE GameDB;

SET collation_connection = @@collation_database;
DROP TABLE IF EXISTS tb_skill;
CREATE TABLE tb_skill (
	id bigint NOT NULL AUTO_INCREMENT,
	character_name varchar(100) not null,
	template_id int default 0,
	skill_exp int DEFAULT 0,
	create_time bigint null default 0,
	modify_time bigint null default 0,
	PRIMARY KEY (id),
	INDEX(character_name)
)
COLLATE='utf8mb4_general_ci'