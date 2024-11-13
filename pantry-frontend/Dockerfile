# Use Node for building the Vite/React frontend
FROM node:18 AS build
WORKDIR /app

# Install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy the remaining files and build the project
COPY . .
RUN npm run build

# Use a lightweight Nginx image to serve the static files
FROM nginx:alpine
# Copy the build output to Nginx's default html directory
COPY --from=build /app/dist /usr/share/nginx/html

# Add a basic Nginx configuration file
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 80 to the outside world
EXPOSE 80
# Run Nginx in the foreground (needed for containerization)
CMD ["nginx", "-g", "daemon off;"]
