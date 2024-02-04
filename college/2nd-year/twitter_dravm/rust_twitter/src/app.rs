use std::{env, time::Duration};

use axum::{http::header, Router};

use tower_http::{
    compression::CompressionLayer, cors::CorsLayer, propagate_header::PropagateHeaderLayer,
    sensitive_headers::SetSensitiveHeadersLayer, timeout::TimeoutLayer, trace, trace::TraceLayer,
};

use crate::models;
use crate::routes;

fn setup_logger() {
    if env::var_os("RUST_LOG").is_none() {
        let env = "backend=debug,tower_http=debug";
        env::set_var("RUST_LOG", env);
    }

    tracing_subscriber::fmt::init();
}

pub async fn create_app() -> Router {
    setup_logger();

    models::sync_indexes()
        .await
        .expect("Failed to sync database indexes");

    Router::new()
        .merge(routes::tweet::create_route())
        .merge(routes::user::create_route())
        .layer(
            TraceLayer::new_for_http()
                .make_span_with(trace::DefaultMakeSpan::new().include_headers(true))
                .on_request(trace::DefaultOnRequest::new().level(tracing::Level::INFO))
                .on_response(trace::DefaultOnResponse::new().level(tracing::Level::INFO)),
        )
        .layer(SetSensitiveHeadersLayer::new(std::iter::once(
            header::AUTHORIZATION,
        )))
        .layer(CompressionLayer::new())
        .layer(PropagateHeaderLayer::new(header::HeaderName::from_static(
            "x-request-id",
        )))
        .layer(TimeoutLayer::new(Duration::from_secs(10)))
        .layer(CorsLayer::permissive())
}
