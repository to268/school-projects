<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Meal extends Model
{
    use HasFactory;

    public function meal_stages()
    {
        return $this->belongsToMany(Stage::class, 'meal_stages');
    }
}
