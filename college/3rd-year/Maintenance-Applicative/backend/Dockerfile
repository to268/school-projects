ARG NODE_VERSION

FROM node:${NODE_VERSION:-22-alpine}

WORKDIR /backend
COPY . .
RUN npm install

EXPOSE 5000/tcp
CMD [ "node", "server.js" ]
