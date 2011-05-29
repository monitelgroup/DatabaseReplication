USE BasicTSQL;

SELECT object_id FROM sys.objects
WHERE type = 'U' AND schema_id = 7 AND name = 'Shippers'