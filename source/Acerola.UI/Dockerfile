FROM microsoft/aspnetcore:2.0.0-jessie
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Acerola.UI.dll"]
