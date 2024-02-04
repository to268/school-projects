use sea_orm::{
    ConnectOptions,
    DatabaseConnection,
    Database
};

use std::time::Duration;
use dotenv::dotenv;
use lazy_static::lazy_static;
use async_once::AsyncOnce;
use tracing::log;

use migration::{Migrator, MigratorTrait, DbErr};

pub async fn migrate() -> Result<(), DbErr> {
    let db = DB_CONNEXION.get().await;
    Migrator::up(db, None).await?;
    Ok(())
}

lazy_static!{
    pub static ref DB_CONNEXION: AsyncOnce<DatabaseConnection> = AsyncOnce::new(async{
        dotenv().ok();

        let database_uri = std::env::var("DATABASE_URI")
            .expect("Failed to load `DATABASE_URI` environment variable.");

        let mut opt = ConnectOptions::new(database_uri);

        opt.max_connections(100)
            .min_connections(5)
            .connect_timeout(Duration::from_secs(8))
            .acquire_timeout(Duration::from_secs(8))
            .idle_timeout(Duration::from_secs(8))
            .max_lifetime(Duration::from_secs(8))
            .sqlx_logging(true)
            .sqlx_logging_level(log::LevelFilter::Info)
            .set_schema_search_path("public".into());

        Database::connect(opt).await
            .expect("cannot connect to the database")
    });
}
