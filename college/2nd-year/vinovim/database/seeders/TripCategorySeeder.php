<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class TripCategorySeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('trip_categories')->insert([
            'category' => 'Dégustation',
        ]);

        DB::table('trip_categories')->insert([
            'category' => 'Initiation',
        ]);

        DB::table('trip_categories')->insert([
            'category' => 'Visite',
        ]);
    }
}
