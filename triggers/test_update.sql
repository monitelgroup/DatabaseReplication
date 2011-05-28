USE BasicTSQL;
GO

UPDATE Sales.Shippers
	SET companyname = 'name_updated', phone = 'phone_updated'
	WHERE shipperid = 4