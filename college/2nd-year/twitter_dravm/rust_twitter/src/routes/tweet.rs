use axum::http::StatusCode;
use axum::{
    extract::{Path, Query},
    routing::{delete, get, post, put},
    Json, Router,
};
use bson::doc;
use bson::oid::ObjectId;
use serde::{Deserialize, Serialize};
use tracing::debug;
use wither::mongodb::options::FindOptions;

use crate::errors::Error;
use crate::models::tweet::{PublicTweet, Tweet};
use crate::utils::custom_response::{CustomResponse, CustomResponseBuilder};
use crate::utils::models::ModelExt;
use crate::utils::pagination::Pagination;
use crate::utils::request_query::RequestQuery;
use crate::utils::to_object_id::to_object_id;
use crate::utils::token::TokenUser;

pub fn create_route() -> Router {
    Router::new()
        .route("/api/tweet", post(create_tweet))
        .route("/api/tweet", get(query_tweets))
        .route("/api/tweet/feed", get(feature_feed))
        .route("/api/tweet/like/:id", post(feature_like))
        .route("/api/tweet/retweet/:id", post(feature_retweet))
        .route("/api/tweet/by_user/:id", get(query_tweets_by_user))
        .route("/api/tweet/:id", get(get_tweet_by_id))
        .route("/api/tweet/:id", delete(remove_tweet_by_id))
        .route("/api/tweet/:id", put(update_tweet_by_id))
}

async fn create_tweet(
    user: TokenUser,
    Json(payload): Json<CreateTweet>,
) -> Result<CustomResponse<PublicTweet>, Error> {
    let tweet = Tweet::new(payload.content, user.id);
    let tweet = Tweet::create(tweet).await?;
    let res = PublicTweet::from(tweet);

    let res = CustomResponseBuilder::new()
        .body(res)
        .status_code(StatusCode::CREATED)
        .build();

    debug!("Created tweet");
    Ok(res)
}

async fn query_tweets(
    user: TokenUser,
    Query(query): Query<RequestQuery>,
) -> Result<CustomResponse<Vec<PublicTweet>>, Error> {
    let pagination = Pagination::build_from_request_query(query);

    let options = FindOptions::builder()
        .sort(doc! { "timestamp": -1 })
        .skip(pagination.offset)
        .limit(pagination.limit as i64)
        .build();

    let (tweets, count) = Tweet::find_and_count(Some(doc! { "user": &user.id }), options).await?;
    let body = tweets
        .into_iter()
        .map(Into::into)
        .collect::<Vec<PublicTweet>>();

    let res = CustomResponseBuilder::new()
        .body(body)
        .pagination(pagination.count(count).build())
        .build();

    debug!("Queried tweets");
    Ok(res)
}

async fn query_tweets_by_user(
    Path(id): Path<String>,
    Query(query): Query<RequestQuery>,
) -> Result<CustomResponse<Vec<PublicTweet>>, Error> {
    let user_id = to_object_id(id)?;
    let pagination = Pagination::build_from_request_query(query);

    let options = FindOptions::builder()
        .sort(doc! { "timestamp": -1 })
        .skip(pagination.offset)
        .limit(pagination.limit as i64)
        .build();

    let (tweets, count) = Tweet::find_and_count(Some(doc! { "user": &user_id }), options).await?;
    let body = tweets
        .into_iter()
        .map(Into::into)
        .collect::<Vec<PublicTweet>>();

    let res = CustomResponseBuilder::new()
        .body(body)
        .pagination(pagination.count(count).build())
        .build();

    debug!("Queried tweets");
    Ok(res)
}

async fn get_tweet_by_id(Path(id): Path<String>) -> Result<Json<PublicTweet>, Error> {
    let tweet_id = to_object_id(id)?;
    let tweet = Tweet::find_one(doc! { "_id": tweet_id }, None)
        .await?
        .map(PublicTweet::from);

    let tweet = match tweet {
        Some(tweet) => tweet,
        None => {
            debug!("Tweet not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    debug!("Found tweet");
    Ok(Json(tweet))
}

async fn remove_tweet_by_id(
    user: TokenUser,
    Path(id): Path<String>,
) -> Result<CustomResponse<()>, Error> {
    let tweet_id = to_object_id(id)?;

    check_tweet_ownership(tweet_id, user).await?;

    let delete_result = Tweet::delete_one(doc! { "_id": tweet_id }).await?;

    if delete_result.deleted_count == 0 {
        debug!("Tweet not found, returning 404 status code");
        return Err(Error::not_found());
    }

    let res = CustomResponseBuilder::new()
        .status_code(StatusCode::NO_CONTENT)
        .build();

    debug!("Deleted tweet");
    Ok(res)
}

async fn update_tweet_by_id(
    user: TokenUser,
    Path(id): Path<String>,
    Json(payload): Json<UpdateTweet>,
) -> Result<Json<PublicTweet>, Error> {
    let tweet_id = to_object_id(id)?;
    let update = bson::to_document(&payload).unwrap();

    check_tweet_ownership(tweet_id, user).await?;

    let tweet = Tweet::find_one_and_update(doc! { "_id": &tweet_id }, doc! { "$set": update })
        .await?
        .map(PublicTweet::from);

    let tweet = match tweet {
        Some(tweet) => tweet,
        None => {
            debug!("Tweet not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    debug!("Updated tweet");
    Ok(Json(tweet))
}

async fn feature_feed(user: TokenUser) -> Result<Json<Vec<PublicTweet>>, Error> {
    // We need the real twitter algorithm, but it will do the job

    let pipeline = vec![
        doc! {
            "$match": {
                "user": { "$ne": &user.id }
            }
        },
        doc! {
            "$sample": {
                "size": 30
            }
        },
    ];

    let tweets = Tweet::aggregate::<Tweet>(pipeline).await?;

    let body = tweets
        .into_iter()
        .map(Into::into)
        .collect::<Vec<PublicTweet>>();

    debug!("Created feed");
    Ok(Json(body))
}

async fn feature_like(
    _user: TokenUser,
    Path(id): Path<String>,
) -> Result<Json<PublicTweet>, Error> {
    println!("{:?}", id);
    let tweet_id = to_object_id(id)?;

    let tweet =
        Tweet::find_one_and_update(doc! { "_id": &tweet_id }, doc! { "$inc":  { "likes": 1}})
            .await?
            .map(PublicTweet::from);

    let tweet = match tweet {
        Some(tweet) => tweet,
        None => {
            debug!("Tweet not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    debug!("Liked tweet");
    Ok(Json(tweet))
}

async fn feature_retweet(
    user: TokenUser,
    Path(id): Path<String>,
) -> Result<Json<PublicTweet>, Error> {
    let tweet_id = to_object_id(id)?;

    let tweet =
        Tweet::find_one_and_update(doc! { "_id": &tweet_id }, doc! { "$inc":  { "retweets": 1}})
            .await?
            .map(PublicTweet::from);

    let tweet = match tweet {
        Some(tweet) => tweet,
        None => {
            debug!("Tweet not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    create_tweet(
        user,
        Json(CreateTweet {
            content: tweet.content.clone(),
        }),
    )
    .await?;

    debug!("Retwetted tweet");
    Ok(Json(tweet))
}

async fn check_tweet_ownership(tweet_id: ObjectId, user: TokenUser) -> Result<(), Error> {
    let tweet_to_check = Tweet::find_one(doc! { "_id": tweet_id }, None)
        .await?
        .map(PublicTweet::from);

    let is_owner = match tweet_to_check {
        Some(tweet) => tweet.user == user.id,
        None => {
            debug!("Tweet not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    if !is_owner {
        debug!("Not the owner of the Tweet, returning 400 status code");
        return Err(Error::bad_request());
    }

    Ok(())
}

#[derive(Deserialize)]
struct CreateTweet {
    content: String,
}

#[derive(Serialize, Deserialize)]
struct UpdateTweet {
    content: String,
    likes: u32,
    retweets: u32,
}
