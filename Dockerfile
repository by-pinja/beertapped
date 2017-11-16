FROM microsoft/aspnetcore:2
WORKDIR /app
COPY api/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "api.dll"]