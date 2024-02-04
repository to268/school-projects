<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('clients', function (Blueprint $table) {
            $table->id();
            $table->unsignedInteger('gender_id');
            $table->string('last_name', 50);
            $table->string('first_name', 50);
            $table->string('email', 128)->unique()->index();
            $table->char('password', 64)->index();
            $table->date('birthday');
            $table->boolean('is_admin');
            $table->unsignedInteger('postal_code')->nullable();
            $table->rememberToken();
            $table->timestamps();

            $table->foreign('gender_id')->references('id')->on('genders');
        });

        /*
        CREATE OR REPLACE FUNCTION ps_rgpd_anonymization ()
        RETURNS text
        AS
        $$
        DECLARE
        BEGIN
            IF clients.updated_at < NOW() - INTERVAL '5 year' THEN
                UPDATE clients SET first_name = 'Anonyme', last_name = 'Anonyme', email = 'Anonyme@' || md5(random()::text), phone = 0000000000, password = md5(random()::text),gender_id = 3, code_postal = 'Anonyme', city = 'Anonyme', birthday = '1000-01-01', postal_code = 0, updated_at = NOW() WHERE id = clients.id;
            END IF;
        END;
        $$ LANGUAGE 'plpgsql'
        */
        DB::unprepared('CREATE OR REPLACE FUNCTION ps_rgpd_anonymization () RETURNS text AS $$ DECLARE BEGIN IF clients.updated_at < NOW() - INTERVAL \'5 year\' THEN UPDATE clients SET first_name = \'Anonyme\', last_name = \'Anonyme\', email = \'Anonyme@\' || md5(random()::text), phone = 0000000000, password = md5(random()::text),gender_id = 3, code_postal = \'Anonyme\', city = \'Anonyme\', birthday = \'1000-01-01\', postal_code = 0, updated_at = NOW() WHERE id = clients.id; END IF; END; $$ LANGUAGE \'plpgsql\'');
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('clients');
    }
};
