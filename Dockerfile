FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./KomunikatyRSO.Web/*.csproj ./KomunikatyRSO.Web/
COPY ./KomunikatyRSO.Web.Infrastructure/*.csproj ./KomunikatyRSO.Web.Infrastructure/
COPY ./KomunikatyRSO.Shared/*.csproj ./KomunikatyRSO.Shared/
RUN dotnet restore "./KomunikatyRSO.Web/KomunikatyRSO.Web.csproj"

# Copy everything else
COPY ./KomunikatyRSO.Web/. ./KomunikatyRSO.Web/
COPY ./KomunikatyRSO.Web.Infrastructure/. ./KomunikatyRSO.Web.Infrastructure/
COPY ./KomunikatyRSO.Shared/. ./KomunikatyRSO.Shared/
COPY nginx.conf.sigil .

#Build database
#WORKDIR /app/KomunikatyRSO.Web
#RUN dotnet ef database update

#Build project
WORKDIR /app
RUN dotnet publish "./KomunikatyRSO.Web/KomunikatyRSO.Web.csproj" -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
#COPY --from=build /app/KomunikatyRSO.Web/komunikatyrso-test.db ./
COPY --from=build /app/KomunikatyRSO.Web/out ./
COPY --from=build /app/nginx.conf.sigil ./
ENTRYPOINT ["dotnet", "KomunikatyRSO.Web.dll"]