@extends('layouts.main')

@section('content')

<section class="trips section container" id="trips">
    <h2 class="section__title">Route des vins</h2>

    <div class="trips__container grid">
        @foreach($trips as $trip)
        <article class="trips__card">
            <img src="{{ $trip->image->path }}" alt="" class="trips__img">
            <h3 class="trips__title">{{ $trip->title }}</h3>
            <p class="">{{ $trip->title }}</p>
            <span class="trips__category">
                {{ $trip->vineyard->category }}<br>
                {{ $trip->participant->category }}<br>
                {{ $trip->theme->name }}<br>
            </span>
            <br>
            <span class="trips__price">{{ app\Http\Controllers\TripsController::get_price($trip->id); }} €</span>
            <button class="trips__button">
                <i class='bx bx-shopping-bag'></i>
            </button>
        </article>
        @endforeach
    </div>

</section>

@stop