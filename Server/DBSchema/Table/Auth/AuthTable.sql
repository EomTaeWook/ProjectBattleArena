USE Game;

SET collation_connection = @@collation_database;
DROP TABLE IF EXISTS auth;
CREATE TABLE auth (
	account varchar(100) not null,
	password varchar(100) not null,
	account_type int not null default 0,
	created_time bigint not null,
	modify_time bigint null default 0,
	PRIMARY KEY (account),
	INDEX(account)
)
COLLATE='utf8mb4_general_ci'