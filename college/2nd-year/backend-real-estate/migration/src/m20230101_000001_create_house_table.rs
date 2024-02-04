use sea_orm::ActiveModelTrait;
use sea_orm_migration::{prelude::*, sea_orm::{TransactionTrait, Set, prelude::Decimal}};
use entity::house;

#[derive(DeriveMigrationName)]
pub struct Migration;

#[async_trait::async_trait]
impl MigrationTrait for Migration {
    async fn up(&self, manager: &SchemaManager) -> Result<(), DbErr> {
        manager
            .create_table(
                Table::create()
                    .table(House::Table)
                    .if_not_exists()
                    .col(
                        ColumnDef::new(House::Id)
                            .integer()
                            .not_null()
                            .auto_increment()
                            .primary_key(),
                    )
                    .col(
                        ColumnDef::new(House::Title)
                            .string_len(128)
                            .not_null()
                    )
                    .col(
                        ColumnDef::new(House::Image)
                            .string_len(255)
                            .not_null()
                    )
                    .col(
                        ColumnDef::new(House::Price)
                            .decimal_len(12, 2)
                            .not_null()
                    )
                    .to_owned(),
            ).await?;

        Ok(seed(&manager).await?)
    }

    async fn down(&self, manager: &SchemaManager) -> Result<(), DbErr> {
        manager
            .drop_table(Table::drop().table(House::Table).to_owned())
            .await
    }
}

async fn seed<'a>(manager: &SchemaManager<'a>) -> Result<(), DbErr> {
    let db = manager.get_connection();
    let transaction = db.begin().await?;

    house::ActiveModel {
        title: Set("Florida house".to_owned()),
        image: Set("/images/florida-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(156999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    house::ActiveModel {
        title: Set("Greece house".to_owned()),
        image: Set("/images/greece-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(9999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    house::ActiveModel {
        title: Set("Australia house".to_owned()),
        image: Set("/images/australia-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(129999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    house::ActiveModel {
        title: Set("Los Angeles house".to_owned()),
        image: Set("/images/los-angeles-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(189999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    house::ActiveModel {
        title: Set("Washington house".to_owned()),
        image: Set("/images/washington-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(11999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    house::ActiveModel {
        title: Set("Las Vegas house".to_owned()),
        image: Set("/images/las-vegas-house.webp".to_owned()),
        price: Set(Decimal::from_f32_retain(24999999.00_f32).unwrap()),
        ..Default::default()
    }
    .insert(&transaction)
    .await?;

    Ok(transaction.commit().await?)
}

#[derive(Iden)]
enum House {
    Table,
    Id,
    Title,
    Image,
    Price,
}
