# Base image for Vite/React
FROM node:18 AS build
WORKDIR /app

# Install dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install

# Build the Vite app
COPY . .
RUN npm run build

# Use an nginx container to serve the built static files
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

# this is new
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]