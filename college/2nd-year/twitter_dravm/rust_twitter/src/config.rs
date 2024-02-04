use lazy_static::lazy_static;
use std::env;

#[derive(Debug, PartialEq)]
pub struct Config {
    mongo_uri: String,
    mongo_db: String,
    axum_port: u16,
}

#[derive(Debug)]
struct ConfigBuilder {
    mongo_uri: String,
    mongo_db: String,
    axum_port: u16,
}

impl Config {
    pub fn get_uri(&self) -> &String {
        &self.mongo_uri
    }

    pub fn get_db(&self) -> &String {
        &self.mongo_db
    }

    pub fn get_port(&self) -> u16 {
        self.axum_port
    }
}

impl ConfigBuilder {
    fn build(self) -> Config {
        Config {
            mongo_uri: self.mongo_uri,
            mongo_db: self.mongo_db,
            axum_port: self.axum_port,
        }
    }

    fn with_uri(mut self, mongo_uri: String) -> ConfigBuilder {
        self.mongo_uri = mongo_uri;
        self
    }

    fn with_db(mut self, mongo_db: String) -> ConfigBuilder {
        self.mongo_db = mongo_db;
        self
    }

    fn with_port(mut self, axum_port: u16) -> ConfigBuilder {
        self.axum_port = axum_port;
        self
    }
}

impl Default for ConfigBuilder {
    fn default() -> Self {
        ConfigBuilder {
            mongo_uri: String::from("mongodb://localhost"),
            mongo_db: String::from("twitter_dravm"),
            axum_port: 3000,
        }
    }
}

lazy_static! {
    pub static ref CONFIG: Config = {
        let mut config_builder = ConfigBuilder::default();

        if let Some(mongo_uri) = env::var_os("MONGO_URI") {
            let uri = mongo_uri.into_string().expect("error parsing MONGO_URI");
            config_builder = config_builder.with_uri(uri);
        }

        if let Some(mongo_db) = env::var_os("MONGO_DB") {
            let db = mongo_db.into_string().expect("error parsing MONGO_DB");
            config_builder = config_builder.with_db(db);
        }

        if let Some(axum_port) = env::var_os("AXUM_PORT") {
            let str_port = axum_port.into_string().expect("error parsing AXUM_PORT");

            let port = str_port
                .parse::<u16>()
                .expect("error parsing the axum port");

            config_builder = config_builder.with_port(port);
        }

        config_builder.build()
    };
}

#[cfg(test)]
mod test {
    #[test]
    fn build_default_config() {
        use super::*;

        let default_config = ConfigBuilder {
            mongo_uri: String::from("mongodb://localhost"),
            mongo_db: String::from("twitter_dravm"),
            axum_port: 3000,
        }
        .build();

        let config = ConfigBuilder::default().build();

        assert_eq!(config, default_config);
    }

    #[test]
    fn build_custom_config() {
        use super::*;

        let custom_config = ConfigBuilder::default()
            .with_uri(String::from("mongodb://mongodb"))
            .with_db(String::from("twitter"))
            .with_port(80)
            .build();

        let reference_config = Config {
            mongo_uri: String::from("mongodb://mongodb"),
            mongo_db: String::from("twitter"),
            axum_port: 80,
        };

        assert_eq!(custom_config, reference_config);
    }
}
