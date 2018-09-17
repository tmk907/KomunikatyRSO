FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.sln ./
COPY ./KomunikatyRSO.Web/*.csproj ./KomunikatyRSO.Web/
COPY ./KomunikatyRSO.Web.Infrastructure/*.csproj ./KomunikatyRSO.Web.Infrastructure/
COPY ./KomunikatyRSO.Shared/*.csproj ./KomunikatyRSO.Shared/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY --from=build /app/KomunikatyRSO.Web/out ./
ENTRYPOINT ["dotnet", "KomunikatyRSO.Web.dll"]