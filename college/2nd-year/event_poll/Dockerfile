FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /EventPollBackend/EventPoll

COPY /EventPollBackend/EventPoll ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /EventPollBackend/EventPoll
COPY --from=build-env / /

EXPOSE 80/tcp

ENTRYPOINT ["/EventPollBackend/EventPoll/out/EventPoll"]
