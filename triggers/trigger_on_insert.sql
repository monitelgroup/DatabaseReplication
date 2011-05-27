USE BasicTSQL;
GO
CREATE TRIGGER Sales.trg_shippers_insert ON Sales.Shippers AFTER INSERT
AS
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_new, companyname_new, phone_new)
		SELECT event_type='INSERTED', shipperid, companyname, phone
		FROM inserted;
GO