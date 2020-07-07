FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5051
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet Actio.Servies.Identity.dll