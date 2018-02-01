FROM microsoft/dotnet:2.0-sdk

ENV ASPNETCORE_URLS="http://*:5050"

EXPOSE 5000
WORKDIR /app
COPY . .
WORKDIR /app/Acerola.UI

ENTRYPOINT ["dotnet", "run"]