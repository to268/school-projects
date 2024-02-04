<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Relations\Pivot;

class MealStage extends Pivot
{
    use HasFactory;

    public function meal()
    {
        return $this->belongsTo(Meal::class);
    }

    public function stage()
    {
        return $this->belongsTo(Stage::class);
    }
}
