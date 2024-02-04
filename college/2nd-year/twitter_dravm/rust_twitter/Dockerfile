FROM rust:latest AS build-env
WORKDIR /rust_twitter

COPY . .

ENV CARGO_REGISTRIES_CRATES_IO_PROTOCOL=sparse
RUN cargo build --release --no-default-features

FROM debian:bullseye
WORKDIR /

COPY --from=build-env /rust_twitter/target/release/rust_twitter /rust_twitter/rust_twitter
# FIXME: Migrating data doesn't work for no reason (see #9)
# COPY --from=build-env /rust_twitter/data /rust_twitter/data

EXPOSE 8080/tcp

ENTRYPOINT ["./rust_twitter/rust_twitter"]
