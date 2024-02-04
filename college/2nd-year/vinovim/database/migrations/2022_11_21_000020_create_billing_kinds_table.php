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
        Schema::create('billing_kinds', function (Blueprint $table) {
            $table->unsignedInteger('kind_id');
            $table->unsignedInteger('billing_id');
            $table->timestamps();
            $table->primary(['kind_id', 'billing_id']);

            $table->foreign('kind_id')->references('id')->on('billing_methods');
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
        Schema::dropIfExists('billing_kinds');
    }
};
