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
        Schema::create('accommodation_stages', function (Blueprint $table) {
            $table->unsignedInteger('accommodation_id');
            $table->unsignedInteger('stage_id');
            $table->timestamps();
            $table->primary(['accommodation_id', 'stage_id']);

            $table->foreign('accommodation_id')->references('id')->on('accommodations');
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
        Schema::dropIfExists('accommodation_stages');
    }
};
