sqlcmd /S %COMPUTERNAME%\SQLEXPRESS /i RemoveDatabase.sql
sqlcmd /S %COMPUTERNAME%\SQLEXPRESS /i CreateDatabase.sql