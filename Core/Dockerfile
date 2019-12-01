FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "Core/Core.csproj"
COPY . .
WORKDIR "/src/Core"
RUN dotnet build "Core.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Core.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Core.dll"]