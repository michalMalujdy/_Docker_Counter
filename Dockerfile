FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Counter.Web/Counter.Web.csproj", "Counter.Web/"]
RUN dotnet restore "Counter.Web/Counter.Web.csproj"
COPY . .
WORKDIR "/src/Counter.Web"
RUN dotnet build "Counter.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Counter.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Counter.Web.dll"]
