CREATE TABLE IF NOT EXISTS shipment (
    shipment_id SERIAL PRIMARY KEY,
    route JSON,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
