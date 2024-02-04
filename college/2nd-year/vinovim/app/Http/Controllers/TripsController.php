<?php

namespace App\Http\Controllers;

use App\Models\Trip;
use App\Models\VineyardCategory;
use App\Models\ParticipantCategory;
use App\Models\Theme;
use App\Models\Comment;
use Illuminate\Http\Request;

class TripsController extends Controller
{
    public function index()
    {
        $trips = Trip::all();
        $vineyards = VineyardCategory::all();
        $participants = ParticipantCategory::all();
        $themes = Theme::all();

        return view('index',
            ['trips'        => $trips,
            'vineyards'     => $vineyards,
            'participants'  => $participants,
            'themes'        => $themes],
        );
    }

    public function search(Request $request)
    {
        $request->validate([
            'vineyard'      => ['required', 'string'],
            'participant'   => ['required', 'string'],
            'theme'         => ['required', 'string'],
        ]);

        if ($request->vineyard != "Tous")
            $sel_vineyard = VineyardCategory::where('category', '=', $request->vineyard)->first();
        else
            $sel_vineyard = null;

        if ($request->participant != "Tous")
            $sel_participant = ParticipantCategory::where('category', '=', $request->participant)->first();
        else
            $sel_participant = null;

        if ($request->theme != "Tous")
            $sel_theme = Theme::where('name', '=', $request->theme)->first();
        else
            $sel_theme = null;

        $trips = Trip::
            when($sel_vineyard, function ($query, $vineyard) {
                if ($vineyard != null)
                    $query->where('vineyard_categories_id', '=', $vineyard->id);
            })
            ->when($sel_participant, function ($query, $participant) {
                if ($participant != null)
                    $query->where('participant_categories_id', '=', $participant->id);
            })
            ->when($sel_theme, function ($query, $theme) {
                if ($theme != null)
                    $query->where('theme_id', '=', $theme->id);
            })
            ->get();

        $vineyards = VineyardCategory::all();
        $participants = ParticipantCategory::all();
        $themes = Theme::all();

        $filtered_trips = $trips->filter(function ($trip) use ($request) {
            return str_contains(
                strtolower($trip->title),
                strtolower($request->search)
            );
        });

        return view('index',
            ['trips'            => $filtered_trips,
            'vineyards'         => $vineyards,
            'participants'      => $participants,
            'themes'            => $themes,
            'search'            => $request->search,
            'sel_vineyard'      => $request->vineyard,
            'sel_participant'   => $request->participant,
            'sel_theme'         => $request->theme],
        );
    }

    public static function get_price($trip_id, $quantity = 1)
    {
        $trip = Trip::findOrFail($trip_id);
        $duration = $trip->duration;
        $price = 0;

        foreach ($trip->stages as $stage) {
            foreach ($stage->accommodation_stages as $acmstage) {
                $price += $acmstage->price_per_person * $duration;
            }

            foreach ($stage->activity_stages as $actstage) {
                $price += $actstage->price_per_person * $duration;
            }

            foreach ($stage->meal_stages as $mstage) {
                $price += $mstage->price_per_person * $duration * 3;
            }

            foreach ($stage->tour_stages as $tstage) {
                $price += $tstage->price_per_person * $duration;
            }
        }

        return $price * $quantity;
    }

    public function read($id = null)
    {
        if (isset($id))
            $trip = Trip::findOrFail($id);
        else
            $trip = Trip::findOrFail(1);

        $comments = Comment::where('trip_id', '=', $trip->id)->paginate(10);
        $comments_count = Comment::where('trip_id', '=', $trip->id)->count();

        return view('trip', [
            'trip' => $trip,
            'comments' => $comments,
            'comments_count' => $comments_count,
        ]);
    }

    public function gift($id)
    {
        $cookie = CartController::add_to_cart($id,
            request()->cookie(CartController::CART_ITEMS),
            request()->cookie(CartController::CART_QUANTITIES)
        );
        return redirect('/cart')
            ->withCookies($cookie)
            ->with('gift', true);
    }

    public function booking($id)
    {
        $cookie = CartController::add_to_cart($id,
            request()->cookie(CartController::CART_ITEMS),
            request()->cookie(CartController::CART_QUANTITIES)
        );
        return redirect('/cart')->withCookies($cookie);
    }

    public function customize($id)
    {
        $cookie = CartController::add_to_cart($id,
            request()->cookie(CartController::CART_ITEMS),
            request()->cookie(CartController::CART_QUANTITIES)
        );
        return redirect('/cart')->withCookies($cookie);
    }

    // public function create(Request $request)
    // {
    //     $request->validate([
    //         'title' => ['required', 'string'],
    //         'description' => ['required', 'string'],
    //         'duration' => ['required', 'integer'],
    //         'participant_category' => ['required', 'exists:participant_categories,id'],
    //         'trip_category' => ['required', 'exists:trip_categories,id'],
    //         'vineyard_category' => ['required', 'exists:vineyard_categories,id'],
    //         'theme' => ['required', 'exists:themes,id'],
    //         'destination' => ['required', 'exists:destination,id'],
    //         'wine_trail' => ['required', 'exists:wine_trail,id'],
    //         'image' => ['required', 'exists:images,id'],
    //     ]);
    //
    //     $image = Image::find($request->image);
    //
    //     $image->trip()->create([
    //         'title' => $request->title,
    //         'description' => $request->descritpino,
    //         'duration' => $request->duration,
    //         'image_id' => $request->image,
    //     ]);
    //
    //     return redirect('/');
    // }

    /* This is a CRUD example */
    // public function update(Request $request, $id)
    // {
    //     $trip = Trip::findOrFail($id);
    //     $image = Trip::find($id);
    //
    //     $trip->image()->associate($image);
    //
    //     $request->validate([
    //         'title' => ['required', 'string'],
    //         'description' => ['required', 'string'],
    //         'duration' => ['required', 'integer'],
    //         'participant_category' => ['required', 'exists:participant_categories,id'],
    //         'trip_category' => ['required', 'exists:trip_categories,id'],
    //         'vineyard_category' => ['required', 'exists:vineyard_categories,id'],
    //         'theme' => ['required', 'exists:themes,id'],
    //         'destination' => ['required', 'exists:destination,id'],
    //         'wine_trail' => ['required', 'exists:wine_trail,id'],
    //         'image' => ['required', 'exists:images,id'],
    //     ]);
    //
    //     return redirect('/trip/{$id}');
    // }

    // public function delete($id) {
    //     $trip = Trip::findOrFail($id);
    //     $trip->image()->detach();
    //     $trip->delete();
    //
    //     return redirect()->back();
    // }
}
