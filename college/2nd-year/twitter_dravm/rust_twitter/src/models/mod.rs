pub mod tweet;
pub mod user;

use crate::errors::Error;
use crate::utils::models::ModelExt;

pub async fn sync_indexes() -> Result<(), Error> {
    tweet::Tweet::sync_indexes().await?;
    user::User::sync_indexes().await?;

    Ok(())
}
