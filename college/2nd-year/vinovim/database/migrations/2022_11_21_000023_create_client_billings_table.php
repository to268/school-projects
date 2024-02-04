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
        Schema::create('client_billings', function (Blueprint $table) {
            $table->unsignedInteger('client_id');
            $table->unsignedInteger('billing_id');
            $table->timestamps();
            $table->primary(['client_id', 'billing_id']);

            $table->foreign('client_id')->references('id')->on('clients');
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
        Schema::dropIfExists('client_billings');
    }
};
