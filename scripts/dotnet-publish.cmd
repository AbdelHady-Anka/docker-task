cd ..
cd src
dotnet clean .\Actio.Api
dotnet publish .\Actio.Api -c Release -o .\bin\Docker
dotnet clean .\Actio.Services.Activities
dotnet publish .\Actio.Services.Activities -c Release -o .\bin\Docker
dotnet clean .\Actio.Servies.Identity
dotnet publish .\Actio.Servies.Identity -c Release -o .\bin\Docker