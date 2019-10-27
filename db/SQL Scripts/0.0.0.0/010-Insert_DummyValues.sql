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
        'Me, myself, and I',
        'This client is for purchases that come to HQ.');


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


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         deliver_after,
                         deliver_before,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (1,
        'Purchase',
        'Order placed',
        1,
        NULL,
        NULL,
        3,
        'Home Base',
        38.889484,
        -77.035278);


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         deliver_after,
                         deliver_before,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (2,
        'Sale',
        'Order placed',
        2,
        '2000-01-01',
        '2050-12-31',
        3,
        'Random address #1',
        38.810954,
        -77.063048);


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         deliver_after,
                         deliver_before,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (3,
        'Sale',
        'Delivery scheduled',
        3,
        '2019-01-01',
        '2019-12-31',
        2,
        'Random address #2',
        38.733661,
        -77.103524);


INSERT INTO transaction (transaction_id,
                         transaction_type,
                         status,
                         associated_client_id,
                         deliver_after,
                         deliver_before,
                         priority,
                         site_name,
                         site_latitude,
                         site_longitude)
VALUES (4,
        'Sale',
        'Order completed',
        4,
        '2019-10-01',
        '2019-10-31',
        1,
        'The White House',
        38.897669,
        -77.036574);


-- Purchases
INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        1,
        10,
        100);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        2,
        4,
        5000);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        3,
        1,
        900);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        4,
        2,
        700);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        5,
        2,
        700);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (1,
        6,
        2,
        100);

-- Sales
INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (2,
        1,
        10,
        2000);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (2,
        2,
        4,
        10000);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (2,
        3,
        1,
        1000);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (3,
        4,
        2,
        750);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (3,
        5,
        2,
        850);


INSERT INTO line_item (transaction_id, product_id, quantity, price)
VALUES (3,
        6,
        2,
        950);