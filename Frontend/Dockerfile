FROM node:14.16.0 as build

WORKDIR /src
COPY . .
RUN npm install -g @angular/cli
RUN npm install
RUN ng build --prod 

FROM nginx:1.17.8 as publish
COPY ./nginx/default.conf /etc/nginx/conf.d/
COPY --from=build /src/dist/lavyn /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]