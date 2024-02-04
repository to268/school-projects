<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class AddressSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('addresses')->insert([
            'number' => '10',
            'street' => 'Place du Monument',
            'postal_code' => '21190',
            'city' => 'Puligny-Montrachet',
        ]);
    }
}
