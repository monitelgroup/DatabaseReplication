USE BasicTSQL;

DELETE FROM NS
FROM Sales.NewShippers AS NS
	JOIN Sales.ShippersReplication AS SR
		ON NS.shipperid = SR.shipperid_old
WHERE SR.id = 5;
