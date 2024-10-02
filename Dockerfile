FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SelectionProcessAdministration/SelectionProcessAdministration.csproj", "SelectionProcessAdministration/"]
RUN dotnet restore "SelectionProcessAdministration/SelectionProcessAdministration.csproj"
COPY . .
WORKDIR "/src/SelectionProcessAdministration"
RUN dotnet build "SelectionProcessAdministration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelectionProcessAdministration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SelectionProcessAdministration.dll"]