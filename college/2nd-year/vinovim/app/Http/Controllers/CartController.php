<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Trip;
use Illuminate\Support\Facades\Cookie;

class CartController extends Controller
{
    const CART_ITEMS = 'cart_items';
    const CART_QUANTITIES = 'cart_quantities';
    const COOKIE_TIME = 4320;

    public function cart(Request $request)
    {
        $cart = null;
        $price = 0;

        if ($request->cookie(self::CART_ITEMS) !== null) {
            $cart = array();
            [$items, $quantities] = $this->get_cart();

            for ($i=0; $i < count($items); $i++) {
                $trip = Trip::where('id', '=', $items[$i])->first();
                array_push($cart, $trip);
                $price += TripsController::get_price($trip->id, $quantities[$i]);
            }

            $cart = $this->unwind_cart($cart);
        }

        return view('cart', [
            'cart' => $cart,
            'price' => $price,
        ]);
    }

    public function modify_cart(Request $request)
    {
        [$items, $quantities] = $this->get_cart();

        if (in_array($request->id, $items)) {
            $index = array_search($request->id, $items);
            $quantities[$index] = $request->quantity;
        }

        return redirect('/cart')->withCookies(self::set_cart($items, $quantities));
    }

    public function delete_cart($id)
    {
        [$items, $quantities] = $this->get_cart();

        if (in_array($id, $items)) {
            if (count($items) == 1) {
                Cookie::queue(Cookie::forget(self::CART_ITEMS));
                Cookie::queue(Cookie::forget(self::CART_QUANTITIES));
                return redirect('/cart');
            }

            $index = array_search($id, $items);
            unset($items[$index]);
            unset($quantities[$index]);
        }

        return redirect('/cart')->withCookies(self::set_cart($items, $quantities));
    }

    public static function trips_sum($items, $quantities)
    {
        $price = 0;

        for ($i=0; $i < count($items); $i++) {
            $price += TripsController::get_price($items[$i], $quantities[$i]);
        }

        return $price;
    }

    public static function add_to_cart($id, $items, $quantities)
    {
        Trip::findOrFail($id);

        if ($items !== null) {
            $items = explode(',', $items);
            $quantities = explode(',', $quantities);
        } else {
            $items = array();
            $quantities = array();
        }

        if (!in_array($id, $items)) {
            array_push($items, $id);
            array_push($quantities, 1);
        }

        return self::set_cart($items, $quantities);
    }

    public static function get_count()
    {
        if (Cookie::get(self::CART_ITEMS) == "")
            return 0;

        $items = explode(',', Cookie::get(self::CART_ITEMS));
        return count($items);
    }

    private function get_cart()
    {
        return [
            explode(',', request()->cookie(self::CART_ITEMS)),
            explode(',', request()->cookie(self::CART_QUANTITIES)),
        ];
    }

    /*
     * We are unable to set the values directy
     * because we are is a static context
     */
    private static function set_cart($items, $quantities)
    {

        return [
            cookie(self::CART_ITEMS, implode(',', $items), self::COOKIE_TIME),
            cookie(self::CART_QUANTITIES, implode(',', $quantities), self::COOKIE_TIME),
        ];
    }

    private function unwind_cart($trips)
    {
        $data = [];
        $quantities = $this->get_cart()[1];

        for ($i=0; $i < count($trips); $i++) {
            array_push($data, [
                'trip' => $trips[$i],
                'quantity' => $quantities[$i],
            ]);
        }

        return $data;
    }
}
