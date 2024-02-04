@extends('layouts.main')

@section('content')
<section class="trips section container" id="trips">
    <h2 class="section__title">Sejour</h2>

    <div class="search__form">
        <form method="POST" class="search__form" action="{{url('/')}}">
            @csrf
            <input class="search__input" type="" name="search" @isset($search) value="{{ $search }}" @endisset>
            <select class="search__select" name="vineyard" id="pet-select" required>
                @isset($sel_vineyard)
                    <option value="Tous">Tous</option>
                @else
                    <option value="Tous" selected="selected">Tous</option>
                @endisset

                @foreach($vineyards as $vineyard)
                    @if(isset($sel_vineyard) && $sel_vineyard == $vineyard->category)
                        <option value="{{ $vineyard->category }}" selected="selected">{{ $vineyard->category }}</option>
                    @else
                        <option value="{{ $vineyard->category }}">{{ $vineyard->category }}</option>
                    @endif
                @endforeach
            </select>
            <select class="search__select" name="participant" id="pet-select">
                @isset($sel_participant)
                    <option value="Tous">Tous</option>
                @else
                    <option value="Tous" selected="selected">Tous</option>
                @endisset

                @foreach($participants as $participant)
                    @if(isset($sel_participant) && $sel_participant == $participant->category)
                        <option value="{{ $participant->category }}" selected="selected">{{ $participant->category }}</option>
                    @else
                        <option value="{{ $participant->category }}">{{ $participant->category }}</option>
                    @endif
                @endforeach
            </select>
            <select class="search__select" name="theme" id="pet-select">
                @isset($sel_theme)
                    <option value="Tous">Tous</option>
                @else
                    <option value="Tous" selected="selected">Tous</option>
                @endisset

                @foreach($themes as $theme)
                    @if(isset($sel_theme) && $sel_theme == $theme->name)
                        <option value="{{ $theme->name }}" selected="selected">{{ $theme->name }}</option>
                    @else
                        <option value="{{ $theme->name }}">{{ $theme->name }}</option>
                    @endif
                @endforeach
            </select>
            <button class="search__button" type="submit">
                <i class='bx bx-search'></i>
            </button>
        </form>
    </div>

    <div class="trips__container grid">
        @foreach($trips as $trip)
        <a href="{{url('/trip/' . $trip->id)}}">
            <article  class="trips__card">
                <img src="/{{ $trip->image->path }}" alt="" class="trips__img">
                <h3 class="trips__title">{{ $trip->title }}</h3>
                <p class="">{{ $trip->title }}</p>
                <span class="trips__category">
                    {{ $trip->vineyard->category }}<br>
                    {{ $trip->theme->name }}<br>
                    <abbr title="{{ $trip->participant->category }}">{{ $trip->participant->emoji }}</abbr><br>
                </span>
                <br>
                <span class="trips__price">{{ app\Http\Controllers\TripsController::get_price($trip->id); }} €</span>
                <button class="trips__button">
                    <i class='bx bx-shopping-bag'></i>
                </button>
            </article>
        </a>
        @endforeach
    </div>
</section>
@stop
