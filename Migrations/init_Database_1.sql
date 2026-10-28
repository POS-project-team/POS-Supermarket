USE [master]
GO
IF DB_ID('POS_SuperMarcket') IS NOT NULL
	PRINT('Is Already Exsisit')
ELSE
	BEGIN
	CREATE DATABASE POS_SuperMarcket;
	PRINT('Database Successfully created')
	END
