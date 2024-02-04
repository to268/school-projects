<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Carbon;
use Illuminate\Support\Facades\Validator;
use Illuminate\Http\Request;
use App\Models\Gender;
use App\Models\Client;

class AccountsController extends Controller
{
    public function login()
    {
        if (Auth::check())
            return redirect('/account');

        $genders = Gender::all();
        return view('login', ['genders' => $genders]);
    }

    public function register()
    {
        if (Auth::check())
            return redirect('/account');

        $genders = Gender::all();
        return view('login', ['genders' => $genders]);
    }

    public function handle_login(Request $request)
    {
        if (Auth::check())
            return redirect('/account');

        $validator = Validator::make(request()->all(), [
            'email' => ['required', 'string', 'max:128'],
            'password' => ['required', 'string', 'min:12', 'max:64'],
        ]);

        $validator->after(function ($validator) use($request) {
            $client = Client::where('email', '=', $request->email)->first();

            if (is_null($client)) {
                $validator->errors()->add(
                    'password', 'Le mail et/ou le mot de passe est invalide'
                );
                return;
            }

            if ($this->hash($request->email, $request->password) != $client->password) {
                $validator->errors()->add(
                    'password', 'Le mail et/ou le mot de passe est invalide'
                );
            }
        });

        $validator->validate();

        if ($validator->fails())
            return redirect('/login')
                ->withErrors($validator)
                ->withInput($request->input());

        $client_id = Client::where('email', '=', $request->email)->first()->id;
        Auth::guard('web')->loginUsingId($client_id);
        $request->session()->regenerate();
        return redirect()->intended('/');
    }

    public function handle_register(Request $request)
    {
        if (Auth::check())
            return redirect('/account');

        $validator = Validator::make($request->all(), [
            'last_name' => ['required', 'string', 'max:50'],
            'first_name' => ['required', 'string', 'max:50'],
            'birthday' => ['required', 'date'],
            'email' => ['required', 'string', 'max:128'],
            'postal_code' => ['nullable', 'integer', 'min:02000', 'max:97490'],
            'password' => ['required', 'string', 'min:12', 'max:64'],
            'password_confirmation' => ['required', 'string', 'min:12', 'max:64'],
        ]);

        $validator->after(function ($validator) use($request) {
            if ($this->is_birthday_invalid($request->birthday)) {
                $validator->errors()->add(
                    'date', 'Vous devez avoir au moins 18 ans'
                );
            }

            if ($this->is_email_already_exists($request->email)) {
                $validator->errors()->add(
                    'email', 'Le mail entré est déjà associé a un compte'
                );
            }

            if ($request->password != $request->password_confirmation) {
                $validator->errors()->add(
                    'password_confirmation', 'La confirmation du mot de passe est incorrect'
                );
            }
        });

        $validator->validate();

        if ($validator->fails())
            return redirect('/register')
                ->withErrors($validator)
                ->withInput($request->input());

        // We don't expect an unknown gender but just in case
        $gender = Gender::where('gender', '=', $request->gender)->firstOrFail();

        $data = array(
            'gender_id' => $gender->id,
            'last_name' => $request->last_name,
            'first_name' => $request->first_name,
            'birthday' => $request->birthday,
            'email' => $request->email,
            'postal_code' => $request->postal_code,
            'is_admin' => false,
            'password' => $this->hash($request->email, $request->password),
        );

        DB::table('clients')->insert($data);
        return redirect('/login')->with('status', 'Inscription réussie');
    }

    public function logout(Request $request)
    {
        Auth::logout();
        $request->session()->invalidate();
        $request->session()->regenerateToken();
        return redirect('/');
    }

    public function account()
    {
        if (!Auth::check())
            return redirect()->back();

        $user = Auth::user();
        $gender = Gender::where('id', '=', $user->gender_id)->first();

        return view('account', [
            'user' => $user,
            'gender' => $gender
        ]);
    }

    public function anonymize(Request $request)
    {
        $user = Auth::user();

        if (!$user->is_admin)
            return redirect()->back();

        DB::raw('CALL ps_rgpd_anonymization()');
        return redirect()->back();
    }

    public static function get_current_username()
    {
        if (!Auth::check())
            return 'Connexion';

        $user = Auth::user();
        return $user->first_name;
    }

    private function is_birthday_invalid(String $birthday)
    {
        $current_date = Carbon::now();
        $birthday = new Carbon($birthday);
        $years = $birthday->diffInYears($current_date);

        if ($birthday > $current_date || $years < 18 || $years > 130)
            return true;

        return false;
    }

    private function is_email_already_exists(String $email)
    {
        $nb_same_clients = Client::where('email', '=', $email)->count();

        if ($nb_same_clients > 0)
            return true;

        return false;
    }

    private function hash(String $email, String $password)
    {
        $hash = hash('sha256', $password);
        return hash('sha256', "$email:$hash");
    }
}
