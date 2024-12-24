## Game Store API

## Starting SQL Server
'''powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=sa_password" -e "MSSQL_PID=Evaluation" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql --hostname mssql mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
'''

## setting the connection string to secate manager
'''powershell
$sa_password = "[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings: GameStoreContex" "Server=DESKTOP-V4PAD7G\SQLEXPRESS; Database=GameStore;User ID=sa;Password=$sa_password;TrustServerCertificate=True"


'''