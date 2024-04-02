# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "APINotificacionesV2.csproj"
RUN dotnet build "APINotificacionesV2.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "APINotificacionesV2.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the certificate and set permissions
COPY cert.pfx /app
RUN chmod 777 /app/cert.pfx

# Expose ports and set environment variables (la contrasñea de el certificado deveria estar en una varible de entorno en produccion)
EXPOSE 3131
EXPOSE 3130
ENV ASPNETCORE_URLS=https://+:3131;http://+:3130
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=4226
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/cert.pfx
ENV AWS_ACCESS_KEY_ID=AKIA3RQGXBWZXNWFXMWD
ENV AWS_SECRET_ACCESS_KEY=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
 
# Start the application
ENTRYPOINT ["dotnet", "APINotificacionesV2.dll"]

