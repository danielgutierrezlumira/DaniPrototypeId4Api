FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY DaniPrototypeId4Api/DaniPrototypeId4Api.csproj DaniPrototypeId4Api/
RUN dotnet restore DaniPrototypeId4Api/DaniPrototypeId4Api.csproj
COPY . .
WORKDIR /src/DaniPrototypeId4Api
RUN dotnet build DaniPrototypeId4Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DaniPrototypeId4Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DaniPrototypeId4Api.dll"]
