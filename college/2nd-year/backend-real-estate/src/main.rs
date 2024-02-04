mod database;
mod house;

use axum::{
    routing::get,
    http::{StatusCode, header},
    response::IntoResponse,
    Json,
    Router,
};
use tower_http::{
    trace::{
        TraceLayer,
        self
    },
    sensitive_headers::SetSensitiveHeadersLayer
};
use std::net::SocketAddr;
use crate::house::get_houses;

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    database::migrate().await?;

    tracing_subscriber::fmt::init();

    let app = Router::new()
        .route("/", get(root))
        .route("/api/houses", get(get_houses))
        .layer(
            TraceLayer::new_for_http()
                .make_span_with(trace::DefaultMakeSpan::new().include_headers(true))
                .on_request(trace::DefaultOnRequest::new().level(tracing::Level::INFO))
                .on_response(trace::DefaultOnResponse::new().level(tracing::Level::INFO)),
        )
        .layer(SetSensitiveHeadersLayer::new(std::iter::once(
            header::AUTHORIZATION,
        )));

    let addr = SocketAddr::from(([127, 0, 0, 1], 3000));
    tracing::debug!("listening on {}", addr);
    axum::Server::bind(&addr)
        .serve(app.into_make_service())
        .await
        .unwrap();

    Ok(())
}

async fn root() -> impl IntoResponse {
    (StatusCode::OK, Json("Hello, World!"))
}
