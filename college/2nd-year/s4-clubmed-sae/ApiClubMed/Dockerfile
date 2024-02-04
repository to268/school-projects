FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ApiClubMed

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

WORKDIR /
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 6.0.14

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ApiClubMed
COPY --from=build-env / /

EXPOSE 80/tcp

ENTRYPOINT ["/ApiClubMed/out/ApiClubMed"]
