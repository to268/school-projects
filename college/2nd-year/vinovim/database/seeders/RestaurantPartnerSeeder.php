<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class RestaurantPartnerSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('restaurant_partners')->insert([
            'id' => 1,
            'partner_id' => 1,
            'address_id' => 1,
            'name' => 'Maison Olivier Leflaive',
            'email' => 'contact@olivier-leflaive.com',
            'phone' => '0380219527',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 2,
            'partner_id' => 2,
            'address_id' => 1,
            'name' => 'La Maison Rouge',
            'email' => 'info@lamaisonrouge-beaune.com',
            'phone' => '0664372750',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 3,
            'partner_id' => 3,
            'address_id' => 1,
            'name' => 'Château en Entre-deux-Mers',
            'email' => '',
            'phone' => '',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 4,
            'partner_id' => 4,
            'address_id' => 1,
            'name' => 'Hosphonelerie de Levernois',
            'email' => 'levernois@relaischateaux.com',
            'phone' => '0380247358',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 5,
            'partner_id' => 5,
            'address_id' => 1,
            'name' => 'Maison Prosper Maufoux',
            'email' => 'pascale.rifaux@prosper-maufoux.com',
            'phone' => '0380206871',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 6,
            'partner_id' => 6,
            'address_id' => 1,
            'name' => 'Château La Romaningue',
            'email' => 'contact@laromaningue.fr',
            'phone' => '0535546295',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 7,
            'partner_id' => 7,
            'address_id' => 1,
            'name' => 'Hosphonelerie Le Cèdre',
            'email' => 'reservation@cedrebeaune.com',
            'phone' => '0380240101',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 8,
            'partner_id' => 8,
            'address_id' => 1,
            'name' => 'Hosphonelerie de Levernois',
            'email' => 'levernois@relaischateaux.com',
            'phone' => '0380247358',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 9,
            'partner_id' => 9,
            'address_id' => 1,
            'name' => 'Demeure d\'Antan',
            'email' => '',
            'phone' => '0616963447',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 10,
            'partner_id' => 10,
            'address_id' => 1,
            'name' => 'Château du Tertre',
            'email' => '',
            'phone' => '0557885252',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 11,
            'partner_id' => 11,
            'address_id' => 1,
            'name' => 'Golf du Médoc',
            'email' => 'contact@golfdumedocresort.com',
            'phone' => '0556703131',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 12,
            'partner_id' => 12,
            'address_id' => 1,
            'name' => 'Château Corton C.',
            'email' => '',
            'phone' => '0380262879',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 13,
            'partner_id' => 13,
            'address_id' => 1,
            'name' => 'Prosper Maufoux - Château de Saint-Aubin',
            'email' => 'pascale.rifaux@prosper-maufoux.com',
            'phone' => '0380206871',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 14,
            'partner_id' => 14,
            'address_id' => 1,
            'name' => 'Domaine du Comte Sénard',
            'email' => 'office@domainesenard.com',
            'phone' => '0380264073',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 15,
            'partner_id' => 15,
            'address_id' => 1,
            'name' => 'Domaine Debray',
            'email' => '',
            'phone' => '0380226258',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 16,
            'partner_id' => 16,
            'address_id' => 1,
            'name' => 'Clos de Bourgogne',
            'email' => '',
            'phone' => '',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 17,
            'partner_id' => 17,
            'address_id' => 1,
            'name' => 'Domaine Manuel Olivier',
            'email' => 'contact@domaine-olivier.com',
            'phone' => '0380623933',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 18,
            'partner_id' => 18,
            'address_id' => 1,
            'name' => 'Domaine Trapet',
            'email' => 'message@trapet.fr',
            'phone' => '0380343040',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 19,
            'partner_id' => 19,
            'address_id' => 1,
            'name' => 'Château Bouscaut',
            'email' => 'contact@chateau-bouscaut.com',
            'phone' => '0557831220',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 20,
            'partner_id' => 20,
            'address_id' => 1,
            'name' => 'Château du Taillan',
            'email' => 'info@chateaudutaillan.com',
            'phone' => '0556574700',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 21,
            'partner_id' => 21,
            'address_id' => 1,
            'name' => 'Château Suduiraut',
            'email' => 'contact@suduiraut.com',
            'phone' => '0556636192',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 22,
            'partner_id' => 22,
            'address_id' => 1,
            'name' => 'Château Tour Saint Christophe',
            'email' => '',
            'phone' => '0557257983',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 23,
            'partner_id' => 23,
            'address_id' => 1,
            'name' => 'Château Soutard',
            'email' => 'contact@soutard.com',
            'phone' => '0557247141',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 24,
            'partner_id' => 24,
            'address_id' => 1,
            'name' => 'Château Desmirail',
            'email' => 'contact@desmirail.com',
            'phone' => '0557883433',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 25,
            'partner_id' => 25,
            'address_id' => 1,
            'name' => 'Domaine Schoenheitz',
            'email' => 'cave@vins-schoenheitz.fr',
            'phone' => '0389710396',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 26,
            'partner_id' => 26,
            'address_id' => 1,
            'name' => 'Domaine François Schwach',
            'email' => 'info@schwach.com',
            'phone' => '0389736215',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 27,
            'partner_id' => 27,
            'address_id' => 1,
            'name' => 'Domaine Bott-Geyl',
            'email' => 'info@bott-geyl.com',
            'phone' => '0389479004',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

	    DB::table('restaurant_partners')->insert([
            'id' => 28,
            'partner_id' => 28,
            'address_id' => 1,
            'name' => 'Maison Olivier Leflaive',
            'email' => 'contact@olivier-leflaive.com',
            'phone' => '0380219527',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 29,
            'partner_id' => 29,
            'address_id' => 1,
            'name' => 'Golf du Château de Chailly',
            'email' => 'reservation@chailly.com',
            'phone' => '0380903030',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 30,
            'partner_id' => 30,
            'address_id' => 1,
            'name' => 'Golf du Médoc',
            'email' => 'golf@golfdumedocresort.com',
            'phone' => '0556701190',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 31,
            'partner_id' => 31,
            'address_id' => 1,
            'name' => 'Golf d’Ammerschwihr',
            'email' => 'golf-mail@golf-ammerschwihr.com',
            'phone' => '0389471730',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 32,
            'partner_id' => 32,
            'address_id' => 1,
            'name' => 'Golf de Reims',
            'email' => 'contact@golfdereims.com',
            'phone' => '0326054610',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 33,
            'partner_id' => 33,
            'address_id' => 1,
            'name' => 'Saint-Emilionnais Golf Club',
            'email' => 'golf@segolfclub.com',
            'phone' => '0557408864',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 34,
            'partner_id' => 34,
            'address_id' => 1,
            'name' => 'Pau Golf Club 1856',
            'email' => 'contact@paugolfclub.com',
            'phone' => '0559131856',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 35,
            'partner_id' => 35,
            'address_id' => 1,
            'name' => 'restaurant prosper',
            'email' => 'pascale.rifaux@prosper-maufoux.com',
            'phone' => '0380202382',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 36,
            'partner_id' => 36,
            'address_id' => 1,
            'name' => 'Comte Sénard',
            'email' => 'office@domainesenard.com',
            'phone' => '0380264073',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 37,
            'partner_id' => 37,
            'address_id' => 1,
            'name' => 'Maison Trapet',
            'email' => 'resa@trapet.fr',
            'phone' => '0380343040',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 38,
            'partner_id' => 38,
            'address_id' => 1,
            'name' => 'Le Bistro d’Olivier',
            'email' => 'reservation@olivier-leflaive.com',
            'phone' => '0380219527',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 39,
            'partner_id' => 39,
            'address_id' => 1,
            'name' => 'Domaine Bott-Geyl',
            'email' => '',
            'phone' => '0389479004',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 40,
            'partner_id' => 40,
            'address_id' => 1,
            'name' => 'Château de Ferrand',
            'email' => '',
            'phone' => '0557744711',
            'stars' => 2,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 41,
            'partner_id' => 41,
            'address_id' => 1,
            'name' => 'Château Soutard',
            'email' => 'contact@soutard.com',
            'phone' => '0557247141',
            'stars' => 4,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 42,
            'partner_id' => 42,
            'address_id' => 1,
            'name' => 'Stentz-Buecher',
            'email' => '',
            'phone' => '0389806809',
            'stars' => 5,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 43,
            'partner_id' => 43,
            'address_id' => 1,
            'name' => 'Clos de Bourgogne',
            'email' => 'contact@clos-de-bourgogne.com',
            'phone' => '0470440300',
            'stars' => 1,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);

        DB::table('restaurant_partners')->insert([
            'id' => 44,
            'partner_id' => 44,
            'address_id' => 1,
            'name' => 'Domaine Schoenheitz',
            'email' => 'cave@vins-schoenheitz.fr',
            'phone' => '0389710396',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 45,
            'partner_id' => 45,
            'address_id' => 1,
            'name' => '?????????????',
            'email' => 'resto10@gmail.com',
            'phone' => '0222222222',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 46,
            'partner_id' => 46,
            'address_id' => 1,
            'name' => 'Château Lynch Bages',
            'email' => '',
            'phone' => '0556732400',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 47,
            'partner_id' => 47,
            'address_id' => 1,
            'name' => 'Château Gaudrelle',
            'email' => 'reservation@chateaugaudrelle.com',
            'phone' => '0247259350',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 48,
            'partner_id' => 48,
            'address_id' => 1,
            'name' => 'Champagne Le Gallais',
            'email' => 'clg@champagnelegallais.com',
            'phone' => '0625017369',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 49,
            'partner_id' => 49,
            'address_id' => 1,
            'name' => 'Château Siaurac',
            'email' => 'info@siaurac.com',
            'phone' => '0557516458',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
        DB::table('restaurant_partners')->insert([
            'id' => 50,
            'partner_id' => 50,
            'address_id' => 1,
            'name' => 'Château Soucherie',
            'email' => '',
            'phone' => '0241783118',
            'stars' => 3,
            'type_cooking' => '',
            'specialty' => 'Vin',
        ]);
    }
}
