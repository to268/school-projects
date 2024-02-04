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
        Schema::create('tour_stages', function (Blueprint $table) {
            $table->unsignedInteger('tour_id');
            $table->unsignedInteger('stage_id');
            $table->timestamps();
            $table->primary(['tour_id', 'stage_id']);

            $table->foreign('tour_id')->references('id')->on('tours');
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
        Schema::dropIfExists('tour_stages');
    }
};
