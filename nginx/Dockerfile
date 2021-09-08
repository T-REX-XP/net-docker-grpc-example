FROM nginx:alpine
RUN mkdir -p /etc/letsencrypt/live/
COPY nginx/nginx.conf /etc/nginx/nginx.conf
COPY nginx/fullchain.pem /etc/letsencrypt/live/fullchain.pem
COPY nginx/privkey.pem /etc/letsencrypt/live/privkey.pem