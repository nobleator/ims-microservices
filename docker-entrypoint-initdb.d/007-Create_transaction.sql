CREATE TABLE IF NOT EXISTS transaction (
    transaction_id SERIAL PRIMARY KEY,
    transaction_type TEXT,
    status TEXT,
    associated_site_id INTEGER REFERENCES site (site_id),
    associated_client_id INTEGER REFERENCES client (client_id),
    associated_shipment_id INTEGER REFERENCES shipment (shipment_id),
    deliver_after DATETIME,
    deliver_before DATETIME,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
