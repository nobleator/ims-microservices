CREATE TABLE IF NOT EXISTS line_item (
    line_item_id SERIAL PRIMARY KEY,
    transaction_id INTEGER REFERENCES transaction (transaction_id),
    product_id INTEGER REFERENCES product (product_id),
    quantity NUMERIC,
    price NUMERIC,
    updated_on TIMESTAMP DEFAULT NOW(),
    updated_by TEXT DEFAULT 'SYSTEM',
    created_on TIMESTAMP DEFAULT NOW(),
    created_by TEXT DEFAULT 'SYSTEM',
    deleted BOOLEAN DEFAULT FALSE
);
