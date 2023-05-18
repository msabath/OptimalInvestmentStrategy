FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OptimalInvestStrategy/OptimalInvestStrategy.csproj", "OptimalInvestStrategy/"]
RUN dotnet restore "OptimalInvestStrategy/OptimalInvestStrategy.csproj"
COPY . .
WORKDIR "/src/OptimalInvestStrategy"
RUN dotnet build "OptimalInvestStrategy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OptimalInvestStrategy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OptimalInvestStrategy.dll"]
