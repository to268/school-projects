ARG NODE_VERSION

FROM node:${NODE_VERSION:-22-alpine} AS builder

WORKDIR /frontend
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.27-alpine

WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=builder /frontend/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /frontend/dist .

EXPOSE 80/tcp
ENTRYPOINT ["nginx", "-g", "daemon off;"]
