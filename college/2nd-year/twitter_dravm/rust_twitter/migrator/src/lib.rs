use anyhow::Result;
use async_trait::async_trait;
use bson::Document;
use std::{
    env,
    fs::File,
    io::{BufRead, BufReader},
};

use mongodb_migrator::{migration::Migration, migrator::Env};

pub async fn run_migrator(mongo_uri: &String, mongo_db: &String) {
    let client = mongodb::Client::with_uri_str(mongo_uri).await.unwrap();
    let db = client.database(mongo_db);

    let migrations: Vec<Box<dyn Migration>> = vec![Box::new(Tweet {}), Box::new(User {})];
    mongodb_migrator::migrator::default::DefaultMigrator::new()
        .with_conn(db.clone())
        .with_migrations_vec(migrations)
        .up()
        .await
        .expect("error when running migrator");

    println!("migrations applied");
}

fn get_data_root() -> String {
    let default_root = String::from("./data");

    if let Some(data_root) = env::var_os("DATA_ROOT") {
        return data_root.into_string().unwrap_or(default_root);
    }

    default_root
}

fn read_data(file: &str) -> Result<Vec<Document>> {
    let mut bson_vec = Vec::new();

    let path = format!("{}/{}", get_data_root(), file);
    let file = File::open(path)?;
    let reader = BufReader::new(file);

    for line in reader.lines() {
        let json_line: serde_json::Value = serde_json::from_str(&line?)?;
        bson_vec.push(bson::to_document(&json_line)?);
    }

    Ok(bson_vec)
}

struct Tweet {}
struct User {}

#[async_trait]
impl Migration for Tweet {
    async fn up(&self, env: Env) -> Result<()> {
        env.db
            .expect("db is available")
            .collection("tweets")
            .insert_many(read_data("tweets.json")?, None)
            .await
            .expect("error adding tweets");

        Ok(())
    }
}

#[async_trait]
impl Migration for User {
    async fn up(&self, env: Env) -> Result<()> {
        env.db
            .expect("db is available")
            .collection("users")
            .insert_many(read_data("users.json")?, None)
            .await
            .expect("error adding users");

        Ok(())
    }
}
