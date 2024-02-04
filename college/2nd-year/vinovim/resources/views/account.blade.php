@extends('layouts.main')

@section('content')
<section class="section container">
    <h1>Votre Compte</h1>
    <h3>{{ $gender->gender }} {{ $user->last_name }} {{ $user->first_name }}</h3>
    <h3>Mail: {{ $user->email }}</h3>
    <h3>Date de naissance: {{ date_create($user->birthday)->format('d/m/Y') }}</h3>

    @if(isset($user->postal_code))
        <h3>Code postal: {{ $user->postal_code }}</h3>
    @else
        <h3>Code postal non reseigné</h3>
    @endif

    @if($user->is_admin)
        <form method="POST" action="{{url('/anonymize')}}">
            @csrf
            <button class="log__button">Anonymiser</button>
        </form>
    @endif

    <form method="POST" action="{{url('/logout')}}">
        @csrf
        <button class="log__button">Déconnexion</button>
    </form>

</section>
@stop
