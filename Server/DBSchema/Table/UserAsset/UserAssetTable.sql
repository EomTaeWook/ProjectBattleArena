USE GameDB;

DROP TABLE IF EXISTS tb_user_asset;
CREATE TABLE tb_user_asset (
  account varchar(100),
  gold int default 0,
  cash int default 0,
  arena_ticket int,
  arean_ticket_latest_time bigint,
  create_time bigint,
  PRIMARY KEY (account)
) COLLATE=utf8mb4_general_ci;