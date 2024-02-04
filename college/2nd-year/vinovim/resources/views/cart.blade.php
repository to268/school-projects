@extends('layouts.main')

@section('content')
<section class="section container">
    <h1>Votre Panier</h1>
    <br/>
    @if(isset($cart))
        @foreach($cart as $item)
            <a href="{{ url('/trip/'.$item['trip']->id) }}">
                <h3>{{ $item['trip']->title }}</h3>
            </a>

            <br/>
            <h4>{{ $item['trip']->description }}</h4>
            <div>
                <span>
                    {{ $item['trip']->duration . " jours" }}
                    <span>|</span>
                    {{ ($item['trip']->duration - 1) . " nuit(s)" }}
                </span>
            </div>
            <h3>{{ App\Http\Controllers\TripsController::get_price($item['trip']->id, $item['quantity']); }} €</h3>

            <br/>
            <img src="{{ $item['trip']->image->path }}" width=128/>

            <br/>
            <br/>
            <form method="POST" action="{{url('/cart')}}">
                @csrf
                <input type="hidden" name="id" value="{{ $item['trip']->id }}">

                <label>
                    <span>Nombre de personnes: </span>
                    <input type="number" name="quantity" placeholder="Nombre de personnes*" value="{{ $item['quantity'] }}" min="1" max="99">
                </label>
                <br/>
                <button type="submit">
                    <i class='bx bx-edit-alt'></i>
                    <span>Modifier</span>
                </button>
            </form>
            <form method="POST" action="{{url('/cart/' . $item['trip']->id)}}">
                @csrf
                <input type="hidden" name="_method" value="DELETE">
                <input type="hidden" name="id" value="{{ $item['trip']->id }}">

                <button type="submit">
                    <i class='bx bx-trash'></i>
                    <span>Supprimer</span>
                </button>
            </form>

            <br/>
            <br/>
            <br/>
            <br/>
        @endforeach
        <h3>Total: {{ $price }} €</h3>
        <br/>
        @if (session('gift'))
        <a href="{{ url('/payment?gift=true') }}">
        @else
        <a href="{{ url('/payment') }}">
        @endif
            <button class="log__button">Poursuive l'achat</button>
        </a>
    @else
        <h3>Votre panier est vide</h3>
    @endif
</section>
@stop
