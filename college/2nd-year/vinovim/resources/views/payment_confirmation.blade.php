@extends('layouts.main')

@section('content')
<section class="section container">
    <div class="success__title">
        <h1>Le Paiement de {{ $amount }} € a bien été effectué</h1>
        @if (isset($promo_code))
            <h1>Le code promo est {{ $promo_code['code'] }}</h1>
            <h1>Valide jusqu'au {{ $promo_code['validity'] }}</h1>
        @endif
    </div>

    <a href="{{ url('/') }}">
        <button class="log__button">Retour a l'accueil</button>
    </a>
</section>
@stop
