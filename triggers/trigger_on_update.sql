USE BasicTSQL;
GO

CREATE TRIGGER Sales.trg_shippers_update ON Sales.Shippers AFTER UPDATE
AS 
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_old, companyname_old, phone_old, shipperid_new, companyname_new, phone_new)
	SELECT event_type = 'UPDATED', I.shipperid, I.companyname, I.phone, D.shipperid, D.companyname, D.phone
	FROM inserted AS I, deleted AS D
GO