<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class MealSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('meals')->insert([
            'partner_id' => 35,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Restaurant Prosper',
            'description' => 'Le temps d’une pause sur la Route des Grands Crus ou d’un séjour dans un charmant village au cœur du vignoble, nous vous proposons ici une expérience culinaire de charme.',
            'price_per_person' => 10,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 36,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Comte Sénard',
            'description' => 'Les déjeuners proposés à la Table d’Hôtes consistent en une dégustation des vins du Domaine au cours d’un repas bourguignon.',
            'price_per_person' => 30,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 37,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Maison Trapet',
            'description' => 'Choisissez ce que vous souhaitez manger, froid «Comme dans les vignes !» ou chaud  «Comme un dimanche !»',
            'price_per_person' => 40,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 38,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Le Bistro d’Olivier',
            'description' => 'Un menu en 3 services avec une dégustation des vins de la maison. Revenir à la source de la première « table de dégustation » d’Olivier Leflaive en 1997, tel est le souhait de sa fille Julie, si attachée à la transmission, comme un trait d’union entre les deux générations.',
            'price_per_person' => 60,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 39,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Bott-Geyl',
            'description' => 'Le Domaine vous ouvre ses portes pour une visite et une dégustation de leurs magnifiques Grands Crus cultivés en biodynamie.',
            'price_per_person' => 20,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 40,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château de Ferrand',
            'description' => 'Adrien, sommelier du domaine, vous accueille à la propriété pour une visite du domaine et des chais, suivie d’une dégustation commentée de plusieurs cuvées du Château. Vous pourrez également profiter d’un atelier assemblage accompagné d’une planche de fromages et de charcuteries soigneusement sélectionnés.',
            'price_per_person' => 50,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 41,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Soutard',
            'description' => 'Le Château Soutard est un domaine classé Grand Cru de vingt-deux hectares, situé près de Saint-Emilion, dont l’activité viticole remonte au XVIIème siècle.',
            'price_per_person' => 70,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 42,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Stentz-Buecher',
            'description' => 'Depuis 3 ans maintenant, la famille Stentz-Buecher propose des activités oenotouristiques : des visites et des dégustations au sein de leur propriété, dans la cave des vieux millésimes.',
            'price_per_person' => 90,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 43,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Clos de Bourgogne',
            'description' => 'Sandrine et André Lanaud sont formateurs en œnologie et terroir, dégustateurs professionnels et passionnés des vins de Bourgogne.',
            'price_per_person' => 80,
        ]);

        DB::table('meals')->insert([
            'partner_id' => 44,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Domaine Schoenheitz',
            'description' => 'L’encépagement du domaine de 14 hectares est composé de 31% de Riesling, 21% de Gewurztraminer, 15% de Pinot Noir, autant de Pinot Gris et autant de Pinot Blanc. Le Muscat ne représente que 4% de l’encépagement total du domaine.',
            'price_per_person' => 15,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 45,
            'address_id' => 1,
            'image_id' => 1,
            'name' => '???????????????',
            'description' => '??????????????',
            'price_per_person' => 25,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 46,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Lynch Bages',
            'description' => 'Le Château vous accueille pour un cours d’oenologie, l’occasion de découvrir 4 appellations médocaines. Ou bien optez pour un atelier assemblage : distinguez les 4 principaux cépages bordelais à travers une dégustation de monocépages de Lynch-Bages, suivie de l’assemblage du même millésime.',
            'price_per_person' => 30,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 47,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Gaudrelle',
            'description' => 'L’équipe de Château Gaudrelle vous accueille pour des visites du domaine, des caves troglodytes et du vignoble mais aussi pour des dégustations et des ateliers ludiques autour du vin, de manière exclusive ou groupée.',
            'price_per_person' => 50,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 48,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Champagne Le Gallais',
            'description' => 'Chaque cuvée raconte une histoire : celle d’une parcelle, celle d’un assemblage, celle d’une année particulière, celle d’un geste ancestral, … Et toutes ces histoires sont issues d’un même clos et écrites par un terroir, des hommes et des femmes passionnés.',
            'price_per_person' => 45,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 49,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Siaurac',
            'description' => 'Situés sur la rive droite de Bordeaux entre Saint-Emilion et Pomerol, le Château Siaurac and Co a appartenu à la même famille depuis 1832, jusqu\'à son rachat en 2014 par le groupe artemis.',
            'price_per_person' => 15,
        ]);
        DB::table('meals')->insert([
            'partner_id' => 50,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Château Soucherie',
            'description' => 'Château Soucherie c’est avant tout la conviction d’un homme, Roger-François Béguinot, qui a investi pour pouvoir faire des grands vins d’expression terroir. Le Château Soucherie ne possède qu’1,8 hectares du Clos de Perrière, mais représente aujourd’hui un archétype des vins de Savennières d’un certain classicisme.',
            'price_per_person' => 15,
        ]);
    }
}
