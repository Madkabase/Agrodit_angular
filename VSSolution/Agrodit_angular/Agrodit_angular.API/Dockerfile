FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Agrodit_angular.API/Agrodit_angular.API.csproj", "Agrodit_angular.API/"]
RUN dotnet restore "Agrodit_angular.API/Agrodit_angular.API.csproj"
COPY . .
WORKDIR "/src/Agrodit_angular.API"
RUN dotnet build "Agrodit_angular.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Agrodit_angular.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Agrodit_angular.API.dll"]
