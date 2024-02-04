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
        Schema::create('billings_trip', function (Blueprint $table) {
            $table->unsignedInteger('trip_id');
            $table->unsignedInteger('billing_id');
            $table->timestamps();
            $table->primary(['trip_id', 'billing_id']);

            $table->foreign('trip_id')->references('id')->on('trips');
            $table->foreign('billing_id')->references('id')->on('billings');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('billings_trip');
    }
};