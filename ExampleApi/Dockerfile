FROM microsoft/dotnet:2.1.0-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 62453
EXPOSE 44300

FROM microsoft/dotnet:2.1.300-sdk AS build
WORKDIR /src
COPY ExampleApi/ExampleApi.csproj ExampleApi/
RUN dotnet restore ExampleApi/ExampleApi.csproj
COPY . .
WORKDIR /src/ExampleApi
RUN dotnet build ExampleApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ExampleApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ExampleApi.dll"]
