use bson::{
    serde_helpers::{bson_datetime_as_rfc3339_string, serialize_object_id_as_hex_string},
    DateTime,
};
use chrono::Utc;
use serde::{Deserialize, Serialize};
use tokio::task;
use validator::Validate;
use wither::bson::{doc, oid::ObjectId};
use wither::Model as WitherModel;

use crate::errors::Error;
use crate::utils::models::ModelExt;

impl ModelExt for User {
    type T = User;
}

#[derive(Debug, Clone, Serialize, Deserialize, WitherModel, Validate)]
#[model(index(keys = r#"doc!{ "handle": 1 }"#, options = r#"doc!{ "unique": true }"#))]
pub struct User {
    #[serde(rename = "_id", skip_serializing_if = "Option::is_none")]
    pub id: Option<ObjectId>,
    #[validate(length(min = 1, max = 64))]
    pub name: String,
    #[validate(length(min = 1, max = 32))]
    pub handle: String,
    #[validate(length(max = 256))]
    pub image_url: Option<String>,
    pub password: String,
    pub following: u32,
    pub followers: u32,
    pub timestamp: DateTime,
    pub locked_at: Option<DateTime>,
}

impl User {
    pub fn new(
        name: String,
        handle: String,
        image_url: Option<String>,
        password_hash: String,
    ) -> Self {
        Self {
            id: None,
            name,
            handle,
            image_url,
            password: password_hash,
            following: 0,
            followers: 0,
            timestamp: Utc::now().into(),
            locked_at: None,
        }
    }

    pub fn is_password_match(&self, password: &str) -> bool {
        bcrypt::verify(password, self.password.as_ref()).unwrap_or(false)
    }
}

#[derive(Debug, Serialize, Deserialize)]
pub struct PublicUser {
    #[serde(alias = "_id", serialize_with = "serialize_object_id_as_hex_string")]
    pub id: ObjectId,
    pub name: String,
    pub handle: String,
    pub image_url: Option<String>,
    pub following: u32,
    pub followers: u32,
    #[serde(with = "bson_datetime_as_rfc3339_string")]
    pub timestamp: DateTime,
}

impl From<User> for PublicUser {
    fn from(user: User) -> Self {
        Self {
            id: user.id.unwrap(),
            name: user.name,
            handle: user.handle,
            image_url: user.image_url,
            following: user.following,
            followers: user.followers,
            timestamp: user.timestamp,
        }
    }
}

pub async fn hash_password<P>(password: P) -> Result<String, Error>
where
    P: AsRef<str> + Send + 'static,
{
    task::spawn_blocking(move || bcrypt::hash(password.as_ref(), bcrypt::DEFAULT_COST))
        .await
        .map_err(Error::RunSyncTask)?
        .map_err(Error::HashPassword)
}
