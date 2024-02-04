<?php

namespace App\Http\Controllers;

use App\Models\PromoCode;
use Illuminate\Http\Request;
use Illuminate\Support\Carbon;
use Illuminate\Support\Facades\Cookie;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;

class PaymentController extends Controller
{
    public function payment()
    {
        $items = explode(',', request()->cookie(CartController::CART_ITEMS));
        $quantities = explode(',', request()->cookie(CartController::CART_QUANTITIES));
        $amount = CartController::trips_sum($items, $quantities);

        return view('payment', ['amount' => $amount]);
    }

    public function promo_code(Request $request)
    {
        $request->validate([
            'promo_code'      => ['required', 'string', 'max:16'],
        ]);

        $status = $this->build_promo_status(
            PromoCode::where('code', '=', $request->promo_code)->first()
        );

        return redirect()->back()->with('status', $status);
    }

    public function proceed(Request $request)
    {
        $validator = Validator::make($request->all(), [
            'street_number' => ['required', 'integer', 'min:1', 'max:99999'],
            'street_name' => ['required', 'string', 'max:120'],
            'postal_code' => ['required', 'integer', 'min:02000', 'max:97490'],
            'city' => ['required', 'string', 'max:60'],

            'card_owner' => ['required', 'string', 'min:3', 'max:60'],
            'card_number' => ['required', 'integer', 'max:9999999999999999999'],
            'expiration_date' => ['required', 'string', 'min:5', 'max:5'],
            'cvc' => ['required', 'integer', 'max:9999'],

            'amount' => ['required', 'integer'],
            'gift' => ['required', 'string'],
            'promo_code' => ['nullable', 'string', 'max:16'],
        ]);

        $validator->after(function ($validator) use($request) {
            if ($this->is_expiration_date_invalid($request->expiration_date)) {
                $validator->errors()->add(
                    'expiration_date', 'La date d\'expiration est invalide'
                );
            }

            if ($this->is_fake_card($request->card_number)) {
                $validator->errors()->add(
                    'card_number', 'La carte est invalide'
                );
            }
        });

        $validator->validate();

        if ($validator->fails())
            return redirect()->back()
                ->withErrors($validator)
                ->withInput($request->input());

        $promo_code_id = null;
        $time = strval(time());

        if ($request->promo_code) {
            $promo_code = PromoCode::where('code', '=', $request->promo_code)->firstOrFail();
            $promo_code->has_been_used = true;
            $promo_code->save();

            $promo_code_id = $promo_code->id;
        }

        $billing = array(
            'billing_state_id' => 1,
            'promo_code_id' => $promo_code_id,
            'amount' => $request->amount,
            'is_a_gift' => $request->gift == 'true' ? true : false,
            'bill_url' => "/bill/$time.pdf",
        );

        DB::table('billings')->insert($billing);

        $promo_code = null;

        if ($request->gift == 'true') {
            $code = array(
                'code' => $this->generate_random_string(),
                'amount' => $request->amount,
                'validity' => Carbon::now()->addDays(90)->format('Y-m-d'),
                'has_been_used' => false,
            );

            $promo_code = $code;
            $promo_code['validity'] = $this->format_date($promo_code['validity']);
            DB::table('promo_codes')->insert($code);
        }

        Cookie::queue(Cookie::forget(CartController::CART_ITEMS));
        Cookie::queue(Cookie::forget(CartController::CART_QUANTITIES));

        return view('payment_confirmation', [
            'amount' => $request->amount,
            'promo_code' => $promo_code
        ]);
    }

    private function is_expiration_date_invalid($date_str)
    {
        $month = strtok($date_str, '/');
        $year = strtok('/');
        $full_year = '20' . $year;

        if (!is_numeric($month) || !is_numeric($full_year))
            return true;

        if (intval($month) < 1 || intval($month) > 12 || intval($year) > 99)
            return true;

        $date = date_create();
        $date->setDate($full_year, $month, 1);

        if (!$date || $date < date_create())
            return true;

        return false;
    }

    /* The card is checked if it's real by using the Luhn algorithm */
    private function is_fake_card($number)
    {
        $tbl = str_split($number);

        // Convert all digits to integers
        for ($i = 0; $i < count($tbl); $i++)
            $tbl[$i] = intval($tbl[$i]);

        for ($i = count($tbl) - 2; $i  >= 0; $i  -= 2)
            $tbl[$i] = ($tbl[$i] * 2) % 9;

        if ((array_sum($tbl) % 10) == 0)
            return false;

        return true;
    }

    private function format_date($date_str)
    {
        return Carbon::rawCreateFromFormat('Y-m-d', $date_str)->format('d/m/Y');
    }

    private function build_promo_status($code)
    {
        $validity = Carbon::createFromFormat('Y-m-d', $code->validity);

        if ($code == null || $code->has_been_used ||
            $validity->lt(Carbon::now()))
            return [
                'status' => 'Error',
                'code' => null,
                'message' => 'Le code promo est invalide',
                'discount' => null,
            ];

        return [
            'status' => 'OK',
            'code' => $code->code,
            'message' => "Réduction de $code->amount € appliquée",
            'discount' => $code->amount,
        ];
    }

    private function generate_random_string($length = 16) {
        return substr(
            str_shuffle(
                str_repeat(
                    $x = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ',
                    ceil($length / strlen($x)
                    )
                )
            ), 1, $length
        );
    }
}
