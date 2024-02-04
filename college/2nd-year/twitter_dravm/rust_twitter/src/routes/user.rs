use axum::http::StatusCode;
use axum::{
    extract::{Path, Query},
    routing::{delete, get, post, put},
    Json, Router,
};
use bson::doc;
use serde::{Deserialize, Serialize};
use tracing::debug;
use wither::mongodb::options::FindOptions;

use crate::errors::{AuthenticateError, Error};
use crate::models::user::{hash_password, PublicUser, User};
use crate::utils::custom_response::{CustomResponse, CustomResponseBuilder};
use crate::utils::models::ModelExt;
use crate::utils::pagination::Pagination;
use crate::utils::request_query::RequestQuery;
use crate::utils::to_object_id::to_object_id;
use crate::utils::token::{self, TokenUser};

pub fn create_route() -> Router {
    Router::new()
        .route("/api/user", post(create_user))
        .route("/api/user", get(query_users))
        .route("/api/user/auth", post(authenticate_user))
        .route("/api/user/:id", get(get_user_by_id))
        .route("/api/user/:id", put(update_user))
        .route("/api/user/:id", delete(remove_user))
}

async fn create_user(Json(payload): Json<CreateUser>) -> Result<CustomResponse<PublicUser>, Error> {
    let password_hash = hash_password(payload.password).await?;
    let user = User::new(
        payload.name,
        payload.handle,
        payload.image_url,
        password_hash,
    );
    let user = User::create(user).await?;
    let res = PublicUser::from(user);

    let res = CustomResponseBuilder::new()
        .body(res)
        .status_code(StatusCode::CREATED)
        .build();

    debug!("Created user");
    Ok(res)
}

async fn authenticate_user(
    Json(body): Json<AuthorizeUser>,
) -> Result<Json<AuthenticateResponse>, Error> {
    let handle = &body.handle;
    let password = &body.password;

    if handle.is_empty() {
        debug!("Missing handle, returning 400 status code");
        return Err(Error::bad_request());
    }

    if password.is_empty() {
        debug!("Missing password, returning 400 status code");
        return Err(Error::bad_request());
    }

    let user = User::find_one(doc! { "handle": handle }, None).await?;

    let user = match user {
        Some(user) => user,
        None => {
            debug!("User not found, returning 401");
            return Err(Error::not_found());
        }
    };

    if !user.is_password_match(password) {
        debug!("User password is incorrect, returning 401 status code");
        return Err(Error::Authenticate(AuthenticateError::WrongCredentials));
    }

    if user.locked_at.is_some() {
        debug!("User is locked, returning 401");
        return Err(Error::Authenticate(AuthenticateError::Locked));
    }

    let secret = &token::SECRET;
    let token = token::create(user.clone(), secret)
        .map_err(|_| Error::Authenticate(AuthenticateError::TokenCreation))?;

    let res = AuthenticateResponse {
        access_token: token,
        user: PublicUser::from(user),
    };

    Ok(Json(res))
}

async fn query_users(
    Query(query): Query<RequestQuery>,
) -> Result<CustomResponse<Vec<PublicUser>>, Error> {
    let pagination = Pagination::build_from_request_query(query);

    let options = FindOptions::builder()
        .sort(doc! { "timestamp": -1 })
        .skip(pagination.offset)
        .limit(pagination.limit as i64)
        .build();

    let (users, count) = User::find_and_count(None, options).await?;
    let body = users
        .into_iter()
        .map(Into::into)
        .collect::<Vec<PublicUser>>();

    let res = CustomResponseBuilder::new()
        .body(body)
        .pagination(pagination.count(count).build())
        .build();

    debug!("Queried users");
    Ok(res)
}

async fn get_user_by_id(Path(id): Path<String>) -> Result<Json<PublicUser>, Error> {
    let user_id = to_object_id(id)?;
    let user = User::find_one(doc! { "_id": user_id }, None)
        .await?
        .map(PublicUser::from);

    let user = match user {
        Some(user) => user,
        None => {
            debug!("User not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    debug!("Found user");
    Ok(Json(user))
}

async fn remove_user(user: TokenUser) -> Result<CustomResponse<()>, Error> {
    let delete_result = User::delete_one(doc! { "_id": &user.id }).await?;

    if delete_result.deleted_count == 0 {
        debug!("User not found, returning 404 status code");
        return Err(Error::not_found());
    }

    let res = CustomResponseBuilder::new()
        .status_code(StatusCode::NO_CONTENT)
        .build();

    debug!("Deleted user");
    Ok(res)
}

async fn update_user(
    user: TokenUser,
    Json(payload): Json<UpdateUser>,
) -> Result<Json<PublicUser>, Error> {
    let update = bson::to_document(&payload).unwrap();

    let user = User::find_one_and_update(doc! { "_id": &user.id }, doc! { "$set": update })
        .await?
        .map(PublicUser::from);

    let user = match user {
        Some(user) => user,
        None => {
            debug!("User not found, returning 404 status code");
            return Err(Error::not_found());
        }
    };

    debug!("Updated user");
    Ok(Json(user))
}

#[derive(Deserialize)]
struct CreateUser {
    name: String,
    handle: String,
    image_url: Option<String>,
    password: String,
}

#[derive(Serialize, Deserialize)]
struct UpdateUser {
    content: String,
    likes: u32,
    reusers: u32,
}

#[derive(Debug, Deserialize)]
struct AuthorizeUser {
    handle: String,
    password: String,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct AuthenticateResponse {
    pub access_token: String,
    pub user: PublicUser,
}
