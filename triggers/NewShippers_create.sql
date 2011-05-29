USE BasicTSQL;

CREATE TABLE Sales.NewShippers (
	shipperid INT NOT NULL PRIMARY KEY,
	companyname NVARCHAR(40) NOT NULL,
	phone NVARCHAR(24) NOT NULL
)