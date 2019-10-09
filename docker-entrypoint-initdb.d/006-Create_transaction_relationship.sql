-- Does not include meta columns because this should not be user editable?
CREATE TABLE IF NOT EXISTS transaction_relationship (
    relationship_id SERIAL PRIMARY KEY,
    description TEXT
);
