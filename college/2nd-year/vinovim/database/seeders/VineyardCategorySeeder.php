<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class VineyardCategorySeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble d\'Alsace',
        ]);
        // 2
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Bourgogne',
        ]);
        // 3
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Bordeaux',
        ]);
        // 4
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Provence',
        ]);
        // 5
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Champagne',
        ]);
        // 6
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble du Val De Loire',
        ]);
        // 7
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble du Languedoc-Roussillon',
        ]);
        // 8
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble du Vallé du Rhône',
        ]);
        // 9
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Beaujolais',
        ]);
        // 10
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Corse',
        ]);
        // 11
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble du Sud-Ouest',
        ]);
        // 12
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble du Jura',
        ]);
        // 13
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Savoie',
        ]);
        // 14
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Paris',
        ]);
        // 15
        DB::table('vineyard_categories')->insert([
            'category' => 'Vignoble de Catalogne',
        ]);
    }
}
