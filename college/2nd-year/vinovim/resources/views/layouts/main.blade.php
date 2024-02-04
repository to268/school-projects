<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="{!! asset('css/style.css') !!}" rel="stylesheet">
    <!--=============== FAVICON ===============-->
    <link rel="shortcut icon" href="{!! asset('img/favicon.png') !!}" type="image/x-icon">

    <!--=============== BOXICONS ===============-->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css">

    <!--=============== SWIPER CSS ===============-->
    <link href="{!! asset('css/swiper-bundle.min.css') !!}" rel="stylesheet">

    <!--=============== CSS ===============-->
    <link href="{!! asset('css/styles.css') !!}" rel="stylesheet">

    <script type="text/javascript">
        var ROOT_URL = {!! json_encode(url('/')) !!}
    </script>

    <title>VinoTrip</title>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/cookieconsent@3/build/cookieconsent.min.css" />
</head>

<body>

    <!--==================== HEADER ====================-->
    <header class="header" id="header">
        <nav class="nav container">
            <a href="{{url('/')}}" class="nav__logo">
                <i class='bx bxs-wine nav__logo-icon'></i> Vinotrip
            </a>

            <div class="nav__menu" id="nav-menu">
                <ul class="nav__list">
                    <li class="nav__item">
                        <a href="" class="nav__link active-link">Séjour</a>
                    </li>
                    <li class="nav__item">
                        <a href="" class="nav__link">Vignobles</a>
                    </li>
                    <li class="nav__item"><a href="#">Route des vins</a>
                        <ul class="nav__dropdown">
                            <li class="nav__dropdown-item"><a href="#">Bordeaux</a></li>
                            <li class="nav__dropdown-item"><a href="#">Champagne</a></li>
                            <li class="nav__dropdown-item"><a href="#">Bourgogne</a></li>
                        </ul>
                    <li class="nav__item">
                        <a href="" class="nav__link">Coffret cadeau</a>
                    </li>
                    <li class="nav__item">
                        <a href="" class="nav__link">Offre entreprise</a>
                    </li>
                    <li class="nav__item">
                        <form>
                            <input class="nav__search" type="search" name="nav__search" value="" class="nav__search">
                        </form>

                    </li>
                </ul>

                <div class="nav__close" id="nav-close">
                    <i class='bx bx-x'></i>
                </div>
            </div>

            <div class="nav__btns">


                <!-- Theme change button -->
                <i class='bx bx-moon change-theme' id="theme-button"></i>

                <div class="nav__account" id="account-button">
                    <a href="{{url('/account')}}">
                        <i class='bx bx-user'></i>
                        {{ App\Http\Controllers\AccountsController::get_current_username() }}
                    </a>
                </div>

                <div class="nav__account" id="nav-cart">
                    <a href="{{url('/cart')}}">
                        <i class='bx bx-cart'></i>
                        {{ App\Http\Controllers\CartController::get_count() }}
                    </a>
                </div>

                <div class="nav__toggle" id="nav-toggle">
                    <i class='bx bx-grid-alt'></i>
                </div>
            </div>
        </nav>
    </header>


    <!--Page Content-->

    <main class="main">
        <div class="container">

            @yield('content')

        </div>
    </main>

    <!--Footer-->
    <footer class="footer section">
        <div class="footer__container container grid">
            <div class="footer__content">
                <h3 class="footer__title">Nos informations</h3>

                <ul class="footer__list">
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
            <div class="footer__content">
                <h3 class="footer__title">À propos</h3>

                <ul class="footer__links">
                    <li>
                        <a href="{{ url('/guide') }}" class="footer__link">Guide Utilisateur</a>
                    </li>
                    <li>
                        <a href="#" class="footer__link">À propos</a>
                    </li>
                </ul>
            </div>

            <div class="footer__content">
                <h3 class="footer__title">Sejours</h3>

                <ul class="footer__links">
                    <li>
                        <a href="#" class="footer__link">Séjour</a>
                    </li>
                    <li>
                        <a href="#" class="footer__link">Vignobles</a>
                    </li>
                    <li>
                        <a href="#" class="footer__link">Routes des vins</a>
                    </li>
                    <li>
                        <a href="#" class="footer__link">Coffret cadeau</a>
                    </li>
                    <li>
                        <a href="#" class="footer__link">Offre entreprise</a>
                    </li>

                </ul>
            </div>

            <!-- <div class="footer__content">
                <h3 class="footer__title">Social</h3>

                <ul class="footer__social">
                    <a href="https://www.facebook.com/" target="_blank" class="footer__social-link">
                        <i class='bx bxl-facebook'></i>
                    </a>

                    <a href="https://twitter.com/" target="_blank" class="footer__social-link">
                        <i class='bx bxl-twitter'></i>
                    </a>

                    <a href="https://www.instagram.com/" target="_blank" class="footer__social-link">
                        <i class='bx bxl-instagram'></i>
                    </a>
                </ul>
            </div>
        </div> -->

            <!-- <span class="footer__copy">&#169; Bedimcode. All rigths reserved</span> -->
    </footer>

    <!--=============== SCROLL UP ===============-->
    <a href="#" class="scrollup" id="scroll-up">
        <i class='bx bx-up-arrow-alt scrollup__icon'></i>
    </a>

    <!--=============== SWIPER JS ===============-->
    <script src="{{ asset('js/swiper-bundle.min.js')}}"></script>

    <!--=============== CHATBOT JS ===============-->
    <script src="{{ asset('js/chatbot.js')}}" defer></script>

    <!--=============== MAIN JS ===============-->
    <script src="{{ asset('js/main.js')}}"></script>
    <script src="https://cdn.jsdelivr.net/npm/cookieconsent@3/build/cookieconsent.min.js" data-cfasync="false"></script>
    <script>
    window.cookieconsent.initialise({
      "palette": {
        "popup": {
          "background": "#fedafd"
        },
        "button": {
          "background": "#fedafd"
        }
      },
      "theme": "edgeless",
      "type": "opt-in",
      "content": {
        "message": "Ce site utilise des cookies pour améliorer votre navigation. Vous pouvez en apprendre plus sur",
        "allow": "Accepte",
        "link": "l'utilisation des cookies et la manière de modifier vos paramètres."
      }
    });
    </script>


    <!--=============== CHATBOT ===============-->
    <button class="scrollup open-chatbot" id="open-chatbot">
        <i class='bx bx-help-circle'></i>
    </button>

    <div class="chatbot hidden-chatbot scrollup" id="chatbot-div">
        <div class="chatbot-dialog"></div>
    </div>
</body>

</html>
