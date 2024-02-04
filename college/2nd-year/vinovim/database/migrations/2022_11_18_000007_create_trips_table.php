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
        Schema::create('trips', function (Blueprint $table) {
            $table->id();
            $table->unsignedInteger('participant_categories_id');
            $table->unsignedInteger('trip_categories_id');
            $table->unsignedInteger('vineyard_categories_id');
            $table->unsignedInteger('theme_id');
            $table->unsignedInteger('destination_id');
            $table->unsignedInteger('wine_trail_id');
            $table->unsignedInteger('image_id');
            $table->string('title', 100)->index();
            $table->smallInteger('duration');
            $table->string('description', 512);
            $table->timestamps();

            $table->foreign('participant_categories_id')->references('id')->on('participant_categories');
            $table->foreign('trip_categories_id')->references('id')->on('trip_categories');
            $table->foreign('vineyard_categories_id')->references('id')->on('vineyard_categories');
            $table->foreign('theme_id')->references('id')->on('themes');
            $table->foreign('destination_id')->references('id')->on('destinations');
            $table->foreign('wine_trail_id')->references('id')->on('wine_trails');
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
        Schema::dropIfExists('trips');
    }
};
