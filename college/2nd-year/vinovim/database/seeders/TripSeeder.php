<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class TripSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 9,
            'title' => 'Week-end dégustation Côte de Beaune',
            'duration' => 2,
            'description' => 'Sancerre - Week-end vignoble',
        ]);
        // 2
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 12,
            'title' => 'Séjour vin en Côte de Nuits',
            'duration' => 7,
            'description' => 'Pérégrinez sur les sentiers de la Côte de Nuits, arrêtez-vous déjeuner chez des vignerons et découvrez les secrets de ces terroirs. Vous êtes sur les terres du Pinot Noir et de la Truffe de Bourgogne.',
        ]);
        // 3
        DB::table('trips')->insert([
            'participant_categories_id' => 7,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 13,
            'title' => 'Bordelais - Séjour oenologique',
            'duration' => 2,
            'description' => 'Du Médoc au Sauternais en passant par l’Entre-Deux-Mers, venez découvrir la diversité des vins bordelais, au travers des expériences oenologiques parfois surprenantes.',
        ]);
        // 4
        DB::table('trips')->insert([
            'participant_categories_id' => 6,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 2,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 2,
            'image_id' => 1,
            'title' => 'Séjour viticole Beaune',
            'duration' => 7,
            'description' => 'Au cœur des célèbres vignes de la Côte de Beaune, oubliez vos petites contrariétés, le temps semble ici s’être arrêté. Partez en séjour œnologique en Bourgogne, seul compte désormais votre bien-être.',
        ]);
        // 5
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 11,
            'title' => 'Séjour vigneron à Beblenheim',
            'duration' => 7,
            'description' => 'Passionnés par le vignoble alsacien ou curieux de le découvrir ? Partez à la découverte de ce vignoble atypique et à la rencontre de ses vignerons chaleureux.',
        ]);
        // 6
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 2,
            'title' => 'Week-end Grands Crus Kaysersberg En couple Entre amis Culture',
            'duration' => 2,
            'description' => 'La Vallée de Kaysersberg, berceau des Grands Crus d’Alsace, vous invite à découvrir de manière conviviale ses plus grands vins au cours de dégustations à la propriété en compagnie des vignerons',
        ]);
        // 7
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 3,
            'title' => 'Cadeau dégustation - Route d\'Alsace',
            'duration' => 7,
            'description' => 'Le séjour idéal à offrir pour découvrir la plus ancienne route des vins de France. Baladez-vous le long des villages typiques de la région et faites une halte à la propriété pour des dégustations de vins.',
        ]);
        // 8
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 4,
            'title' => 'Séjour découverte des vins du Rhône',
            'duration' => 5,
            'description' => 'Un coffret cadeau idéal pour découvrir les appellations incontournables de la Vallée du Rhône: Condrieu, Hermitage, Châteauneuf, Gigondas. Faites une halte à la propriété pour des dégustations de vins.',
        ]);
        // 9
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 2,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 2,
            'image_id' => 5,
            'title' => 'Week-end à offrir - Champagne',
            'duration' => 2,
            'description' => 'Week-end pour les amoureux des bulles qui souhaitent découvrir en toute convivialité les champagnes des petits producteurs tout en poussant les portes des grandes maisons reconnues dans le monde entier.',
        ]);
        // 10
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 6,
            'title' => 'Saint Aubin - Week-end œnologique',
            'duration' => 2,
            'description' => 'Confortablement installés à l’hôtel particulier de la maison Prosper Maufoux, vous découvrirez l’univers des vignerons bourguignons qui vous feront partager leur profond attachement à leur terre.',
        ]);
        // 11
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 7,
            'title' => 'Week-end d\'exception en Champagne',
            'duration' => 2,
            'description' => 'Olivier Poussier vous ouvre les portes des domaines Selosse et Jacquesson en Champagne. Des dégustations exclusives, sublimées par les accords mets et vins proposés par le meilleur sommelier du monde 2000.',
        ]);
        // 12
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 8,
            'title' => 'Séjour dégustation Saint-Emilion',
            'duration' => 4,
            'description' => 'Envie de vous rapprocher de la nature et de (re)découvrir le goût des produits. Prenez le temps d\'arpenter le vignoble, à l\'écart de l\'agitation de Bordeaux. Un week-end œnologique et biologique !',
        ]);
        // 13
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 10,
            'title' => 'Week-end d\'œnologie - Epernay',
            'duration' => 2,
            'description' => 'enez découvrir au grand air et dans une ambiance conviviale le vignoble champenois : balades en vélo, 2 CV, visites de caves, dégustations…un week-end dégustation de Champagne plein de surprises !',
        ]);
        // 14
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 2,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 2,
            'image_id' => 11,
            'title' => 'Week-end vin et golf - Beaune',
            'duration' => 2,
            'description' => 'La route des Grands Crus de Bourgogne est à elle seule évocatrice d’élégance. Associez-y le cadre du Golf de Chailly et son château du XVIème siècle, et évadez-vous le temps d’un séjour découverte de vins d’exception.',
        ]);
        // 15
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 12,
            'title' => 'Week-end vin Collioure',
            'duration' => 2,
            'description' => 'En compagnie d\'un guide, vous parcourez les sentiers viticoles autour de Collioure, de la vallée de l\'Agly, de la Côte Vermeille et du massif des Albères. Balades et dégustations !',
        ]);
        // 16
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 14,
            'title' => 'Trois visites en Médoc',
            'duration' => 3,
            'description' => 'Dans une ambiance détendue et familiale, venez découvrir l\'univers des vins de Bordeaux : visites de Châteaux, techniques de vinification et dégustations.',
        ]);
        // 17
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 15,
            'title' => 'Week-end viticole dans la Drôme',
            'duration' => 2,
            'description' => 'Entre vignes et champs de lavande, profitez d’un week-end évasion en Drôme Provençale. Rencontrez les artisans vignerons de la région lors vos visites et dégustations.',
        ]);
        // 18
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 16,
            'title' => 'Week-end œnologique Kaysersberg',
            'duration' => 2,
            'description' => 'Oubliez choucroute et autre carpe frite, la gastronomie alsacienne est aujourd’hui inventive, dynamique et exalte à merveille les qualités aromatiques des cépages alsaciens.',
        ]);
        // 19
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 2,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 2,
            'image_id' => 17,
            'title' => 'Week-end oenologique Suze-la Rousse',
            'duration' => 2,
            'description' => 'Parcourez la Drôme reconnue pour ses vins naturels subtils, ses paysages verdoyants et ses villages perchés dans les montagnes. Entre visites de propriétés viticoles converties à l’agriculture biologique et balade en vélo !',
        ]);
        // 20
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 1,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 1,
            'image_id' => 18,
            'title' => 'Saint-Emilion - Séjour oenologique',
            'duration' => 6,
            'description' => 'En immersion dans le vignoble bordelais, venez vivre le quotidien de viticulteurs passionnés. Hébergés au Château, vous vous y sentirez si bien qu’il y a cher à parier que vous y retournerez.',
        ]);
        // 21
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 3,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 3,
            'image_id' => 19,
            'title' => 'Séjour en amoureux Saint-Emilion',
            'duration' => 5,
            'description' => 'Nichés au cœur du vignoble de Saint-Emilion, lâchez prise et succombez aux charmes des vins du Château Siaurac et de nos établissements. Ne faites plus simplement plaisir, surprenez !',
        ]);
        // 22
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 2,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 2,
            'image_id' => 20,
            'title' => 'Séjour en amoureux en Côte-Rôtie',
            'duration' => 7,
            'description' => 'Au cœur de la célèbre Côte-Rôtie, évadez-vous le temps d’un séjour en amoureux, entre dégustations exclusives et cuisine raffinée. Découvrez les plus beaux trésors du Rhône septentrional.',
        ]);
        // 23
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 4,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 4,
            'image_id' => 9,
            'title' => 'Week-end dégustation en Province',
            'duration' => 2,
            'description' => 'Province - Week-end vignoble',
        ]);
        // 24
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 13,
            'title' => 'Séjour vin en Champagne',
            'duration' => 7,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 25
        DB::table('trips')->insert([
            'participant_categories_id' => 7,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 13,
            'title' => 'Val de Loire - Séjour oenologique',
            'duration' => 2,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 26
        DB::table('trips')->insert([
            'participant_categories_id' => 6,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 1,
            'title' => 'Séjour viticole Langdoc-Roussillon',
            'duration' => 7,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 27
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 1,
            'title' => 'Séjour vigneron dans le Rhône',
            'duration' => 7,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 28
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 2,
            'title' => 'Week-end Grands Crus Beaujolais',
            'duration' => 2,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 29
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 3,
            'title' => 'Cadeau dégustation - Route de Corse',
            'duration' => 7,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 30
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 4,
            'title' => 'Séjour découverte des vins du Sud-Ouest',
            'duration' => 5,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 31
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 5,
            'title' => 'Week-end à offrir - Jura',
            'duration' => 2,
            'description' => 'Jura - Week-end vignoble',
        ]);
        // 32
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 13,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 13,
            'image_id' => 6,
            'title' => 'Savoie - Week-end œnologique',
            'duration' => 2,
            'description' => 'Savoie - Week-end vignoble',
        ]);
        // 33
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 14,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 14,
            'image_id' => 7,
            'title' => 'Week-end d\'exception en Ile-de-France',
            'duration' => 2,
            'description' => 'Ile-de-France - Week-end vignoble',
        ]);
        // 34
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 15,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 15,
            'image_id' => 8,
            'title' => 'Séjour dégustation Catalogne',
            'duration' => 4,
            'description' => 'Catalogne - Week-end vignoble',
        ]);
        // 35
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 4,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 4,
            'image_id' => 10,
            'title' => 'Week-end d\'œnologie - Provence',
            'duration' => 2,
            'description' => 'Provence - Week-end vignoble',
        ]);
        // 36
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 11,
            'title' => 'Week-end vin et golf - Champagne',
            'duration' => 2,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 37
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 12,
            'title' => 'Week-end vin Val de Loire',
            'duration' => 2,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 38
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 14,
            'title' => 'Trois visites en Langdoc-Roussillon',
            'duration' => 3,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 39
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 15,
            'title' => 'Week-end viticole dans les Rhône',
            'duration' => 2,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 40
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 16,
            'title' => 'Week-end œnologique Beaujolais',
            'duration' => 2,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 41
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 17,
            'title' => 'Week-end oenologique Corse',
            'duration' => 2,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 42
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 18,
            'title' => 'Sud-Ouest - Séjour oenologique',
            'duration' => 6,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 43
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 19,
            'title' => 'Séjour en amoureux Jura',
            'duration' => 5,
            'description' => 'Jura - Week-end vignoble',
        ]);
        // 44
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 13,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 13,
            'image_id' => 20,
            'title' => 'Séjour en amoureux en Savoie',
            'duration' => 7,
            'description' => 'Savoie - Week-end vignoble',
        ]);
        // 45
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 9,
            'title' => 'Week-end dégustation en Champagne',
            'duration' => 2,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 46
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 13,
            'title' => 'Séjour vin en Val de Loire',
            'duration' => 7,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 47
        DB::table('trips')->insert([
            'participant_categories_id' => 7,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 13,
            'title' => 'Langdoc-Roussillon - Séjour oenologique',
            'duration' => 2,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 48
        DB::table('trips')->insert([
            'participant_categories_id' => 6,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 1,
            'title' => 'Séjour viticole Rhône',
            'duration' => 7,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 49
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 1,
            'title' => 'Séjour vigneron Beaujolais',
            'duration' => 7,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 50
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 2,
            'title' => 'Week-end Grands Crus Corse',
            'duration' => 2,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 51
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 3,
            'title' => 'Cadeau dégustation - Route du Sud-Ouest',
            'duration' => 7,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 52
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 4,
            'title' => 'Séjour découverte des vins du Jura',
            'duration' => 5,
            'description' => 'Jura - Week-end vignoble',
        ]);
        // 53
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 13,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 13,
            'image_id' => 5,
            'title' => 'Week-end à offrir - Savoie',
            'duration' => 2,
            'description' => 'Savoie - Week-end vignoble',
        ]);
        // 54
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 14,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 14,
            'image_id' => 6,
            'title' => 'Ile-de-France - Week-end œnologique',
            'duration' => 2,
            'description' => 'Ile-de-France - Week-end vignoble',
        ]);
        // 55
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 15,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 15,
            'image_id' => 7,
            'title' => 'Week-end d\'exception en Catalogne',
            'duration' => 2,
            'description' => 'Catalogne - Week-end vignoble',
        ]);
        // 56
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 4,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 4,
            'image_id' => 8,
            'title' => 'Séjour dégustation Provence',
            'duration' => 4,
            'description' => 'Provence - Week-end vignoble',
        ]);
        // 57
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 10,
            'title' => 'Week-end d\'œnologie - Champagne',
            'duration' => 2,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 58
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 11,
            'title' => 'Week-end vin et golf - Val de Loire',
            'duration' => 2,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 59
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 12,
            'title' => 'Week-end vin Langdoc-Roussillon',
            'duration' => 2,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 60
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 14,
            'title' => 'Trois visites en Rhône',
            'duration' => 3,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 61
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 15,
            'title' => 'Week-end viticole Beaujolais',
            'duration' => 2,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 62
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 16,
            'title' => 'Week-end œnologique Corse',
            'duration' => 2,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 63
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 17,
            'title' => 'Week-end oenologique du Sud-Ouest',
            'duration' => 2,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 64
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 18,
            'title' => 'Jura - Séjour oenologique',
            'duration' => 6,
            'description' => 'Jura - Week-end vignoble',
        ]);
        // 65
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 13,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 13,
            'image_id' => 19,
            'title' => 'Séjour en amoureux Savoie',
            'duration' => 5,
            'description' => 'Savoie - Week-end vignoble',
        ]);
        // 66
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 14,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 14,
            'image_id' => 20,
            'title' => 'Séjour en amoureux en Ile-de-France',
            'duration' => 7,
            'description' => 'Ile-de-France - Week-end vignoble',
        ]);
        // 67
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 15,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 15,
            'image_id' => 9,
            'title' => 'Week-end dégustation en Catalogne',
            'duration' => 2,
            'description' => 'Catalogne - Week-end vignoble',
        ]);
        // 68
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 4,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 4,
            'image_id' => 13,
            'title' => 'Séjour vin en Provence',
            'duration' => 7,
            'description' => 'Provence - Week-end vignoble',
        ]);
        // 69
        DB::table('trips')->insert([
            'participant_categories_id' => 7,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 13,
            'title' => 'Champagne - Séjour oenologique',
            'duration' => 2,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 70
        DB::table('trips')->insert([
            'participant_categories_id' => 6,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 1,
            'title' => 'Séjour viticole Val de Loire',
            'duration' => 7,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 71
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 1,
            'title' => 'Séjour vigneron Langdoc-Roussillon',
            'duration' => 7,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 72
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 2,
            'title' => 'Week-end Grands Crus Rhône',
            'duration' => 2,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 73
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 3,
            'title' => 'Cadeau dégustation - Route de Beaujolais',
            'duration' => 7,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 74
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 4,
            'title' => 'Séjour découverte des vins de Corse',
            'duration' => 5,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 75
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 5,
            'title' => 'Week-end à offrir - Sud-Ouest',
            'duration' => 2,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 76
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 6,
            'title' => 'Jura - Week-end œnologique',
            'duration' => 2,
            'description' => 'Jura - Week-end vignoble',
        ]);
        // 77
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 13,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 13,
            'image_id' => 7,
            'title' => 'Week-end d\'exception en Savoie',
            'duration' => 2,
            'description' => 'Savoie - Week-end vignoble',
        ]);
        // 78
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 14,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 14,
            'image_id' => 8,
            'title' => 'Séjour dégustation Ile-de-France',
            'duration' => 4,
            'description' => 'Ile-de-France - Week-end vignoble',
        ]);
        // 79
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 15,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 15,
            'image_id' => 10,
            'title' => 'Week-end d\'œnologie - Catalogne',
            'duration' => 2,
            'description' => 'Catalogne - Week-end vignoble',
        ]);
        // 80
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 4,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 4,
            'image_id' => 11,
            'title' => 'Week-end vin et golf - Provence',
            'duration' => 2,
            'description' => 'Provence - Week-end vignoble',
        ]);
        // 81
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 5,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 5,
            'image_id' => 12,
            'title' => 'Week-end vin Champagne',
            'duration' => 2,
            'description' => 'Champagne - Week-end vignoble',
        ]);
        // 82
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 6,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 6,
            'image_id' => 14,
            'title' => 'Trois visites en Val de Loire',
            'duration' => 3,
            'description' => 'Val de Loire - Week-end vignoble',
        ]);
        // 83
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 7,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 7,
            'image_id' => 15,
            'title' => 'Week-end viticole Langdoc-Roussillon',
            'duration' => 2,
            'description' => 'Langdoc-Roussillon - Week-end vignoble',
        ]);
        // 84
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 8,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 8,
            'image_id' => 16,
            'title' => 'Week-end œnologique Rhône',
            'duration' => 2,
            'description' => 'Rhône - Week-end vignoble',
        ]);
        // 85
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 9,
            'theme_id' => 3,
            'destination_id' => 1,
            'wine_trail_id' => 9,
            'image_id' => 17,
            'title' => 'Week-end oenologique du Beaujolais',
            'duration' => 2,
            'description' => 'Beaujolais - Week-end vignoble',
        ]);
        // 86
        DB::table('trips')->insert([
            'participant_categories_id' => 2,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 10,
            'theme_id' => 4,
            'destination_id' => 1,
            'wine_trail_id' => 10,
            'image_id' => 18,
            'title' => 'Corse - Séjour oenologique',
            'duration' => 6,
            'description' => 'Corse - Week-end vignoble',
        ]);
        // 87
        DB::table('trips')->insert([
            'participant_categories_id' => 1,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 11,
            'theme_id' => 2,
            'destination_id' => 1,
            'wine_trail_id' => 11,
            'image_id' => 19,
            'title' => 'Séjour en amoureux Sud-Ouest',
            'duration' => 5,
            'description' => 'Sud-Ouest - Week-end vignoble',
        ]);
        // 88
        DB::table('trips')->insert([
            'participant_categories_id' => 3,
            'trip_categories_id' => 1,
            'vineyard_categories_id' => 12,
            'theme_id' => 1,
            'destination_id' => 1,
            'wine_trail_id' => 12,
            'image_id' => 20,
            'title' => 'Séjour en amoureux en Jura',
            'duration' => 7,
            'description' => 'Jura - Week-end vignoble',
        ]);
    }
}
