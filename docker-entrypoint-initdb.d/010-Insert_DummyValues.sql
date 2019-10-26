INSERT INTO product (product_id, name, description)
VALUES (1,
        'Widget #1',
        'The quick brown fox jumps over the lazy dog');


INSERT INTO product (product_id, name, description)
VALUES (2,
        'Widget #2',
        'The quick brown fox jumps over the lazy dog');


INSERT INTO product (product_id, name, description, created_by)
VALUES (3,
        '1st thingamabob',
        'The quick brown fox jumps over the lazy dog',
        'Dave');


INSERT INTO product (product_id, name, description, created_by)
VALUES (4,
        '2nd thingamabob',
        'The quick brown fox jumps over the lazy dog',
        'Dave');


INSERT INTO product (product_id, name, description, created_by)
VALUES (5,
        '3rd thingamabob',
        'The quick brown fox jumps over the lazy dog',
        'Dave');


INSERT INTO product (product_id, name, created_by)
VALUES (6,
        '100 lb basis weight thick and shiny paper',
        'Chandler');


INSERT INTO product (product_id, name, created_by)
VALUES (7,
        '20 lb basis weight thin and shiny paper',
        'Chandler');


INSERT INTO product (product_id, name, created_by)
VALUES (8,
        '50 lb basis weight smooth lightweight paper',
        'Chandler');


INSERT INTO product (product_id, name, created_by)
VALUES (9,
        '60 lb basis weight smooth lightweight paper',
        'Betty');


INSERT INTO product (product_id, name, created_by)
VALUES (10,
        '70 lb basis weight smooth lightweight paper',
        'Betty');


INSERT INTO product (product_id, name, created_by)
VALUES (11,
        '80 lb basis weight smooth lightweight paper',
        'Betty');


INSERT INTO product (product_id, name, created_by)
VALUES (12,
        '50 lb basis weight rough vellum finish lightweight paper',
        'Betty');


INSERT INTO product (product_id, name, created_by)
VALUES (13,
        '60 lb basis weight rough vellum finish lightweight paper',
        'Betty');


INSERT INTO product (product_id, name, created_by)
VALUES (14,
        '70 lb basis weight rough vellum finish lightweight paper',
        'Delaney');


INSERT INTO product (product_id, name, created_by)
VALUES (15,
        '80 lb basis weight rough vellum finish lightweight paper',
        'Delaney');


INSERT INTO client (client_id, name, description)
VALUES (1,
        'Doug Dimmadome',
        'Owner of the Dimmsdale Dimmadome');


INSERT INTO client (client_id, name, description)
VALUES (2,
        'Homer Simpson',
        'Nuclear technician');


INSERT INTO client (client_id, name, description)
VALUES (3,
        'Bob Vance',
        'Vance Refrigeration');


INSERT INTO client (client_id, name, description)
VALUES (4,
        'Jan Levinson',
        'White Pages');


INSERT INTO client (client_id, name, description)
VALUES (5,
        'John Rammel',
        'Prestige Postal Company');


INSERT INTO client (client_id, name, description)
VALUES (6,
        'Gina Rogers',
        'Apex Technology');

-- INSERT INTO site (site_id, address, description, latitude, longitude)
-- VALUES (1, '18 W Maple Street', 'Random address #1', 38.810954, -77.063048);
-- INSERT INTO site (site_id, address, description, latitude, longitude)
-- VALUES (2, '4116 Main Street', 'Random address #2', 38.733661, -77.103524);
-- INSERT INTO site (site_id, address, description, latitude, longitude)
-- VALUES (3, '1600 Pennsylvania Avenue', 'The White House', 38.897669, -77.036574);
-- INSERT INTO site (site_id, address, description, latitude, longitude)
-- VALUES (4, '2 15th St NW, Washington, DC 20024', 'Washington Monument', 38.889484, -77.035278);

INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (1,
        'Purchase',
        'Order placed',
        1,
        3,
        'Random address #1',
        38.810954,
        -77.063048);


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (2,
        'Purchase',
        'Delivery scheduled',
        2,
        2,
        'Random address #2',
        38.733661,
        -77.103524);


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (3,
        'Purchase',
        'Order completed',
        3,
        1,
        'The White House',
        38.897669,
        -77.036574);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (1,
        1,
        1,
        8,
        4000);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (2,
        1,
        2,
        4,
        10000);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (3,
        2,
        3,
        1,
        1000);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (4,
        3,
        4,
        2,
        750);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (5,
        3,
        5,
        2,
        850);


INSERT INTO line_item (line_item_id, transaction_id, product_id, quantity, price)
VALUES (6,
        3,
        6,
        2,
        950);

