# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia só o csproj primeiro para o restore ficar em cache
COPY Paysuit.csproj .
RUN dotnet restore

# Depois copia o resto e publica
COPY . .
RUN dotnet publish Paysuit.csproj -c Release -o /app --no-restore

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Paysuit.dll"]