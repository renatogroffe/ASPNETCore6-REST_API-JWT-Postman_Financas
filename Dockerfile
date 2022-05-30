FROM mcr.microsoft.com/dotnet/sdk:6.0.300 AS build-env
WORKDIR /app

# Copiar arquivos .csproj e restaurar dependencias
COPY APIs.Security.JWT/APIs.Security.JWT.csproj ./APIs.Security.JWT/
COPY Calculos.Common/Calculos.Common.csproj ./Calculos.Common/
COPY APIFinancas/APIFinancas.csproj ./APIFinancas/
RUN dotnet restore APIFinancas/APIFinancas.csproj

# Build da aplicacao
COPY . ./
RUN dotnet publish APIFinancas/APIFinancas.csproj -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:6.0.5
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "APIFinancas.dll"]