USE BasicTSQL;

INSERT INTO Sales.NewShippers(shipperid, companyname, phone)
	SELECT shipperid_new, companyname_new, phone_new
	FROM Sales.ShippersReplication
	WHERE id = 1;