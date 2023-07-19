FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DeliveryBot/DeliveryBot.csproj", "DeliveryBot/"]
COPY ["Delivery.Data/Delivery.Data.csproj", "Delivery.Data/"]
RUN dotnet restore "DeliveryBot/DeliveryBot.csproj"
COPY . .
WORKDIR "/src/DeliveryBot"
RUN dotnet build "DeliveryBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryBot.dll"]
