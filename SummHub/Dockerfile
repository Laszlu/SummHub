FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
RUN apk add nodejs
RUN apk add npm
WORKDIR /app
COPY . ./
RUN npm --prefix SummHub install
RUN dotnet publish "SummHub/SummHub.csproj" -c Release -o output

FROM nginx:alpine
WORKDIR /user/share/nginx/html
COPY --from=build-env /app/output/wwwroot .
COPY SummHub/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80