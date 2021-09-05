FROM nginx
RUN mkdir -p /etc/letsencrypt/live/
COPY nginx.conf /etc/nginx/nginx.conf
COPY fullchain.pem /etc/letsencrypt/live/fullchain.pem
COPY privkey.pem /etc/letsencrypt/live/privkey.pem