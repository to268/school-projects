use bson::{
    serde_helpers::{bson_datetime_as_rfc3339_string, serialize_object_id_as_hex_string},
    DateTime,
};
use chrono::Utc;
use serde::{Deserialize, Serialize};
use validator::Validate;
use wither::bson::{doc, oid::ObjectId};
use wither::Model as WitherModel;

use crate::utils::models::ModelExt;

impl ModelExt for Tweet {
    type T = Tweet;
}

#[derive(Debug, Clone, Serialize, Deserialize, WitherModel, Validate)]
pub struct Tweet {
    #[serde(rename = "_id", skip_serializing_if = "Option::is_none")]
    pub id: Option<ObjectId>,
    pub user: ObjectId,
    #[validate(length(min = 1, max = 1024))]
    pub content: String,
    pub likes: u32,
    pub retweets: u32,
    pub timestamp: DateTime,
}

impl Tweet {
    pub fn new(content: String, user: ObjectId) -> Self {
        Self {
            id: None,
            user,
            content,
            likes: 0,
            retweets: 0,
            timestamp: Utc::now().into(),
        }
    }
}

#[derive(Debug, Serialize, Deserialize)]
pub struct PublicTweet {
    #[serde(alias = "_id", serialize_with = "serialize_object_id_as_hex_string")]
    pub id: ObjectId,
    pub user: ObjectId,
    pub content: String,
    pub likes: u32,
    pub retweets: u32,
    #[serde(with = "bson_datetime_as_rfc3339_string")]
    pub timestamp: DateTime,
}

impl From<Tweet> for PublicTweet {
    fn from(tweet: Tweet) -> Self {
        Self {
            id: tweet.id.unwrap(),
            user: tweet.user,
            content: tweet.content,
            likes: tweet.likes,
            retweets: tweet.retweets,
            timestamp: tweet.timestamp,
        }
    }
}
