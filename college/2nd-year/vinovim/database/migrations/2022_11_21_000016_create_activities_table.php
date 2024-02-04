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
        Schema::create('activities', function (Blueprint $table) {
            $table->id();
            $table->unsignedInteger('partner_id')->unique();
            $table->unsignedInteger('address_id');
            $table->unsignedInteger('image_id');
            $table->string('name', 50)->index();
            $table->string('description', 512);
            $table->time('time');
            $table->string('details', 512);
            $table->unsignedDecimal('price_per_person', 6, 2)->index();
            $table->timestamps();

            $table->foreign('partner_id', 'fk_activities_hotel_partner_id')->references('partner_id')->on('hotel_partners');
            $table->foreign('partner_id', 'fk_activities_cave_partner_id')->references('partner_id')->on('cave_partners');
            $table->foreign('partner_id', 'fk_activities_restaurant_partner_id')->references('partner_id')->on('restaurant_partners');
            $table->foreign('partner_id', 'fk_activities_other_company_id')->references('partner_id')->on('other_companies');
            $table->foreign('image_id')->references('id')->on('images');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('activities');
    }
};
