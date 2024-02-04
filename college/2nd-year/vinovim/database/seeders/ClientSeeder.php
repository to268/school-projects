<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ClientSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Chaput',
            'first_name' => 'Ferdinand',
            'email' => 'c_ferdinand3413@google.com',
            'password' => '6fef3e2b0cc96dafda1ee5bdf8682bec8afef3256184b69312e5892acfae7a50',
            'birthday' => '1961-03-18',
            'postal_code' => '94',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Tremblay',
            'first_name' => 'Callum',
            'email' => 'callum_tremblay2685@protonmail.fr',
            'password' => 'b76c07a791d44b7e2fe339324a3caf9866b3f6a0cc906c570962f529e337d0a4',
            'birthday' => '1964-01-20',
            'postal_code' => '34',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Janssens',
            'first_name' => 'Arden',
            'email' => 'j.arden@outlook.fr',
            'password' => 'da058a086b994866feccd6e7d6ac3962e62448a1cf0faced00ad42d09000e3ab',
            'birthday' => '1970-07-20',
            'postal_code' => '38',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Bouwmeester',
            'first_name' => 'Hanae',
            'email' => 'hbouwmeester@protonmail.fr',
            'password' => '4b0713db82ce8676ee2c80e1d97af49bd83572658273ad3c7864886d1e8043a3',
            'birthday' => '1990-03-27',
            'postal_code' => '12',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Hoedemaker',
            'first_name' => 'Kaseem',
            'email' => 'h.kaseem@outlook.fr',
            'password' => '2e34bde6fb0870f11d3990dd5777b7946417c9ba7e0a4d51364697d02b8e42e6',
            'birthday' => '1955-04-07',
            'postal_code' => '24',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Van Alphen',
            'first_name' => 'Driscoll',
            'email' => 'v.driscoll@icloud.com',
            'password' => '87c835af038817ac6200c19624af9fa5bd1dabd4e11093ecd3b4dd5b39feed72',
            'birthday' => '1952-04-26',
            'postal_code' => '91',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Berg',
            'first_name' => 'Theodore',
            'email' => 'bergtheodore@google.com',
            'password' => 'a2b678846e1e65a23a91f2d6185a573fbc7814e76a3296a5bc36a8e56a029ce2',
            'birthday' => '1966-08-01',
            'postal_code' => '47',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Fontaine',
            'first_name' => 'Olivia',
            'email' => 'folivia@icloud.com',
            'password' => 'bce60c2f8fc04093a7ecf9eb845e4fd9e4adb68c5a0cdb7299af93c19043087f',
            'birthday' => '1973-09-24',
            'postal_code' => '11',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Jonker',
            'first_name' => 'Griffin',
            'email' => 'g_jonker@protonmail.fr',
            'password' => '30dcfef7c17e0cb1befacce1b94b63634bf8ceec0ebb76dab0d26cbf714542e5',
            'birthday' => '1962-11-20',
            'postal_code' => '95',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Berger',
            'first_name' => 'Fallon',
            'email' => 'f_berger@aol.fr',
            'password' => '95733a113051a2f898c43f153610140e7d998c0bb8996d9b09d65b05d23221d3',
            'birthday' => '1972-07-24',
            'postal_code' => '5',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Aaldenberg',
            'first_name' => 'Pamela',
            'email' => 'a_pamela@aol.fr',
            'password' => 'b1b6c94259be1beebb768873eb4261071ef7a4612787cc6b34ac1af958214667',
            'birthday' => '1983-08-15',
            'postal_code' => '73',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Dumont',
            'first_name' => 'Brady',
            'email' => 'bdumont4931@aol.com',
            'password' => '94a31f01b2c6746569c4a3f12d197263385b371dadde88a40063266e416bc819',
            'birthday' => '1996-10-26',
            'postal_code' => '34',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Koopman',
            'first_name' => 'Elton',
            'email' => 'kelton2135@aol.fr',
            'password' => 'f996112e41c0da6538ef65ba44ce367d96f725afc0da6aa27ca8166c50b9e4cb',
            'birthday' => '1983-05-25',
            'postal_code' => '59',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Mertens',
            'first_name' => 'Bianca',
            'email' => 'bianca.mertens@yahoo.fr',
            'password' => '2fb00c99a03570e93447f2c0762033d62f55f0c4761db62c515afb11deb58887',
            'birthday' => '1958-06-21',
            'postal_code' => '31',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Baardwijk',
            'first_name' => 'Liberty',
            'email' => 'l_baardwijk@aol.com',
            'password' => 'b08f9d885a890e626c771e1b92ff165770c540bf6c41ec53d1dc011980d76e72',
            'birthday' => '1949-10-06',
            'postal_code' => '62',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Fontaine',
            'first_name' => 'Michelle',
            'email' => 'fmichelle@hotmail.com',
            'password' => 'd06fe4b935bc1c82f63c033da92968c92da7fad7565c66b5824a030dad18ea23',
            'birthday' => '1956-06-27',
            'postal_code' => '79',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Berger',
            'first_name' => 'Charles',
            'email' => 'c_berger6689@protonmail.fr',
            'password' => '4ef4bf1977ae70cace24e5bba24058642b822ac05e5e12d508bb0af35c6b05c7',
            'birthday' => '1989-12-01',
            'postal_code' => '83',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Dumont',
            'first_name' => 'Aidan',
            'email' => 'daidan4205@protonmail.com',
            'password' => 'bab7dd375beb4155d18d678b40a10c941312bb3b3053272e3d80fa7a4b5be7e0',
            'birthday' => '1945-02-23',
            'postal_code' => '63',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Van Assen',
            'first_name' => 'Shannon',
            'email' => 'v-shannon@icloud.fr',
            'password' => '0be43aa9bb8b9eb111ec2b3dccba3368fb9ce2bc4948a94d7fd6faf4fe7a65e1',
            'birthday' => '1994-10-19',
            'postal_code' => '1',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Lachapelle',
            'first_name' => 'Nevada',
            'email' => 'l.nevada2029@google.fr',
            'password' => '0b6c05bb631f830464b6eeeac051b56a432faa92d93325cc82394555a1012df1',
            'birthday' => '1980-05-15',
            'postal_code' => '51',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Geelen',
            'first_name' => 'Hasad',
            'email' => 'g.hasad7316@protonmail.com',
            'password' => '3629ebc9e7e7af8fdbf4d42b515aecfd1ddae26610686ad2b020ef321eaa96b3',
            'birthday' => '1993-06-10',
            'postal_code' => '52',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
'last_name' => 'Kloet',
            'first_name' => 'Frances',
            'email' => 'franceskloet@icloud.fr',
            'password' => '0083e99c4981b21e25efff424fdb04f890349f24a869bd73900ad5e197c20037',
            'birthday' => '1958-05-23',
            'postal_code' => '58',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Deforest',
            'first_name' => 'Carissa',
            'email' => 'c.deforest9207@aol.com',
            'password' => '2d19352f5727f41efc21052a5cd7ab5fbcf7c9f5caa4d33556291e1eece2ab5d',
            'birthday' => '1945-04-17',
            'postal_code' => '68',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
'last_name' => 'Dam',
            'first_name' => 'Kimberly',
            'email' => 'kimberlydam8544@aol.fr',
            'password' => '479be479580fe3e0eb88a11b93d25679d494b1e513b468fb70232e3c267b7ea9',
            'birthday' => '1980-01-06',
            'postal_code' => '53',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Lavigne',
            'first_name' => 'David',
            'email' => 'dlavigne5462@hotmail.fr',
            'password' => '1cd8bb330104aa9772b6d30159f251a1247bc3d02c4411ab06cbbe78bd073c03',
            'birthday' => '1967-01-29',
            'postal_code' => '46',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Savatier',
            'first_name' => 'Ronan',
            'email' => 'ronan-savatier7615@yahoo.com',
            'password' => '9613881596b255bc3057f5134e06f183b85152017cde9888f8e9d223da693e26',
            'birthday' => '1989-05-10',
            'postal_code' => '20',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Lavigne',
            'first_name' => 'Mason',
            'email' => 'lmason8427@yahoo.com',
            'password' => '91d7f8bf2ac515005152ec78919b0753e993398eb876f5e60af234b0266fb752',
            'birthday' => '1957-11-19',
            'postal_code' => '38',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Van Assen',
            'first_name' => 'Ulla',
            'email' => 'v.ulla@outlook.fr',
            'password' => '4194e633cf5e19852f0d6b18910caba79a6e50f7bbc8bb049f3d308959f9b356',
            'birthday' => '1981-11-08',
            'postal_code' => '70',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Archambault',
'first_name' => 'Charles',
'email' => 'a.charles5699@hotmail.com',
            'password' => '26536e88686ed0a4500c23e3f9183a1bfeaab9a36d0dd066595517d10b4852e7',
            'birthday' => '1987-02-25',
            'postal_code' => '83',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Rademaker',
            'first_name' => 'Gage',
            'email' => 'rademakergage@aol.fr',
            'password' => '836fb8d1aca6f9252b0da39e66e28baa8daf48cc94f890ffa8f8d19f9476865f',
            'birthday' => '2003-06-28',
            'postal_code' => '36',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Tremblay',
            'first_name' => 'Whilemina',
            'email' => 'wtremblay@outlook.fr',
            'password' => '76562f109c1e7025f3709e1f8280d014aedae7f39bf8c6db82eaf1ad8f84033a',
            'birthday' => '2001-06-21',
            'postal_code' => '7',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Smet',
            'first_name' => 'Michelle',
            'email' => 'michelle-smet3545@protonmail.fr',
            'password' => '21844300655342b7a7e3edb506526105fcceab4977685a9408becdbdd438aec1',
            'birthday' => '1990-08-21',
            'postal_code' => '83',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Koopman',
            'first_name' => 'Sonia',
            'email' => 's-koopman1727@google.com',
            'password' => 'c91781fcf4c27e35ba62103abedb25b32b476a91c7bf7aadeb9d34e06a3bc79e',
            'birthday' => '1975-05-12',
            'postal_code' => '83',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Berger',
            'first_name' => 'Fritz',
            'email' => 'b_fritz@outlook.com',
            'password' => '63826eab035338a178673f870b3adbe571b9367f6c461aceb4b21cf99836b3d0',
            'birthday' => '1987-03-29',
            'postal_code' => '51',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Heeren',
            'first_name' => 'Flynn',
            'email' => 'flynnheeren@yahoo.com',
            'password' => 'bc08a881a7477da54d56814672b9a59791ff0a4e7d92840ffa7efcaf8166a143',
            'birthday' => '1977-01-18',
            'postal_code' => '58',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Berg',
            'first_name' => 'Kai',
            'email' => 'kai-berg8360@aol.com',
            'password' => '003036c32b399852d8c7dbe5fdd0db54ce4384d3ab721cdd7f7d25360c32e69e',
            'birthday' => '2001-12-08',
            'postal_code' => '83',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Holt',
            'first_name' => 'Dexter',
            'email' => 'holt.dexter9491@outlook.com',
            'password' => 'e500df9ecf007d1c657a8020dbaf8e39b74d02fbcc04157c33b7ace02e3d5831',
            'birthday' => '1953-02-24',
            'postal_code' => '58',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Segal',
            'first_name' => 'Timothy',
            'email' => 'tsegal@icloud.com',
            'password' => '556f9e3d3243bd0a7ce294e127363f0fadb28848d677c6f04ec1dc76d2650e6f',
            'birthday' => '1959-04-13',
            'postal_code' => '9',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Royer',
            'first_name' => 'Pamela',
            'email' => 'p.royer8849@icloud.fr',
            'password' => '5e47dfd0a2b3fddeed173efe2b81017f0bce8824eff2ff2568b290d8d2caab5a',
            'birthday' => '1999-10-27',
            'postal_code' => '81',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Victor',
            'first_name' => 'Martin',
            'email' => 'victor_martin2838@yahoo.fr',
            'password' => '484a3c595e1e0c37d0bb1ccc01f12e4f37b685012eae75a3453eb7c0d0c7efa9',
            'birthday' => '1980-08-04',
            'postal_code' => '32',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Garcon',
            'first_name' => 'Paloma',
            'email' => 'paloma.garcon9602@google.com',
            'password' => '24f69a405621990d6568d5d90c7b3e44f98e54558cd855fd9ea9cc58d794b413',
            'birthday' => '1957-05-11',
            'postal_code' => '47',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Duval',
            'first_name' => 'Barbara',
            'email' => 'dbarbara@yahoo.fr',
            'password' => '752c93b5e04116c4e948d9bcc2a676c10b2fe47bb4a3b64b518b29b61772bf8f',
            'birthday' => '1992-05-15',
            'postal_code' => '8',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Chastain',
            'first_name' => 'Glenna',
            'email' => 'cglenna3004@icloud.fr',
            'password' => '06a7ae3af7ff6eda53d2fd5fe579ed0b44b368fcb8d8dd3e4ec0398cb7cd6b31',
            'birthday' => '1963-10-23',
            'postal_code' => '79',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Van Der Aart',
            'first_name' => 'Jackson',
            'email' => 'vanderaart_jackson@hotmail.com',
            'password' => 'f4da4963d62e51562169be4d1d1d6fd8a330dcd7909e0aed5e2caa858a58b0db',
            'birthday' => '1984-02-11',
            'postal_code' => '13',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'De Witte',
            'first_name' => 'John',
            'email' => 'john_dewitte@protonmail.com',
            'password' => '1109af4dc27de94f3b913a7e0630f6abb3a96ea5f13c67bf590328f65ce29b0a',
            'birthday' => '1992-08-03',
            'postal_code' => '65',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 3,
            'last_name' => 'Achthoven',
            'first_name' => 'Clio',
            'email' => 'aclio@google.com',
            'password' => 'b69150977397c1e498dbe7a7de2acd3d30e764c09e0376ee6d4f6557a459c672',
            'birthday' => '1968-09-30',
            'postal_code' => '51',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Fabre',
            'first_name' => 'Chandler',
            'email' => 'f.chandler9098@hotmail.fr',
            'password' => '71908dadf654bb7807f8cf8c7878194293e25156fbb4861a1abff8abb59f04ae',
            'birthday' => '1957-12-03',
            'postal_code' => '10',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 2,
            'last_name' => 'Proulx',
            'first_name' => 'Kitra',
            'email' => 'kitra.proulx7358@hotmail.com',
            'password' => 'd43ec5b093d77600b14a36798911becd59e9121a2c7f2af71cad7bcc17f8ebd9',
            'birthday' => '1977-09-10',
            'postal_code' => '63',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Elzinga',
            'first_name' => 'Xaviera',
            'email' => 'e_xaviera@aol.com',
            'password' => 'a489e5f4d287ec640f7f75389a18784665102c8b04cbce4558cbc29e9fb94709',
            'birthday' => '1971-03-13',
            'postal_code' => '11',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Tasse',
            'first_name' => 'Chava',
            'email' => 'c.tasse@google.fr',
            'password' => '7d5073b712892a8b8761888c97b3b412cc8dd0f69f4353d6f50e4d053f791fd5',
            'birthday' => '1954-11-13',
            'postal_code' => '7',
            'is_admin' => 'false',
        ]);

        DB::table('clients')->insert([
            'gender_id' => 1,
            'last_name' => 'Doe',
            'first_name' => 'John',
            'email' => 'john.doe@vinovim.fr',
            'password' => '6ee5bffc05cf04d94b00411cf491a80c37ad0f37c377e34064521c2e530843fb',
            'birthday' => '1954-11-13',
            'is_admin' => 'true',
        ]);
    }
}
