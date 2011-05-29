USE BasicTSQL;
GO

CREATE TRIGGER Sales.trg_shippers_delete ON Sales.Shippers AFTER DELETE
AS 
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_old, companyname_old, phone_old)
	SELECT event_type = 'DELETED', shipperid, companyname, phone
	FROM deleted
GO