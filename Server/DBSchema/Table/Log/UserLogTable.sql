USE GameDB;

DROP TABLE IF EXISTS tb_user_log;
CREATE TABLE tb_user_log (
  id bigint NOT NULL AUTO_INCREMENT,
  account varchar(100) not null,
  path varchar(100) NOT NULL,
  log varchar(1000) NOT NULL,
  logged_time bigint NOT NULL,
  PRIMARY KEY (`id`),
  INDEX(path),
  INDEX(logged_time)
  
) COLLATE=utf8mb4_general_ci;