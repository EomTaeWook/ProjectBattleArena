USE GameDB;

SET collation_connection = @@collation_database;
DROP TABLE IF EXISTS tb_security;
CREATE TABLE tb_security (
	id bigint NOT NULL AUTO_INCREMENT,
	private_key varchar(200) not null,
	public_key varchar(200) not null,
	created_time bigint not NULL,
	PRIMARY KEY (id),
	INDEX(id)
)
COLLATE='utf8mb4_general_ci'