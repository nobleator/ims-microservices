CREATE OR REPLACE FUNCTION get_candidate_deliveries(as_of TIMESTAMP)
RETURNS TABLE(line_item_id INT,
              delivery_priority INT,
              profit NUMERIC,
              weight NUMERIC,
              site_latitude FLOAT,
              site_longitude FLOAT)
AS $$
    WITH purchases_cte (product_id, avg_purchase_price) AS (
        SELECT li.product_id, AVG(li.price / li.quantity) AS avg_purchase_price
        FROM public.line_item li
        JOIN public.transaction t ON t.transaction_id = li.transaction_id
        WHERE t.transaction_type = 'Purchase'
        GROUP BY li.product_id
    )

    SELECT li.line_item_id,
        t.priority,
        (li.price / li.quantity) - pcte.avg_purchase_price AS profit,
        p.weight,
        t.site_latitude,
        t.site_longitude
    FROM public.line_item li
    JOIN public.transaction t ON t.transaction_id = li.transaction_id
    JOIN purchases_cte pcte ON pcte.product_id = li.product_id
    JOIN product p ON p.product_id = li.product_id
    WHERE NOT li.deleted
        AND NOT t.deleted
        AND t.transaction_type = 'Sale'
        AND t.deliver_after < (as_of AT TIME ZONE 'utc')
        AND t.deliver_before > (as_of AT TIME ZONE 'utc')
        AND t.status <> 'Scheduled'
    ORDER BY t.priority DESC;
$$ LANGUAGE SQL;