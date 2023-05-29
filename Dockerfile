FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["src/QueryCEP.API/QueryCEP.API.csproj", "src/QueryCEP.API/"]

RUN dotnet restore "src/QueryCEP.API/QueryCEP.API.csproj"
COPY ./src ./src
WORKDIR "/src/QueryCEP.API"
RUN dotnet publish "QueryCEP.API.csproj" -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "QueryCEP.API.dll"]
