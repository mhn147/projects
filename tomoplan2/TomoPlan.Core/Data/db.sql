USE tomoplan;

CREATE TABLE IF NOT EXISTS users
(
    id                                  CHAR(36)     NOT NULL PRIMARY KEY,
    email                               VARCHAR(255) NOT NULL UNIQUE,
    password_hash                       VARCHAR(255) NOT NULL,
    first_name                          VARCHAR(100) NOT NULL,
    last_name                           VARCHAR(100) NOT NULL,
    email_verified                      TINYINT(1) NOT NULL DEFAULT 0,
    email_verification_token            VARCHAR(255),
    email_verification_token_expires_at DATETIME(6),
    created_at                          DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    updated_at                          DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    deleted                             TINYINT(1) NOT NULL DEFAULT 0,
    INDEX                               idx_email (email),
    INDEX                               idx_deleted (deleted)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS daily_plans
(
    id          CHAR(36) NOT NULL PRIMARY KEY,
    user_id     CHAR(36) NOT NULL,
    date        DATE     NOT NULL,
    created_at  DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    updated_at  DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    is_complete TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
    UNIQUE KEY uk_user_date (user_id, date),
    INDEX       idx_user_id (user_id),
    INDEX       idx_date (date),
    INDEX       idx_is_complete (is_complete)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS daily_tasks
(
    id            CHAR(36)     NOT NULL PRIMARY KEY,
    daily_plan_id CHAR(36)     NOT NULL,
    text          VARCHAR(500) NOT NULL,
    start_time    TIME         NOT NULL,
    end_time      TIME         NOT NULL,
    created_at    DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    updated_at    DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    is_complete   TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plans (id) ON DELETE CASCADE,
    INDEX         idx_daily_plan_id (daily_plan_id),
    INDEX         idx_is_complete (is_complete),
    INDEX         idx_created_at (created_at)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;