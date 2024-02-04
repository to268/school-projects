mod app;
mod config;
mod database;
mod errors;
mod models;
mod routes;
mod utils;

use config::CONFIG;
use std::net::SocketAddr;

#[tokio::main]
async fn main() {
    let app = app::create_app().await;

    let addr = SocketAddr::from(([0, 0, 0, 0], CONFIG.get_port()));
    println!("listening on {}", addr);

    axum::Server::bind(&addr)
        .serve(app.into_make_service())
        .await
        .unwrap();
}
