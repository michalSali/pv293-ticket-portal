docker build -t ticket-portal-image ..
docker run -d -p 8080:80 --name ticket-portal-container ticket-portal-image
