FROM nginx:mainline

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY ./exampleApp/ /usr/share/nginx/html