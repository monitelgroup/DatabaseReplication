USE BasicTSQL;

UPDATE Sales.NewShippers
	SET shipperid = Sales.ShippersReplication.shipperid_new,
		companyname = Sales.ShippersReplication.companyname_new,
		phone = Sales.ShippersReplication.phone_new
FROM Sales.NewShippers JOIN Sales.ShippersReplication
	ON Sales.NewShippers.shipperid = Sales.ShippersReplication.shipperid_old
WHERE Sales.ShippersReplication.id = 4;