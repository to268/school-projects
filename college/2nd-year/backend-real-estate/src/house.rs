use axum::{
    Json,
    http::StatusCode,
};
use sea_orm::entity::prelude::*;
use entity::{prelude::*, house::Model};
use crate::database::DB_CONNEXION;

async fn read_houses() -> Result<Vec<Model>, DbErr> {
    let db = DB_CONNEXION.get().await;
    House::find().all(db).await
}

#[axum_macros::debug_handler]
pub async fn get_houses() -> Result<Json<Vec<Model>>, StatusCode> {
    let data = read_houses()
        .await
        .expect("cannot find houses");

    Ok(Json(data))
}
