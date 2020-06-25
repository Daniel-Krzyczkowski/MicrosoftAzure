FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 61812
EXPOSE 44337

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AspOnDockerSample.Web/AspOnDockerSample.Web.csproj AspOnDockerSample.Web/
RUN dotnet restore AspOnDockerSample.Web/AspOnDockerSample.Web.csproj
COPY . .
WORKDIR /src/AspOnDockerSample.Web
RUN dotnet build AspOnDockerSample.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AspOnDockerSample.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspOnDockerSample.Web.dll"]
