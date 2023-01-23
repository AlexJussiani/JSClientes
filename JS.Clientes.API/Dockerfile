#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./Services/JsClientes/JS.Clientes.API/JS.Clientes.API.csproj", "Services/JsClientes/JS.Clientes.API/"]
COPY ["./Services/JsClientes/building blocks/JS.EventSourcing/JS.EventSourcing.csproj", "Services/JsClientes/building blocks/JS.EventSourcing/"]
COPY ["./Services/JsClientes/building blocks/JS.Core/JS.Core.csproj", "Services/JsClientes/building blocks/JS.Core/"]
COPY ["./Services/JsClientes/building blocks/JS.WebAPI.Core/JS.WebAPI.Core.csproj", "Services/JsClientes/building blocks/JS.WebAPI.Core/"]
COPY ["./Services/JsClientes/JS.Clientes.Domain/JS.Clientes.Domain.csproj", "Services/JsClientes/JS.Clientes.Domain/"]
COPY ["./Services/JsClientes/JS.Clientes.Infra/JS.Clientes.Infra.csproj", "Services/JsClientes/JS.Clientes.Infra/"]
RUN dotnet restore "Services/JsClientes/JS.Clientes.API/JS.Clientes.API.csproj"
COPY . .
WORKDIR "./Services/JsClientes/JS.Clientes.API"
RUN dotnet build "JS.Clientes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JS.Clientes.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JS.Clientes.API.dll"]