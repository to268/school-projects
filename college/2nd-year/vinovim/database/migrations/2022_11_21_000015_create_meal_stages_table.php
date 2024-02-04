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
        Schema::create('meal_stages', function (Blueprint $table) {
            $table->unsignedInteger('meal_id');
            $table->unsignedInteger('stage_id');
            $table->timestamps();
            $table->primary(['meal_id', 'stage_id']);

            $table->foreign('meal_id')->references('id')->on('meals');
            $table->foreign('stage_id')->references('id')->on('stages');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('meal_stages');
    }
};
