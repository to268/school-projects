<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class StageSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('stages')->insert([
            'trip_id' => 1,
            'image_id' => 2,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 2
        DB::table('stages')->insert([
            'trip_id' => 1,
            'image_id' => 2,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 3
        DB::table('stages')->insert([
            'trip_id' => 2,
            'image_id' => 3,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 4
        DB::table('stages')->insert([
            'trip_id' => 2,
            'image_id' => 4,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 5
        DB::table('stages')->insert([
            'trip_id' => 2,
            'image_id' => 5,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 6
        DB::table('stages')->insert([
            'trip_id' => 3,
            'image_id' => 6,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 7
        DB::table('stages')->insert([
            'trip_id' => 3,
            'image_id' => 7,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 8
        DB::table('stages')->insert([
            'trip_id' => 4,
            'image_id' => 8,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 9
        DB::table('stages')->insert([
            'trip_id' => 4,
            'image_id' => 9,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 10
        DB::table('stages')->insert([
            'trip_id' => 5,
            'image_id' => 10,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 11
        DB::table('stages')->insert([
            'trip_id' => 5,
            'image_id' => 11,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 12
        DB::table('stages')->insert([
            'trip_id' => 6,
            'image_id' => 12,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 13
        DB::table('stages')->insert([
            'trip_id' => 6,
            'image_id' => 13,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 14
        DB::table('stages')->insert([
            'trip_id' => 6,
            'image_id' => 14,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 15
        DB::table('stages')->insert([
            'trip_id' => 7,
            'image_id' => 15,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 16
        DB::table('stages')->insert([
            'trip_id' => 7,
            'image_id' => 16,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 17
        DB::table('stages')->insert([
            'trip_id' => 7,
            'image_id' => 17,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 18
        DB::table('stages')->insert([
            'trip_id' => 8,
            'image_id' => 18,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 19
        DB::table('stages')->insert([
            'trip_id' => 8,
            'image_id' => 19,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 20
        DB::table('stages')->insert([
            'trip_id' => 8,
            'image_id' => 20,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 21
        DB::table('stages')->insert([
            'trip_id' => 9,
            'image_id' => 21,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 22
        DB::table('stages')->insert([
            'trip_id' => 9,
            'image_id' => 22,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 23
        DB::table('stages')->insert([
            'trip_id' => 9,
            'image_id' => 23,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 24
        DB::table('stages')->insert([
            'trip_id' => 10,
            'image_id' => 24,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 25
        DB::table('stages')->insert([
            'trip_id' => 10,
            'image_id' => 25,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 26
        DB::table('stages')->insert([
            'trip_id' => 11,
            'image_id' => 26,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 27
        DB::table('stages')->insert([
            'trip_id' => 11,
            'image_id' => 27,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 28
        DB::table('stages')->insert([
            'trip_id' => 11,
            'image_id' => 28,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 29
        DB::table('stages')->insert([
            'trip_id' => 12,
            'image_id' => 29,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 30
        DB::table('stages')->insert([
            'trip_id' => 12,
            'image_id' => 30,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 31
        DB::table('stages')->insert([
            'trip_id' => 13,
            'image_id' => 31,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 32
        DB::table('stages')->insert([
            'trip_id' => 13,
            'image_id' => 32,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 33
        DB::table('stages')->insert([
            'trip_id' => 13,
            'image_id' => 33,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 34
        DB::table('stages')->insert([
            'trip_id' => 14,
            'image_id' => 34,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 35
        DB::table('stages')->insert([
            'trip_id' => 14,
            'image_id' => 35,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 36
        DB::table('stages')->insert([
            'trip_id' => 15,
            'image_id' => 36,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 37
        DB::table('stages')->insert([
            'trip_id' => 15,
            'image_id' => 37,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 38
        DB::table('stages')->insert([
            'trip_id' => 16,
            'image_id' => 38,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 39
        DB::table('stages')->insert([
            'trip_id' => 17,
            'image_id' => 39,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 40
        DB::table('stages')->insert([
            'trip_id' => 17,
            'image_id' => 40,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 41
        DB::table('stages')->insert([
            'trip_id' => 17,
            'image_id' => 41,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 42
        DB::table('stages')->insert([
            'trip_id' => 17,
            'image_id' => 42,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 43
        DB::table('stages')->insert([
            'trip_id' => 18,
            'image_id' => 43,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 44
        DB::table('stages')->insert([
            'trip_id' => 18,
            'image_id' => 44,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 45
        DB::table('stages')->insert([
            'trip_id' => 18,
            'image_id' => 45,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 46
        DB::table('stages')->insert([
            'trip_id' => 19,
            'image_id' => 46,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 47
        DB::table('stages')->insert([
            'trip_id' => 19,
            'image_id' => 47,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 48
        DB::table('stages')->insert([
            'trip_id' => 19,
            'image_id' => 48,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 49
        DB::table('stages')->insert([
            'trip_id' => 20,
            'image_id' => 49,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 50
        DB::table('stages')->insert([
            'trip_id' => 20,
            'image_id' => 50,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 51
        DB::table('stages')->insert([
            'trip_id' => 20,
            'image_id' => 51,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 52
        DB::table('stages')->insert([
            'trip_id' => 21,
            'image_id' => 52,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 53
        DB::table('stages')->insert([
            'trip_id' => 21,
            'image_id' => 53,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 54
        DB::table('stages')->insert([
            'trip_id' => 22,
            'image_id' => 54,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 55
        DB::table('stages')->insert([
            'trip_id' => 22,
            'image_id' => 55,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 56
        DB::table('stages')->insert([
            'trip_id' => 22,
            'image_id' => 56,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 57
        DB::table('stages')->insert([
            'trip_id' => 23,
            'image_id' => 57,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 58
        DB::table('stages')->insert([
            'trip_id' => 23,
            'image_id' => 58,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 59
        DB::table('stages')->insert([
            'trip_id' => 24,
            'image_id' => 59,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 60
        DB::table('stages')->insert([
            'trip_id' => 24,
            'image_id' => 60,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 61
        DB::table('stages')->insert([
            'trip_id' => 24,
            'image_id' => 61,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 62
        DB::table('stages')->insert([
            'trip_id' => 25,
            'image_id' => 62,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 63
        DB::table('stages')->insert([
            'trip_id' => 25,
            'image_id' => 63,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 64
        DB::table('stages')->insert([
            'trip_id' => 26,
            'image_id' => 64,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 65
        DB::table('stages')->insert([
            'trip_id' => 26,
            'image_id' => 65,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 66
        DB::table('stages')->insert([
            'trip_id' => 27,
            'image_id' => 66,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 67
        DB::table('stages')->insert([
            'trip_id' => 27,
            'image_id' => 67,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 68
        DB::table('stages')->insert([
            'trip_id' => 28,
            'image_id' => 68,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 69
        DB::table('stages')->insert([
            'trip_id' => 28,
            'image_id' => 69,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 70
        DB::table('stages')->insert([
            'trip_id' => 28,
            'image_id' => 70,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 71
        DB::table('stages')->insert([
            'trip_id' => 29,
            'image_id' => 71,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 72
        DB::table('stages')->insert([
            'trip_id' => 29,
            'image_id' => 72,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 73
        DB::table('stages')->insert([
            'trip_id' => 29,
            'image_id' => 73,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 74
        DB::table('stages')->insert([
            'trip_id' => 30,
            'image_id' => 74,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 75
        DB::table('stages')->insert([
            'trip_id' => 30,
            'image_id' => 75,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 76
        DB::table('stages')->insert([
            'trip_id' => 30,
            'image_id' => 1,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 77
        DB::table('stages')->insert([
            'trip_id' => 31,
            'image_id' => 2,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 78
        DB::table('stages')->insert([
            'trip_id' => 31,
            'image_id' => 3,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 79
        DB::table('stages')->insert([
            'trip_id' => 31,
            'image_id' => 4,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 80
        DB::table('stages')->insert([
            'trip_id' => 32,
            'image_id' => 5,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 81
        DB::table('stages')->insert([
            'trip_id' => 32,
            'image_id' => 6,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 82
        DB::table('stages')->insert([
            'trip_id' => 33,
            'image_id' => 7,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 83
        DB::table('stages')->insert([
            'trip_id' => 33,
            'image_id' => 8,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 84
        DB::table('stages')->insert([
            'trip_id' => 33,
            'image_id' => 9,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 85
        DB::table('stages')->insert([
            'trip_id' => 34,
            'image_id' => 10,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 86
        DB::table('stages')->insert([
            'trip_id' => 34,
            'image_id' => 11,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 87
        DB::table('stages')->insert([
            'trip_id' => 35,
            'image_id' => 12,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 88
        DB::table('stages')->insert([
            'trip_id' => 35,
            'image_id' => 13,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 89
        DB::table('stages')->insert([
            'trip_id' => 35,
            'image_id' => 14,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 90
        DB::table('stages')->insert([
            'trip_id' => 36,
            'image_id' => 15,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 91
        DB::table('stages')->insert([
            'trip_id' => 36,
            'image_id' => 16,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 92
        DB::table('stages')->insert([
            'trip_id' => 37,
            'image_id' => 17,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 93
        DB::table('stages')->insert([
            'trip_id' => 37,
            'image_id' => 18,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 94
        DB::table('stages')->insert([
            'trip_id' => 38,
            'image_id' => 19,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 95
        DB::table('stages')->insert([
            'trip_id' => 39,
            'image_id' => 20,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 96
        DB::table('stages')->insert([
            'trip_id' => 39,
            'image_id' => 21,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 97
        DB::table('stages')->insert([
            'trip_id' => 39,
            'image_id' => 22,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 98
        DB::table('stages')->insert([
            'trip_id' => 39,
            'image_id' => 23,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 99
        DB::table('stages')->insert([
            'trip_id' => 40,
            'image_id' => 24,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 100
        DB::table('stages')->insert([
            'trip_id' => 40,
            'image_id' => 25,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 101
        DB::table('stages')->insert([
            'trip_id' => 40,
            'image_id' => 26,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 102
        DB::table('stages')->insert([
            'trip_id' => 41,
            'image_id' => 27,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 103
        DB::table('stages')->insert([
            'trip_id' => 41,
            'image_id' => 28,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 104
        DB::table('stages')->insert([
            'trip_id' => 41,
            'image_id' => 29,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 105
        DB::table('stages')->insert([
            'trip_id' => 42,
            'image_id' => 30,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 106
        DB::table('stages')->insert([
            'trip_id' => 42,
            'image_id' => 31,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 107
        DB::table('stages')->insert([
            'trip_id' => 42,
            'image_id' => 32,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 108
        DB::table('stages')->insert([
            'trip_id' => 43,
            'image_id' => 33,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 109
        DB::table('stages')->insert([
            'trip_id' => 43,
            'image_id' => 34,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 110
        DB::table('stages')->insert([
            'trip_id' => 44,
            'image_id' => 35,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 111
        DB::table('stages')->insert([
            'trip_id' => 44,
            'image_id' => 36,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 112
        DB::table('stages')->insert([
            'trip_id' => 44,
            'image_id' => 37,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 1
        DB::table('stages')->insert([
            'trip_id' => 45,
            'image_id' => 38,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 2
        DB::table('stages')->insert([
            'trip_id' => 45,
            'image_id' => 39,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 3
        DB::table('stages')->insert([
            'trip_id' => 46,
            'image_id' => 40,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 4
        DB::table('stages')->insert([
            'trip_id' => 46,
            'image_id' => 41,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 5
        DB::table('stages')->insert([
            'trip_id' => 46,
            'image_id' => 42,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 6
        DB::table('stages')->insert([
            'trip_id' => 47,
            'image_id' => 43,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 7
        DB::table('stages')->insert([
            'trip_id' => 47,
            'image_id' => 44,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 8
        DB::table('stages')->insert([
            'trip_id' => 48,
            'image_id' => 45,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 9
        DB::table('stages')->insert([
            'trip_id' => 48,
            'image_id' => 46,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 10
        DB::table('stages')->insert([
            'trip_id' => 49,
            'image_id' => 47,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 11
        DB::table('stages')->insert([
            'trip_id' => 49,
            'image_id' => 48,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 12
        DB::table('stages')->insert([
            'trip_id' => 50,
            'image_id' => 49,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 13
        DB::table('stages')->insert([
            'trip_id' => 50,
            'image_id' => 50,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 14
        DB::table('stages')->insert([
            'trip_id' => 50,
            'image_id' => 51,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 15
        DB::table('stages')->insert([
            'trip_id' => 51,
            'image_id' => 52,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 16
        DB::table('stages')->insert([
            'trip_id' => 51,
            'image_id' => 53,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 17
        DB::table('stages')->insert([
            'trip_id' => 51,
            'image_id' => 54,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 18
        DB::table('stages')->insert([
            'trip_id' => 52,
            'image_id' => 55,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 19
        DB::table('stages')->insert([
            'trip_id' => 52,
            'image_id' => 56,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 20
        DB::table('stages')->insert([
            'trip_id' => 52,
            'image_id' => 57,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 21
        DB::table('stages')->insert([
            'trip_id' => 53,
            'image_id' => 58,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 22
        DB::table('stages')->insert([
            'trip_id' => 53,
            'image_id' => 59,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 23
        DB::table('stages')->insert([
            'trip_id' => 53,
            'image_id' => 60,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 24
        DB::table('stages')->insert([
            'trip_id' => 54,
            'image_id' => 61,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 25
        DB::table('stages')->insert([
            'trip_id' => 54,
            'image_id' => 62,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 26
        DB::table('stages')->insert([
            'trip_id' => 55,
            'image_id' => 63,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 27
        DB::table('stages')->insert([
            'trip_id' => 55,
            'image_id' => 64,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 28
        DB::table('stages')->insert([
            'trip_id' => 55,
            'image_id' => 65,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 29
        DB::table('stages')->insert([
            'trip_id' => 56,
            'image_id' => 66,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 30
        DB::table('stages')->insert([
            'trip_id' => 56,
            'image_id' => 67,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 31
        DB::table('stages')->insert([
            'trip_id' => 57,
            'image_id' => 68,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 32
        DB::table('stages')->insert([
            'trip_id' => 57,
            'image_id' => 69,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 33
        DB::table('stages')->insert([
            'trip_id' => 57,
            'image_id' => 70,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 34
        DB::table('stages')->insert([
            'trip_id' => 58,
            'image_id' => 71,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 35
        DB::table('stages')->insert([
            'trip_id' => 59,
            'image_id' => 72,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 36
        DB::table('stages')->insert([
            'trip_id' => 59,
            'image_id' => 73,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 37
        DB::table('stages')->insert([
            'trip_id' => 60,
            'image_id' => 74,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 38
        DB::table('stages')->insert([
            'trip_id' => 60,
            'image_id' => 75,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 39
        DB::table('stages')->insert([
            'trip_id' => 61,
            'image_id' => 1,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 40
        DB::table('stages')->insert([
            'trip_id' => 61,
            'image_id' => 2,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 41
        DB::table('stages')->insert([
            'trip_id' => 61,
            'image_id' => 3,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 42
        DB::table('stages')->insert([
            'trip_id' => 61,
            'image_id' => 4,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 43
        DB::table('stages')->insert([
            'trip_id' => 62,
            'image_id' => 5,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 44
        DB::table('stages')->insert([
            'trip_id' => 62,
            'image_id' => 6,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 45
        DB::table('stages')->insert([
            'trip_id' => 62,
            'image_id' => 7,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 46
        DB::table('stages')->insert([
            'trip_id' => 63,
            'image_id' => 8,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 47
        DB::table('stages')->insert([
            'trip_id' => 63,
            'image_id' => 9,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 48
        DB::table('stages')->insert([
            'trip_id' => 63,
            'image_id' => 10,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 49
        DB::table('stages')->insert([
            'trip_id' => 64,
            'image_id' => 11,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 50
        DB::table('stages')->insert([
            'trip_id' => 64,
            'image_id' => 12,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 51
        DB::table('stages')->insert([
            'trip_id' => 64,
            'image_id' => 13,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 52
        DB::table('stages')->insert([
            'trip_id' => 65,
            'image_id' => 14,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 53
        DB::table('stages')->insert([
            'trip_id' => 65,
            'image_id' => 15,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 54
        DB::table('stages')->insert([
            'trip_id' => 66,
            'image_id' => 16,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 55
        DB::table('stages')->insert([
            'trip_id' => 66,
            'image_id' => 17,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 56
        DB::table('stages')->insert([
            'trip_id' => 66,
            'image_id' => 18,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 57
        DB::table('stages')->insert([
            'trip_id' => 67,
            'image_id' => 19,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 58
        DB::table('stages')->insert([
            'trip_id' => 67,
            'image_id' => 20,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 59
        DB::table('stages')->insert([
            'trip_id' => 68,
            'image_id' => 21,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 60
        DB::table('stages')->insert([
            'trip_id' => 68,
            'image_id' => 22,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 61
        DB::table('stages')->insert([
            'trip_id' => 68,
            'image_id' => 23,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 62
        DB::table('stages')->insert([
            'trip_id' => 69,
            'image_id' => 24,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 63
        DB::table('stages')->insert([
            'trip_id' => 69,
            'image_id' => 25,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 64
        DB::table('stages')->insert([
            'trip_id' => 70,
            'image_id' => 26,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 65
        DB::table('stages')->insert([
            'trip_id' => 70,
            'image_id' => 27,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 66
        DB::table('stages')->insert([
            'trip_id' => 71,
            'image_id' => 28,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 67
        DB::table('stages')->insert([
            'trip_id' => 71,
            'image_id' => 29,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 68
        DB::table('stages')->insert([
            'trip_id' => 72,
            'image_id' => 30,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 69
        DB::table('stages')->insert([
            'trip_id' => 72,
            'image_id' => 31,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 70
        DB::table('stages')->insert([
            'trip_id' => 72,
            'image_id' => 32,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 71
        DB::table('stages')->insert([
            'trip_id' => 73,
            'image_id' => 33,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 72
        DB::table('stages')->insert([
            'trip_id' => 73,
            'image_id' => 34,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 73
        DB::table('stages')->insert([
            'trip_id' => 73,
            'image_id' => 35,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 74
        DB::table('stages')->insert([
            'trip_id' => 74,
            'image_id' => 36,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 75
        DB::table('stages')->insert([
            'trip_id' => 74,
            'image_id' => 37,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 76
        DB::table('stages')->insert([
            'trip_id' => 74,
            'image_id' => 38,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 77
        DB::table('stages')->insert([
            'trip_id' => 75,
            'image_id' => 39,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 78
        DB::table('stages')->insert([
            'trip_id' => 75,
            'image_id' => 40,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 79
        DB::table('stages')->insert([
            'trip_id' => 75,
            'image_id' => 75,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 80
        DB::table('stages')->insert([
            'trip_id' => 76,
            'image_id' => 41,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 81
        DB::table('stages')->insert([
            'trip_id' => 76,
            'image_id' => 42,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 82
        DB::table('stages')->insert([
            'trip_id' => 77,
            'image_id' => 43,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 83
        DB::table('stages')->insert([
            'trip_id' => 77,
            'image_id' => 44,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 84
        DB::table('stages')->insert([
            'trip_id' => 77,
            'image_id' => 45,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
        // 85
        DB::table('stages')->insert([
            'trip_id' => 78,
            'image_id' => 46,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\'un déjeuner à la table d\'hôte du domaine avec dégustation commentée des vins Olivier Leflaive',
            'url' => '',
            'movie_url' => '',
        ]);
        // 86
        DB::table('stages')->insert([
            'trip_id' => 78,
            'image_id' => 47,
            'description' => 'Vous poursuivez vos dégustations de vins réputés en Bourgogne avec un rendez-vous dans la matinée au magnifique Château de Corton-André à Aloxe-Corton, pour une dégustation des vins Pierre André',
            'url' => '',
            'movie_url' => '',
        ]);
        // 87
        DB::table('stages')->insert([
            'trip_id' => 79,
            'image_id' => 48,
            'description' => 'A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d\’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l\'oenotourisme en Bourgogne',
            'url' => '',
            'movie_url' => '',
        ]);
        // 88
        DB::table('stages')->insert([
            'trip_id' => 79,
            'image_id' => 49,
            'description' => 'Après un copieux petit déjeuner, vous participez à un atelier oenologique et une balade découverte de la vigne et du vin au Clos de Bourgogne, en compagnie d\'une oenologue diplômée, au départ de Gevrey-Chambertin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 89
        DB::table('stages')->insert([
            'trip_id' => 79,
            'image_id' => 50,
            'description' => 'En fin de matinée, vous visitez une truffière et découvrez les secrets du cavage. La visite se termine par un déjeuner gourmand autour de la truffe accompagné d\'un verre de vin',
            'url' => '',
            'movie_url' => '',
        ]);
        // 90
        DB::table('stages')->insert([
            'trip_id' => 80,
            'image_id' => 51,
            'description' => 'A votre arrivée à Puligny-Montrachet, vous avez rendez-vous à la Maison Olivier Leflaive pour une dégustation commentée de trois vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 91
        DB::table('stages')->insert([
            'trip_id' => 80,
            'image_id' => 52,
            'description' => 'Rendez-vous à Aloxe-Corton en fin de matinée pour une dégustation des vins du Château Corton C',
            'url' => '',
            'movie_url' => '',
        ]);
        // 92
        DB::table('stages')->insert([
            'trip_id' => 81,
            'image_id' => 53,
            'description' => 'A votre arrivée à Puligny-Montrachet, au cœur de la Bourgogne des grands vins, vous avez rendez-vous à la Maison Olivier Leflaive pour une visite de la propriété suivie d\’un déjeuner à la table d\’hôte du domaine avec dégustation commentée des vins Olivier Leflaive par un sommelier de la maison',
            'url' => '',
            'movie_url' => '',
        ]);
        // 93
        DB::table('stages')->insert([
            'trip_id' => 81,
            'image_id' => 54,
            'description' => 'Vous êtes attendus à Beaune pour une visite de la cave du Domaine Debray, suivie d\'une dégustation commentée des vins de la propriété',
            'url' => '',
            'movie_url' => '',
        ]);
        // 94
        DB::table('stages')->insert([
            'trip_id' => 82,
            'image_id' => 55,
            'description' => 'Votre séjour débute par la visite d\'une production artisanale de safran, l\'or rouge aux mille vertus - si activité non disponible, bouteille offerte',
            'url' => '',
            'movie_url' => '',
        ]);
        // 95
        DB::table('stages')->insert([
            'trip_id' => 83,
            'image_id' => 56,
            'description' => 'Direction Saint-Emilion pour une journée à la découverte de cette cité classé au patrimoine mondial de l’UNESCO',
            'url' => '',
            'movie_url' => '',
        ]);
        // 96
        DB::table('stages')->insert([
            'trip_id' => 83,
            'image_id' => 57,
            'description' => 'A votre arrivée dans le Médoc, vous prenez la direction du Château du Taillan, magnifique propriété familiale datant du XVIIème siècle, d\’architecture classique, abritant des caves souterraines répertoriées aux Monuments Historiques de France.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 97
        DB::table('stages')->insert([
            'trip_id' => 83,
            'image_id' => 58,
            'description' => 'Votre journée sur les chemins du Médoc se poursuit au Château Baudan, situé dans l\’appellation de Listrac-Médoc. Alain et Sylvie Blasquez, ainsi que leurs filles, vous accueillent pour une initiation à la dégustation à travers une approche originale et ludique : la dégustation à l\’aveugle. Faites appel à vos sens de la vue, l\’odorat, le goût, et apprenez à deviner un maximum d\’éléments sur le vin dégusté.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 98
        DB::table('stages')->insert([
            'trip_id' => 83,
            'image_id' => 59,
            'description' => 'Pause déjeuner au cœur des vignobles, au restaurant Au Marquis de Terme. Le chef Grégory Coutanceau et le Château Marquis de Terme, Grand Cru classé de l\’appellation Margaux, s\’associent pour vous proposer un concentré d\'art de vivre à la française, autour du vin, de la gastronomie de l\’hospitalité - Menu hors boisson.',
            'url' => '',
            'movie_url' => '',
        ]);
        // 99
        DB::table('stages')->insert([
            'trip_id' => 84,
            'image_id' => 60,
            'description' => 'A votre arrivée en Bourgogne, vous empruntez la route des Grands Crus en direction de Beaune. Vous vous installez dans votre chambre au sein de l\’Hostellerie Le Cèdre, magnifique hôtel 5\* situé dans un écrin de verdure aux arbres centenaires',
            'url' => '',
            'movie_url' => '',
        ]);
        // 100
        DB::table('stages')->insert([
            'trip_id' => 84,
            'image_id' => 61,
            'description' => 'Départ pour le Golf du Château de Chailly, à Pouilly en Auxois et déjeuner au restaurant Le Rubillon',
            'url' => '',
            'movie_url' => '',
        ]);
        // 101
        DB::table('stages')->insert([
            'trip_id' => 84,
            'image_id' => 62,
            'description' => 'Vous empruntez la route des Grands Crus et sillonnez le vignoble en direction d\'Aloxe-Corton, au Château Corton C. pour une dégustation des vins du domaine',
            'url' => '',
            'movie_url' => '',
        ]);
        // 102
        DB::table('stages')->insert([
            'trip_id' => 85,
            'image_id' => 63,
            'description' => 'A votre arrivée en Alsace, vous êtes attendus à Hunawihr, où vous avez rendez-vous pour une visite du domaine François Schwach et une dégustation de leurs spécialités de Crémants d\’Alsace',
            'url' => '',
            'movie_url' => '',
        ]);
        // 103
        DB::table('stages')->insert([
            'trip_id' => 85,
            'image_id' => 64,
            'description' => 'Matinée en immersion au Domaine Schoenheitz, vous y êtes attendus pour une visite de la cave suivie d\'une dégustation gastronomique commentée par le vigneron : dégustation d\'une sélection de 5 vins, Kougelhopf et Munster fermier au lait cru',
            'url' => '',
            'movie_url' => '',
        ]);
        // 104
        DB::table('stages')->insert([
            'trip_id' => 85,
            'image_id' => 65,
            'description' => 'Dans la matinée, vous partez à la découverte de Saint-Hippolyte, magnifique village alsacien avec ses maisons à colombages séparées par des venelles',
            'url' => '',
            'movie_url' => '',
        ]);
        // 105
        DB::table('stages')->insert([
            'trip_id' => 86,
            'image_id' => 66,
            'description' => 'A votre arrivée à Bordeaux, vous empruntez la route des Châteaux en direction de Margaux',
            'url' => '',
            'movie_url' => '',
        ]);
        // 106
        DB::table('stages')->insert([
            'trip_id' => 86,
            'image_id' => 67,
            'description' => 'Départ pour le Golf du Médoc et déjeuner au restaurant Le Club au Golf du Médoc',
            'url' => '',
            'movie_url' => '',
        ]);
        // 107
        DB::table('stages')->insert([
            'trip_id' => 86,
            'image_id' => 68,
            'description' => 'Parcours 18 trous au Golf de Lacanau',
            'url' => '',
            'movie_url' => '',
        ]);
        // 108
        DB::table('stages')->insert([
            'trip_id' => 87,
            'image_id' => 69,
            'description' => 'A votre arrivée en Champagne, vous prenez la direction d\'Epernay où votre guide vous attend pour débuter une balade d\’une demi-journée en VTT au cœur du vignoble champenois. Selon votre niveau (et votre motivation !), votre guide s\’adaptera et vous fera découvrir les plus beaux points de vue de la région. Une halte dégustation chez un vigneron champenois ainsi qu\’un déjeuner clôtureront cette petite escapade sportive et gourmande',
            'url' => '',
            'movie_url' => '',
        ]);
        // 109
        DB::table('stages')->insert([
            'trip_id' => 87,
            'image_id' => 70,
            'description' => 'Dans la matinée, vous partez en petit groupe pour une balade en Estafette où vous sera conté l\’histoire de la région Champagne et du travail de la vigne selon les saisons. En point d\’orgue de la balade, vous profiterez d\’une dégustation d\’un verre de Champagne au milieu des vignes',
            'url' => '',
            'movie_url' => '',
        ]);
        // 110
        DB::table('stages')->insert([
            'trip_id' => 88,
            'image_id' => 71,
            'description' => 'A votre arrivée à Saint-Emilion,vous vous installez dans l\’une des magnifiques chambres d\'une propriété viticole. Vous découvrez une demeure entourée de ses vignes, pleine de charme et de raffinement',
            'url' => '',
            'movie_url' => '',
        ]);
        // 111
        DB::table('stages')->insert([
            'trip_id' => 88,
            'image_id' => 72,
            'description' => 'En matinée, départ pour Saint-Emilion, à la sortie du village vous empruntez une petite route sinueuse qui monte jusqu\'au Château de Ferrand, Grand Cru Classé depuis 2012',
            'url' => '',
            'movie_url' => '',
        ]);
        // 112
        DB::table('stages')->insert([
            'trip_id' => 88,
            'image_id' => 73,
            'description' => 'Votre week-end oenologie se termine par une journée libre à Saint-Emilion, cité médiévale classée au Patrimoine Mondial de l\'Unesco',
            'url' => '',
            'movie_url' => '',
        ]);
    }
}
