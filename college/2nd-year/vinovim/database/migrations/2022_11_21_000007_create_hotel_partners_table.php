<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
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
        Schema::create('hotel_partners', function (Blueprint $table) {
            $table->unsignedInteger('id');
            $table->unsignedInteger('partner_id')->unique();
            $table->unsignedInteger('address_id');
            $table->string('name', 50)->index();
            $table->string('email', 50);
            $table->string('phone', 12);
            $table->string('category', 50);
            $table->unsignedSmallInteger('rooms');
            $table->timestamps();
            $table->primary(['id', 'partner_id']);

            $table->foreign('address_id')->references('id')->on('addresses');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('hotel_partners');
    }
};
