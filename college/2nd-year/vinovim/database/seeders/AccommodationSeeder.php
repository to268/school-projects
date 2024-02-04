<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class AccommodationSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('accommodations')->insert([
            'partner_id' => 1,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Maison Olivier Leflaive',
            'description' => 'Au cœur de Puligny-Montrachet, la Maison Olivier Leflaive, authentique bâtisse du XVIIème siècle surplombant la place du village, vous propose dix sept chambres de charme et deux suites avec terrasses.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Au cœur de Puligny-Montrachet, la Maison Olivier Leflaive, authentique bâtisse du XVIIème siècle surplombant la place du village, vous propose dix sept chambres de charme et deux suites avec terrasses.',
            'price_per_person' => 20,
        ]);
        // 2
        DB::table('accommodations')->insert([
            'partner_id' => 2,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'La Maison Rouge',
            'description' => 'Cette grande ferme bourguignonne rénovée, est située aux portes de Beaune, à seulement 5 kilomètres de ses célèbres Hospices.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Cette grande ferme bourguignonne rénovée, est située aux portes de Beaune, à seulement 5 kilomètres de ses célèbres Hospices.',
            'price_per_person' => 40,
        ]);
        // 3
        DB::table('accommodations')->insert([
            'partner_id' => 3,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château en Entre-deux-Mers',
            'description' => 'Dormez dans une splendide propriété viticole en Entre-deux-Mers, vignoble aux portes de Bordeaux enserré entre la Garonne et la Dordogne.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Dormez dans une splendide propriété viticole en Entre-deux-Mers, vignoble aux portes de Bordeaux enserré entre la Garonne et la Dordogne.',
            'price_per_person' => 60,
        ]);
        // 4
        DB::table('accommodations')->insert([
            'partner_id' => 4,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Hostellerie de Levernois',
            'description' => 'L\’Hostellerie de Levernois, hôtel 5 étoiles, est une demeure authentique, au charme et au caractère d\’une Maison Bourgeoise bourguignonne, alliant tradition et modernité.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'L\’Hostellerie de Levernois, hôtel 5 étoiles, est une demeure authentique, au charme et au caractère d\’une Maison Bourgeoise bourguignonne, alliant tradition et modernité.',
            'price_per_person' => 80,
        ]);
        // 5
        DB::table('accommodations')->insert([
            'partner_id' => 5,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Maison Prosper Maufoux',
            'description' => 'La Maison Prosper Maufoux vous reçoit au Château de Saint-Aubin, sur la route des Grands Crus de Bourgogne dans le village du même nom, dans leur maison d\’hôtes de charme entre élégance contemporaine et authenticité, décoration design et mobilier d\’époque.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'La Maison Prosper Maufoux vous reçoit au Château de Saint-Aubin, sur la route des Grands Crus de Bourgogne dans le village du même nom, dans leur maison d\’hôtes de charme entre élégance contemporaine et authenticité, décoration design et mobilier d\’époque.',
            'price_per_person' => 10,
        ]);
        // 6
        DB::table('accommodations')->insert([
            'partner_id' => 6,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château La Romaningue',
            'description' => 'En arrivant au domaine de la Romaningue, vous empruntez le chemin qui serpente dans les bois, longez l\’étang en passant sous le chai et sa tour du 15ème siècle.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'En arrivant au domaine de la Romaningue, vous empruntez le chemin qui serpente dans les bois, longez l\’étang en passant sous le chai et sa tour du 15ème siècle.',
            'price_per_person' => 30,
        ]);
        // 7
        DB::table('accommodations')->insert([
            'partner_id' => 7,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Hostellerie Le Cèdre',
            'description' => 'L\’Hostellerie Le Cèdre, magnifique demeure bourgeoise datant du XIXème siècle située dans un écrin de verdure au cœur de Beaune.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'L\’Hostellerie Le Cèdre, magnifique demeure bourgeoise datant du XIXème siècle située dans un écrin de verdure au cœur de Beaune.',
            'price_per_person' => 50,
        ]);
        // 8
        DB::table('accommodations')->insert([
            'partner_id' => 8,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Hostellerie de Levernois',
            'description' => 'L\’Hostellerie de Levernois, hôtel 5 étoiles, est une demeure authentique, au charme et au caractère d\’une Maison Bourgeoise bourguignonne, alliant tradition et modernité.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'L\’Hostellerie de Levernois, hôtel 5 étoiles, est une demeure authentique, au charme et au caractère d\’une Maison Bourgeoise bourguignonne, alliant tradition et modernité.',
            'price_per_person' => 70,
        ]);
        // 9
        DB::table('accommodations')->insert([
            'partner_id' => 9,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Demeure d\'Antan',
            'description' => 'Superbe maison d\'hôtes et ancien domaine viticole datant du 18eme siècle, situé entre Riquewihr et Colmar. Accueil chaleureux, maison à colombage, décoration typique, petit-déjeuner alsacien, vos hôtes feront de votre séjour dans la région une expérience unique.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Superbe maison d\'hôtes et ancien domaine viticole datant du 18eme siècle, situé entre Riquewihr et Colmar. Accueil chaleureux, maison à colombage, décoration typique, petit-déjeuner alsacien, vos hôtes feront de votre séjour dans la région une expérience unique.',
            'price_per_person' => 90,
        ]);
        // 10
        DB::table('accommodations')->insert([
            'partner_id' => 10,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château du Tertre',
            'description' => 'Le Château du Tertre vous accueille dans un décor chaleureux et raffiné, offrant une vue imprenable sur le vignoble.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Le Château du Tertre vous accueille dans un décor chaleureux et raffiné, offrant une vue imprenable sur le vignoble.',
            'price_per_person' => 15,
        ]);
        // 11
        DB::table('accommodations')->insert([
            'partner_id' => 11,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Golf du Médoc',
            'description' => 'Niché au cœur de 200 hectares de nature, le Golf du Médoc Hôtel & Spa s\’intègre parfaitement au paysage. L\’établissement propose 79 chambres et suites décorées.',
            'opening_time' => '08:00:00',
            'closing_time' => '21:00:00',
            'details' => 'Niché au cœur de 200 hectares de nature, le Golf du Médoc Hôtel & Spa s\’intègre parfaitement au paysage. L\’établissement propose 79 chambres et suites décorées.',
            'price_per_person' => 15,
        ]);
    }
}
