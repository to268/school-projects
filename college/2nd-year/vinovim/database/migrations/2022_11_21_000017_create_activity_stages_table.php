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
        Schema::create('activity_stages', function (Blueprint $table) {
            $table->unsignedInteger('activity_id');
            $table->unsignedInteger('stage_id');
            $table->timestamps();
            $table->primary(['activity_id', 'stage_id']);

            $table->foreign('activity_id')->references('id')->on('activities');
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
        Schema::dropIfExists('activity_stages');
    }
};
