USE ServerLog;

DROP TABLE IF EXISTS log;

CREATE TABLE log (
  id bigint NOT NULL AUTO_INCREMENT,
  level varchar(100) NOT NULL,
  logged_time datetime NOT NULL,
  message varchar(100)NOT NULL,
  callsite varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id` (`id`)
) COLLATE=utf8mb4_general_ci;