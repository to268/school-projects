<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class TourSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('tours')->insert([
            'partner_id' => 12,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Corton C.',
            'description' => 'Visite et dégustation de vins au Château Corton C. à Aloxe-Corton',
            'price_per_person' => 69,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 13,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Maison Prosper Maufoux - Château de Saint-Aubin',
            'description' => 'Maison Prosper Maufoux - Château de Saint-Aubin',
            'price_per_person' => 50,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 14,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine du Comte Sénard',
            'description' => 'Déjeuner dégustation au Domaine Comte Sénard - Aloxe-Corton',
            'price_per_person' => 75,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 15,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Debray',
            'description' => 'Domaine Debray - Beaune',
            'price_per_person' => 25,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 16,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Clos de Bourgogne',
            'description' => 'Atelier autour de la vigne et du vin au Clos de Bourgogne',
            'price_per_person' => 53,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 17,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Manuel Olivier',
            'description' => 'Domaine Manuel Olivier - Bourgogne - Nuits-Saint-Georges',
            'price_per_person' => 33,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 18,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Trapet',
            'description' => 'Table vigneronne de la Maison Trapet - Bourgogne - Gevrey-chambertin',
            'price_per_person' => 92,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 19,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Bouscaut',
            'description' => 'Château Bouscaut',
            'price_per_person' => 30,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 20,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château du Taillan',
            'description' => 'Château du Taillan à Taillan-Médoc',
            'price_per_person' => 60,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 21,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Suduiraut',
            'description' => 'Château Suduiraut - Sauternes',
            'price_per_person' => 45,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 22,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Tour Saint Christophe',
            'description' => 'Château Tour Saint Christophe',
            'price_per_person' => 80,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 23,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Soutard',
            'description' => 'Dégustation au Château Soutard, à Saint-Emilion',
            'price_per_person' => 20,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 24,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Desmirail',
            'description' => 'Château Desmirail à Margaux',
            'price_per_person' => 15,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 25,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Schoenheitz',
            'description' => 'Domaine Schoenheitz - Dégustation en accords mets et vins',
            'price_per_person' => 65,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 26,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine François Schwach',
            'description' => 'Dégustation des crémants d\'Alsace au Domaine François Schwach à Kaysersberg',
            'price_per_person' => 30,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 27,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Bott-Geyl',
            'description' => 'Grands Crus en biodynamie au Domaine Bott-Geyl en Alsace',
            'price_per_person' => 75,
        ]);

        DB::table('tours')->insert([
            'partner_id' => 28,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Maison Olivier Leflaive',
            'description' => 'Maison Olivier Leflaive - Visite et dégustation de vin au domaine',
            'price_per_person' => 25,
        ]);
    }
}
