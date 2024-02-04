<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ActivitySeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('activities')->insert([
            'partner_id' => 29,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Golf du Château de Chailly',
            'description' => 'Parcours 18 trous, sur le parcours conçu par Thierry Sprecher et Géry Watine, champion de France professionnel en 1989',
            'time' => '01:00:00',
            'details' => 'Parcours 18 trous, sur le parcours conçu par Thierry Sprecher et Géry Watine, champion de France professionnel en 1989',
            'price_per_person' => 20,
        ]);
        // 2
        DB::table('activities')->insert([
            'partner_id' => 30,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Golf du Médoc',
            'description' => 'Parcours "Les Châteaux" (le green fee peut-être remplacé par un soin au Spa Décleor), ce parcours de championnat, dessiné par l’architecte américain Bill Coore s’inscrit dans la plus pure tradition des links écossais',
            'time' => '02:00:00',
            'details' => 'Parcours "Les Châteaux" (le green fee peut-être remplacé par un soin au Spa Décleor), ce parcours de championnat, dessiné par l’architecte américain Bill Coore s’inscrit dans la plus pure tradition des links écossais',
            'price_per_person' => 45,
        ]);
        // 3
        DB::table('activities')->insert([
            'partner_id' => 31,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Golf d’Ammerschwihr',
            'description' => 'Départ pour le Golf d’Ammerschwihr pour un parcours 18 trous. Le green fee peut être remplacé par un soin à la carte au spa de l’hôtel',
            'time' => '04:00:00',
            'details' => 'Départ pour le Golf d’Ammerschwihr pour un parcours 18 trous. Le green fee peut être remplacé par un soin à la carte au spa de l’hôtel',
            'price_per_person' => 90,
        ]);
        // 4
        DB::table('activities')->insert([
            'partner_id' => 32,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Golf de Reims',
            'description' => 'Départ pour le Golf de Reims pour un parcours 18 trous',
            'time' => '00:30:00',
            'details' => 'Départ pour le Golf de Reims pour un parcours 18 trous',
            'price_per_person' => 74,
        ]);
        // 5
        DB::table('activities')->insert([
            'partner_id' => 33,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Saint-Emilionnais Golf Club',
            'description' => 'Déjeuner au restaurant du club suivi d’un parcours 18 trous. Dans une vallée bordée de chênes et entourée par les vignes, un parcours d’exception vous attend, signé par l\'un des meilleurs architectes au monde, l\'américain Tom Doak. Laissez-vous emporter par la vue et profitez !',
            'time' => '00:15:00',
            'details' => 'Déjeuner au restaurant du club suivi d’un parcours 18 trous. Dans une vallée bordée de chênes et entourée par les vignes, un parcours d’exception vous attend, signé par l\'un des meilleurs architectes au monde, l\'américain Tom Doak. Laissez-vous emporter par la vue et profitez !',
            'price_per_person' => 15,
        ]);
        // 6
        DB::table('activities')->insert([
            'partner_id' => 34,
            'address_id' => 1,
            'image_id' => 1,
            'name' => 'Pau Golf Club 1856',
            'description' => 'A votre arrivée à Pau, votre séjour débute par un parcours de golf. Le Pau Golf Club 1856 à Billère vous accueille sur son parcours 18 trous, par 69 sur 5314m, tracé par l’architecte Willy Dunn. Il est situé dans le prolongement du parc du château, en bordure du Gave de Pau, dans un cadre superbe face aux montagnes et au milieu d\'arbres centenaires - licence française de golf obligatoire pour accéder au parcours',
            'time' => '00:45:00',
            'details' => 'A votre arrivée à Pau, votre séjour débute par un parcours de golf. Le Pau Golf Club 1856 à Billère vous accueille sur son parcours 18 trous, par 69 sur 5314m, tracé par l’architecte Willy Dunn. Il est situé dans le prolongement du parc du château, en bordure du Gave de Pau, dans un cadre superbe face aux montagnes et au milieu d\'arbres centenaires - licence française de golf obligatoire pour accéder au parcours',
            'price_per_person' => 85,
        ]);
    }
}
