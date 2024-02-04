@extends('layouts.main')

@section('content')

<section class="log">
    <div class="log__container">
        <input type="checkbox" id="chk" aria-hidden="true">
        <div id="register" class="log__signup">

            <form method="POST" action="{{url('/register')}}">
                @csrf
                <label class="log__title" for="chk" aria-hidden="true">Inscription</label>

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

                <div class="success">
                    @if (session('status'))
                    <div class="success__title">
                        {{ session('status') }}
                    </div>
                    <ul class="success__list">
                        <li>
                            Votre Compte a bien été crée
                        </li>
                        <li>
                            Veuillez a présent vous connecter
                        </li>
                    </ul>
                    @endif
                </div>

                <div class="log__row">
                    <div class="log__column">
                        <select class="log__select" name="gender" required>
                            <option value="" disabled selected hidden>Titre</option>
                            @foreach($genders as $gender)
                            <option value="{{ $gender->gender }}">{{ $gender->gender }}</option>
                            @endforeach
                        </select>
                        <label>
                            <input class="log__input" type="text" name="last_name" placeholder="Nom*" required>
                        </label>
                        <label>
                            <input class="log__input" type="text" name="first_name" placeholder="Prénom*" required>
                        </label>
                        <label>
                            <input class="log__input" type="date" name="birthday" placeholder="Date de naissance*" style="padding: 0px; padding-left: 8px">
                        </label>
                    </div>
                    <div class="log__column">
                        <label>
                            <input class="log__input" type="email" name="email" placeholder="Email*" required>
                        </label>
                        <label>
                            <input class="log__input" type="number" name="postal_code" placeholder="Code postal" min="02000" max="97490">
                        </label>
                        <label>
                            <input class="log__input" type="password" name="password" placeholder="Mot de passe*" required>
                        </label>
                        <label>
                            <input class="log__input" type="password" name="password_confirmation" placeholder="Confirmation mot de passe*" required>
                        </label>
                        <label class="log__info">Élément obligatoire : *</label>
                        <button class="log__button">Inscription</button>
                    </div>
                </div>
            </form>
        </div>

        <div id="login" class="log__login">
            <form method="POST" action="{{url('/login')}}">
                @csrf
                <label class="log__title" for="chk" aria-hidden="true">Connexion</label>
                <input class="log__input" type="email" name="email" placeholder="Email" required>
                <input class="log__input" type="password" name="password" placeholder="Mot de passe" required>
                <button class="log__button">Connexion</button>
            </form>
        </div>
    </div>
</section>

@stop
