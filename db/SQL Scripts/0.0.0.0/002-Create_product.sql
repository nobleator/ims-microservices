CREATE TABLE IF NOT EXISTS product (
    product_id INT GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    name TEXT,
    description TEXT,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
