CREATE TABLE IF NOT EXISTS site (
    site_id SERIAL PRIMARY KEY,
    address TEXT,
    description TEXT,
    latitude FLOAT,
    longitude FLOAT,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
