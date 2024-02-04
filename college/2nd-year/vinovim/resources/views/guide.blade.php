@extends('layouts.main')

@section('content')
<section class="section container">
    <h1 style="text-align: center;">Guide Utilisateur</h1>
    <br/>
    <iframe id="guide" title="guide utilisateur" src="/assets/guide.pdf" frameborder="1" scrolling="auto" width="100%" height="800vh"></iframe>

    <a href="{{ url('/') }}">
        <button class="log__button">Retour a l'accueil</button>
    </a>
</section>
@stop
