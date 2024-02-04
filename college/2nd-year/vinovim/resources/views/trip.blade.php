@extends('layouts.main')

@section('content')

<section class="trip section container">
    <div class="trip__primary">
        <img class="trip__primary-img" src="/{{ $trip->image->path }}">
        <div class="trip__primary-info">
            <h2>{{ $trip->title }}</h2>
            <div>
                <span>
                    {{ $trip->duration . " jours" }}
                    <span>|</span>
                    {{ ($trip->duration - 1) . " nuit(s)" }}
                </span>
                <span>{{ app\Http\Controllers\TripsController::get_price($trip->id); }} €</span>
                "/"
                <span>pers.</span>
            </div>
            <div>
                <ul>
                    @foreach($trip->stages as $stage)
                        <li>{{ substr($stage->description, 0, 42) }}</li>
                    @endforeach
                </ul>
            </div>
            <div>
                <ul>
                    <li>{{ $trip->participant->category }}</li>
                </ul>
            </div>
        </div>
    </div>

    <div>
        <span>Vos choix:</span>
        <br/>
        <a href="{{ URL::current() . '/gift' }}"><span>Je fais plaisir </span>OFFRIR</a>
        <br/>
        <a href="{{ URL::current() . '/booking' }}"><span>J'ai une date de séjour </span>RÉSERVER</a>
        <br/>
        <a href="{{ URL::current() . '/customize' }}"><span>Je veux du sur-mesure </span>PERSONNALISER</a>
    </div>

    <div>
        <ul>
            <br/>
            <li><a href="">PROGRAMME DU SÉJOUR</a></li>
            <li><a href="">OPTIONS</a></li>
            <li><a href="">HÉBERGEMENTS</a></li>
            <li><a href="">CHÂTEAUX / DOMAINES</a></li>
            <li><a href="">AVIS ({{ $comments_count }})</a></li>
            <br/>
        </ul>
        <div class="trip__detail">
            <h3>LE PROGRAMME DÉTAILLÉ DE VOTRE SÉJOUR</h3>
            <ul>
                @foreach($trip->stages as $stage)
                    <li class="trip__stage">
                        <div class="trip__stage-img">
                            <img src="/{{ $stage->image->path }}" alt="">
                        </div>
                        <div class="trip__stage-desc">
                            <h4 class="trip__stage-title">Jour {{ $loop->index + 1 }}: {{ substr($stage->description, 0, 42) }}</h4>
                            <ul>
                                <li class="trip__stage-desc">{{ $stage->description }}</li>
                            </ul>
                        </div>
                    </li>
                @endforeach
            </ul>
        </div>

        <div class="trip__options">
            <h3>LES OPTIONS QUE VOUS POUVEZ AJOUTER À VOTRE OFFRE</h3>
            <p>Ajoutez des activités à votre séjour sur la route des Grands Crus de Bourgogne (vous pourrez activer ces options sur le formulaire de réservation) :</p>
            <p>Diner au restaurant Le Prosper au pied de votre hébergement.</p>
            <p>Atelier tonnellerie à Meursault, pour découvrir de manière vivante et conviviale tous les secrets de fabrication.</p>
        </div>

        <div>
            <h3>Les hébergements proposés</h3>
            <div>
                <ul>
                    <li class="trip__options-choice">
                        <div>
                            <div>
                                <img src="https://medias1.vinotrip.com/img/co/31.jpg">
                                <br>
                            </div>
                        </div>
                        <div>
                            <span>Maison Prosper Maufoux</span><br><br>
                            <p>La Maison Prosper Maufoux vous reçoit au Château de Saint-Aubin, sur la route des Grands Crus de Bourgogne dans le village du même nom, dans leur maison d’hôtes de charme entre élégance contemporaine et authenticité, décoration design et mobilier d’époque.</p>
                            En savoir plus sur notre partenaire
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <div>
            <h3>Les châteaux et les domaines...</h3>
            <div>
                <ul>
                    <li class="trip__options-choice">
                        <div><img src="https://www.vinotrip.com/img/cms/Maison-Olivier-Leflaive-bourgogne.jpg"></div>
                        <div>
                            <h4>Maison Olivier Leflaive - Visite et dégustation de vin au domaine</h4>
                            <p>Chez les Leflaive, le vin coule dans les veines, un héritage transmis comme un trésor de générations en générations : Olivier en est la 18ème ! En 1984 il crée sa propre maison d’élevage et de vinification, domaine et négoce haute couture au cœur de la Bourgogne viticole...</p>
                        </div>
                        <span>En savoir plus sur notre partenaire <a href="https://www.vinotrip.com/fr/partenaires/8-maison-olivier-leflaive" rel="nofollow">Maison Olivier Leflaive</a></span>
                    </li>
                    <li class="trip__options-choice">

                        <div><img src="https://www.vinotrip.com/img/cms/domaine-debray-vin.jpg"></div>
                        <div>
                            <h4>Domaine Debray - Beaune</h4>
                            <p>Le domaine Debray est une propriété viticole familiale au cœur de Beaune, désormais tenue par Yvonnick Debray qui a travaillé de nombreuses années dans la vente de vins de Bourgogne. Le domaine Debray produit diverses appellations à la fois en Côte de Beaune et en Côte de Nuits...</p>
                        </div>
                        <span>En savoir plus sur notre partenaire <a href="https://www.vinotrip.com/fr/partenaires/156-domaine-debray" rel="nofollow">Domaine Debray</a></span>
                    </li>
                </ul>
            </div>
        </div>
    </div>

        <br/>
        <br/>

        <div class="trip__comments">
            @foreach($comments as $comment)
                <h4>{{ $comment->title }} {{ $comment->stars }} étoile(s)</h4>
                <br/>

                <span>Le {{ date_create($comment->date)->format('d/m/Y à H:i:s') }}</span>
                <br/>
                <br/>

                <span>{{ $comment->content }}</span>
                <br/>
                <br/>
                <br/>
            @endforeach
        </div>
</section>

@stop
