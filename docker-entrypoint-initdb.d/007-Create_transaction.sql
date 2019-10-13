CREATE TABLE IF NOT EXISTS transaction (
    transaction_id SERIAL PRIMARY KEY,
    transaction_type TEXT,
    status TEXT,
    associated_client_id INTEGER REFERENCES client (client_id),
    associated_shipment_id INTEGER REFERENCES shipment (shipment_id),
    deliver_after TIMESTAMP,
    deliver_before TIMESTAMP,
    priority INTEGER,
    site_name TEXT DEFAULT 'DEFAULT',
    site_latitude FLOAT DEFAULT 0.0,
    site_longitude FLOAT DEFAULT 0.0,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
