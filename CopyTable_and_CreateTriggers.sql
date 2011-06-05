--SQL-запрос, создающий журнал транзакций для таблицы Sales.Shippers в виде SQL таблицы:
IF OBJECT_ID('Sales.ShippersReplication', 'U') IS NULL
CREATE TABLE Sales.ShippersReplication (
	id INT NOT NULL IDENTITY PRIMARY KEY,
	event_time DATETIME NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	event_type NVARCHAR(10) NOT NULL,
	login_name SYSNAME NOT NULL DEFAULT(SUSER_SNAME()),
	shipperid_old INT DEFAULT(NULL),
	companyname_old NVARCHAR(40) DEFAULT(NULL),
	phone_old NVARCHAR(24) DEFAULT(NULL),
	shipperid_new INT DEFAULT(NULL),
	companyname_new NVARCHAR(40) DEFAULT(NULL),
	phone_new NVARCHAR(24) DEFAULT(NULL)
);

/**
--SQL-запрос, создающий журнал транзакций для таблицы Sales.Shippers в виде SQL таблицы с копированием записей:
USE BasicTSQL;
GO
SELECT * INTO Sales.ShippersReplication FROM Sales.Shippers
GO
*/

--Триггер на вставку:
USE BasicTSQL;
GO
CREATE TRIGGER Sales.trg_shippers_insert ON Sales.Shippers AFTER INSERT
AS
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_new, companyname_new, phone_new)
		SELECT event_type='INSERTED', shipperid, companyname, phone
		FROM inserted;
GO

--Триггер на удаление:
USE BasicTSQL;
GO

CREATE TRIGGER Sales.trg_shippers_delete ON Sales.Shippers AFTER DELETE
AS 
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_old, companyname_old, phone_old)
	SELECT event_type = 'DELETED', shipperid, companyname, phone
	FROM deleted
GO

--Триггер на обновление:
USE BasicTSQL;
GO
CREATE TRIGGER Sales.trg_shippers_update ON Sales.Shippers AFTER UPDATE
AS 
	SET NOCOUNT ON;
	INSERT INTO Sales.ShippersReplication(event_type, shipperid_old, companyname_old, phone_old, shipperid_new, companyname_new, phone_new)
	SELECT event_type = 'UPDATED', I.shipperid, I.companyname, I.phone, D.shipperid, D.companyname, D.phone
	FROM inserted AS I, deleted AS D
GO
