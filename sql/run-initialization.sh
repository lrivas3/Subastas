# Wait to be sure that SQL Server came up
# Might have to check for health
sleep 90s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P password123! -d master -i init.sql
