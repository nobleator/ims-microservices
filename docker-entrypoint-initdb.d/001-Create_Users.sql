-- User authentication/management tables
-- Used in account_service?
CREATE TABLE IF NOT EXISTS ims_user (
    ims_user_id SERIAL PRIMARY KEY,
    name TEXT,
    description TEXT,
    password_salt TEXT,
    password_hash TEXT,
    failed_attempts INTEGER,
    locked_until TIMESTAMP
);
CREATE TABLE IF NOT EXISTS ims_role (
    ims_role_id SERIAL PRIMARY KEY,
    name TEXT,
    description TEXT
);
CREATE TABLE IF NOT EXISTS ims_user_role (
    ims_user_role_id SERIAL PRIMARY KEY,
    ims_user_id INTEGER REFERENCES ims_user (ims_user_id),
    ims_role_id INTEGER REFERENCES ims_role (ims_role_id)
);
INSERT INTO ims_role (ims_role_id, name, description)
VALUES (1, 'User', 'Default user account');
INSERT INTO ims_role (ims_role_id, name, description)
VALUES (2, 'Admin', 'Phenomenal cosmic power, itty bitty living space');
