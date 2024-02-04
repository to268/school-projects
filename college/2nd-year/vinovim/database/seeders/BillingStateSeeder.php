<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class BillingStateSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('billing_states')->insert([
            'state' => 'Paid',
        ]);
        // 2
        DB::table('billing_states')->insert([
            'state' => 'Unpaid',
        ]);
        // 3
        DB::table('billing_states')->insert([
            'state' => 'Refunded',
        ]);
        // 2
        DB::table('billing_states')->insert([
            'state' => 'Canceled',
        ]);
    }
}
