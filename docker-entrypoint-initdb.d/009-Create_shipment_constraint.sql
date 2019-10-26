CREATE TABLE IF NOT EXISTS shipment_constraint (
    shipment_constraint_id INT GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    primary_transaction_id INTEGER REFERENCES transaction (transaction_id),
    relationship INTEGER REFERENCES transaction_relationship (relationship_id),
    secondary_transaction_id INTEGER REFERENCES transaction (transaction_id),
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
