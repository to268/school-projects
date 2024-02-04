<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class WineTrailSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins d\'Alsace',
        ]);
        // 2
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de Bourgogne',
        ]);
        // 3
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de Bordeaux',
        ]);
        // 4
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de Provence',
        ]);
        // 5
        DB::table('wine_trails')->insert([
            'trail' => 'Route du Champagne',
        ]);
        // 6
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins Val de Loire',
        ]);
        // 7
        DB::table('wine_trails')->insert([
            'trail' => 'Languedoc-Roussillon',
        ]);
        // 8
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins du Rhône',
        ]);
        // 9
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins du Beaujolais',
        ]);
        // 10
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de Corse',
        ]);
        // 11
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins du Sud-Ouest',
        ]);
        // 12
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins du Jura',
        ]);
        // 13
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de la Savoie',
        ]);
        // 14
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins Ile-de-France',
        ]);
        // 15
        DB::table('wine_trails')->insert([
            'trail' => 'Route des vins de Catalogne',
        ]);
    }
}
