FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY ./KomunikatyRSO.Web/*.csproj ./KomunikatyRSO.Web/
COPY ./KomunikatyRSO.Web.Infrastructure/*.csproj ./KomunikatyRSO.Web.Infrastructure/
COPY ./KomunikatyRSO.Shared/*.csproj ./KomunikatyRSO.Shared/
RUN dotnet restore "./KomunikatyRSO.Web/KomunikatyRSO.Web.csproj"

COPY ./KomunikatyRSO.Web/. ./KomunikatyRSO.Web/
COPY ./KomunikatyRSO.Web.Infrastructure/. ./KomunikatyRSO.Web.Infrastructure/
COPY ./KomunikatyRSO.Shared/. ./KomunikatyRSO.Shared/

#Build database
WORKDIR /app/KomunikatyRSO.Web
RUN dotnet ef database update

WORKDIR /app
RUN dotnet publish "./KomunikatyRSO.Web/KomunikatyRSO.Web.csproj" -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=build /app/KomunikatyRSO.Web/out ./
ENTRYPOINT ["dotnet", "KomunikatyRSO.Web.dll"]