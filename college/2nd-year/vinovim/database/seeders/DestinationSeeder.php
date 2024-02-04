<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class DestinationSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('destinations')->insert([
            'destination' => 'Bourgogne',
        ]);

        DB::table('destinations')->insert([
            'destination' => 'Bordeaux',
        ]);

        DB::table('destinations')->insert([
            'destination' => 'Paris',
        ]);
    }
}
