<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ChatBotQuestionSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('chat_bot_questions')->insert([
            'question' => 'Que signifie les emojis ?',
            'answer' => '💑: signifie que le séjour est fait pour être en couple<br/>👪: signifie que le séjour est fait pour être en famille<br/>👦👧: signifie que le séjour est fait pour être entre amis',
            'topic' => 'home',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment rechercher les séjours ?',
            'answer' => 'Vous pouvez écrire les mots clés dans la barre de recherche.<br/>Des filtres sont disponible pour filtrer par vignoble, par type de séjour et par thème.',
            'topic' => 'home',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment se connecter ou créer un compte ?',
            'answer' => 'Il suffit de cliquer sur le bouton connexion et entrez les informations nécessaires.',
            'topic' => 'home',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment se déconnecter ?',
            'answer' => 'Il suffit de cliquer sur son nom et cliquer sur le bouton déconnexion.',
            'topic' => 'home',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment naviguer sur le site ?',
            'answer' => 'Vous pouvez naviguer sur le site grâce aux options dans la bannière du site.',
            'topic' => 'home',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment offrir un séjour ?',
            'answer' => 'Cliquer sur offrir, ensuite payer le séjour, un code promo sera généré.<br/>Il suffit de partager le code promo et le séjour en question avec le bon nombre de personnes, puis appliquer le code promo lors du paiement.<br/>A noter que les codes promos sont valide 90 jours depuis la date de création du séjour.',
            'topic' => 'trip',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment réserver un séjour ?',
            'answer' => 'Cliquer sur réserver, ou personnaliser, pour modifier votre séjour comme vous le souhaiter',
            'topic' => 'trip',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment personnaliser un séjour ?',
            'answer' => 'Cliquer sur personnaliser, ensuite payer le séjour, un code promo sera généré.<br/>Il suffit de partager le code promo et le séjour en question avec le bon nombre de personnes, puis appliquer le code promo lors du paiement.<br/>A noter que les codes promos sont valide 90 jours depuis la date de création du séjour.',
            'topic' => 'trip',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment voir les avis d\'un séjour ?',
            'answer' => 'Cliquer sur avis, les avis sont situés en bas de la page.',
            'topic' => 'trip',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment créer un compte ?',
            'answer' => 'Si vous ne voyez pas la page d\'inscription, il faut cliquer sur le titre connexion.<br/>Vous devez ensuite entrez les informations nécessaire, vous devez avoir 18 ans ou plus et entrez un mot de passe de 12 caractères.',
            'topic' => 'login',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment se connecter ?',
            'answer' => 'Si vous ne voyez pas la page de connexion, il faut cliquer sur le texte connexion en bas.<br/>Vous devez ensuite entrez les informations pour vous identifier.',
            'topic' => 'login',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment créer un compte ?',
            'answer' => 'Si vous ne voyez pas la page d\'inscription, il faut cliquer sur le titre connexion.<br/>Vous devez ensuite entrez les informations nécessaire, vous devez avoir 18 ans ou plus et entrez un mot de passe de 12 caractères.',
            'topic' => 'register',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment ajouter un séjour ?',
            'answer' => 'Il faut aller sur la page d\'un séjour et utiliser une des 3 options d\'achat disponnibles.',
            'topic' => 'cart',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment personnaliser son achat ?',
            'answer' => 'Il suffit de changer le nombre de personnes et de cliquer sur modifier.',
            'topic' => 'cart',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment visualiser un séjour de son panier ?',
            'answer' => 'Il suffit de cliquer sur le titre du séjour pour visualiser le séjour.',
            'topic' => 'cart',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment supprimer un séjour de son panier ?',
            'answer' => 'Il suffit de cliquer sur le bouton supprimer.',
            'topic' => 'cart',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment se déconnecter ?',
            'answer' => 'Il suffit de cliquer sur le bouton déconnexion.',
            'topic' => 'account',
        ]);

        DB::table('chat_bot_questions')->insert([
            'question' => 'Comment entrer un code promo ?',
            'answer' => 'Il faut saisir le code promo dans le champ prévu a cet effet et cliquer sur le bouton appliquer.',
            'topic' => 'payment',
        ]);
    }
}
