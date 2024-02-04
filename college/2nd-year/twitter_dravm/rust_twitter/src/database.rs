use mongodb::{bson::doc, Client, Database};

use crate::config::CONFIG;
use async_once::AsyncOnce;
use lazy_static::lazy_static;

lazy_static! {
    pub static ref DB_CONNEXION: AsyncOnce<Database> = AsyncOnce::new(async {
        #[cfg(feature = "migrator")]
        migrator::run_migrator(CONFIG.get_uri(), CONFIG.get_db()).await;

        Client::with_uri_str(CONFIG.get_uri())
            .await
            .expect("cannot create a mongodb client")
            .database(CONFIG.get_db())
    });
}
