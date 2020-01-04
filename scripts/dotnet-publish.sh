cd src
dotnet publish ./Actio.Api -c Release -o ./Actio.Api/bin/Docker
dotnet publish ./Actio.Services.Activities -c Release -o ./Actio.Services.Activities/bin/Docker
dotnet publish ./Actio.Services.Identity -c Release -o ./Actio.Services.Identity/bin/Docker