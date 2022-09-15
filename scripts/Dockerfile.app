FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app
COPY publish /app

ENTRYPOINT ["dotnet", "PJ.Example.API.dll"]
