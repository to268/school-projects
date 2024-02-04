<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ThemeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('themes')->insert([
            'name' => 'Bien-être',
        ]);

        DB::table('themes')->insert([
            'name' => 'Culture',
        ]);

        DB::table('themes')->insert([
            'name' => 'Gastronomie',
        ]);

        DB::table('themes')->insert([
            'name' => 'Sport',
        ]);
    }
}
