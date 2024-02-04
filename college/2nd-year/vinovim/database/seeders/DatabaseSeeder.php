<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        $this->call([
            GenderSeeder::class,
            ClientSeeder::class,
            DestinationSeeder::class,
            ImageSeeder::class,
            ParticipantCategorySeeder::class,
            ThemeSeeder::class,
            TripCategorySeeder::class,
            VineyardCategorySeeder::class,
            WineTrailSeeder::class,
            TripSeeder::class,
            StageSeeder::class,

            AddressSeeder::class,

            HotelPartnerSeeder::class,
            CavePartnerSeeder::class,
            OtherCompanySeeder::class,
            RestaurantPartnerSeeder::class,

            TourSeeder::class,
            AccommodationSeeder::class,
            MealSeeder::class,
            ActivitySeeder::class,

            BillingStateSeeder::class,
            BillingSeeder::class,
            PromoCodeSeeder::class,

            TourStageSeeder::class,
            AccommodationStageSeeder::class,
            MealStageSeeder::class,
            ActivityStageSeeder::class,
            CommentSeeder::class,

            ChatBotQuestionSeeder::class,
        ]);
    }
}
