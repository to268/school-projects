ARG NODE_VERSION

FROM node:${NODE_VERSION:-19.3-alpine}

RUN npm install -g http-server
WORKDIR /vue_twitter
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

EXPOSE 8080/tcp
CMD [ "http-server", "dist" ]