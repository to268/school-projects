<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class BillingSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // 1
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '700',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 2
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '500',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 3
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '300',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 4
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => 27,
            'amount' => '200',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 5
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '100',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 6
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => 9,
            'amount' => '600',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 7
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '250',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 8
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => 10,
            'amount' => '375',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 9
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '666',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 10
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => 36,
            'amount' => '486',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 11
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '129',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 12
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '374',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 13
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => 40,
            'amount' => '711',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 14
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '696',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 15
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '191',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 16
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '234',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 17
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => 46,
            'amount' => '567',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 18
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '321',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 19
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '654',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 20
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '200',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 21
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => 50,
            'amount' => '543',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 22
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '345',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 23
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '231',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 24
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '132',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 25
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '465',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 26
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '564',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 27
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '645',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 28
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '756',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 29
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '576',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 30
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '135',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 31
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '531',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 32
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '246',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 33
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '642',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 34
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '357',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 35
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '753',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 36
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '300',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 37
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '120',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 38
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '230',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 39
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '340',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 40
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '450',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 41
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '320',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 42
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '210',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 43
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '430',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 44
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '540',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 45
        DB::table('billings')->insert([
            'billing_state_id' => 1,
            'promo_code_id' => null,
            'amount' => '650',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 46
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => null,
            'amount' => '102',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 47
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => null,
            'amount' => '203',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 48
        DB::table('billings')->insert([
            'billing_state_id' => 4,
            'promo_code_id' => null,
            'amount' => '304',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
        // 49
        DB::table('billings')->insert([
            'billing_state_id' => 2,
            'promo_code_id' => 6,
            'amount' => '405',
            'is_a_gift' => 'true',
            'bill_url' => '',
        ]);
        // 50
        DB::table('billings')->insert([
            'billing_state_id' => 3,
            'promo_code_id' => 50,
            'amount' => '506',
            'is_a_gift' => 'false',
            'bill_url' => '',
        ]);
    }
}
