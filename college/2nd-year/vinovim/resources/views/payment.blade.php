@extends('layouts.main')

@section('content')
<section class="section container">
    @if (session('status'))
        @if (session('status')['status'] == 'OK')
        <div class="success__title">
        @else
        <div class="error__title">
        @endif
            <h3>{{ session('status')['message'] }}</h3>
        </div>
    @endif

    <div class="error">
        @if($errors->any())
        <div class="error__title">
            Erreur
        </div>
        <ul class="error__list">
            @foreach($errors->all() as $error)
            <li>
                {{$error}}
            </li>
            @endforeach
        </ul>
        @endif
    </div>

    <h1>Code promo</h1>
    <form method="POST" action="{{url('/payment/promo_code')}}">
        @csrf
        <label>
            @if (isset(session('status')['code']))
                <input class="log__input" type="text" name="promo_code" placeholder="Code promo" max="16" value="{{ session('status')['code'] }}">
            @else
                <input class="log__input" type="text" name="promo_code" placeholder="Code promo" max="16">
            @endif
        </label>
            <button type="submit" class="log__button">Appliquer</button>
        @if(isset($promo_status))
            <h3>{{ $promo_status }}</h3>
        @endif
    </form>
    <br/>

    <form method="POST" action="{{url('/payment/proceed')}}">
        @csrf
        <h1>Règlement du paiement</h1>
        <br/>

        <h3>Adresse de facturation</h3>
        <label>
            <input class="log__input" type="number" name="street_number" placeholder="Numéro de rue*" min="1" max="99999" required>
        </label>
        <label>
            <input class="log__input" type="text" name="street_name" placeholder="Nom de la rue*" maxlength="120" required>
        </label>
        <label>
            <input class="log__input" type="number" name="postal_code" placeholder="Code postal*" min="02000" max="97490" required>
        </label>
        <label>
            <input class="log__input" type="text" name="city" placeholder="Nom de la ville*" maxlength="60" required>
        </label>

        <h3>Coordonnées bancaires</h3>
        <label>
            <input class="log__input" type="text" name="card_owner" placeholder="Titulaire de la carte*" minlength="3" maxlength="60" required>
        </label>
        <label>
            <input class="log__input" type="number" name="card_number" placeholder="Numéro de la carte*" max="9999999999999999999" required>
        </label>
        <label>
            <input class="log__input" type="text" name="expiration_date" placeholder="Date d'expiration (mm/aa)*" minlength="5" maxlength="5" required>
        </label>
        <label>
            <input class="log__input" type="number" name="cvc" placeholder="Cryptogramme visuel*" max="9999" required >
        </label>
        <label class="log__info">Élément obligatoire : *</label>

        @if (isset(session('status')['code']))
            <input type="hidden" name="promo_code" value="{{ session('status')['code'] }}">
        @endif

        @if (isset(session('status')['discount']))
            <input type="hidden" name="amount" value="{{ $amount - session('status')['discount'] }}">
        @else
            <input type="hidden" name="amount" value="{{ $amount }}">
        @endif

        <br/>

        @if (isset(session('status')['discount']))
            <h3>Sous-Total: {{ $amount }} €</h3>
            <h3>Réduction: -{{ session('status')['discount'] }} € (code: {{ session('status')['code'] }})</h3>
            <h1>Total: {{ $amount - session('status')['discount'] }} €</h1>
        @else
            <h1>Total: {{ $amount }} €</h1>
        @endif
        @if (Request::input('gift'))
            <input type="hidden" name="gift" value="true">
            <h1>🎁 Vous offrez un cadeau 🎁</h1>
            <br/>
        @else
            <input type="hidden" name="gift" value="false">
        @endif

        @if (isset(session('status')['discount']))
            <button type="submit" class="log__button">Payer {{ $amount - session('status')['discount'] }} €</button>
        @else
            <button type="submit" class="log__button">Payer {{ $amount }} €</button>
        @endif
    </form>
</section>
@stop
