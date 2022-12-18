USE LogDB;

DROP TABLE IF EXISTS log;

CREATE TABLE log (
  id bigint NOT NULL AUTO_INCREMENT,
  account varchar(100) not null,
  path varchar(100) NOT NULL,
  log varchar(1000) NOT NULL,
  logged_time bigint NOT NULL,
  PRIMARY KEY (`id`),
  INDEX(path),
  INDEX(logged_time)
  
) COLLATE=utf8mb4_general_ci;