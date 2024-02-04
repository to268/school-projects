<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ParticipantCategorySeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('participant_categories')->insert([
            'category' => 'En couple',
            'emoji' => '💑',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'En famille',
            'emoji' => '👪',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'Entre amis',
            'emoji' => '👦👧',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'En couple & Entre Amis',
            'emoji' => '💑  👦👧',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'En famille & Entre Amis',
            'emoji' => '👪  👦👧',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'Entre amis & En Couple',
            'emoji' => '👦👧  👪',
        ]);

        DB::table('participant_categories')->insert([
            'category' => 'Pour tous',
            'emoji' => '💑  👪  👦👧',
        ]);
    }
}
