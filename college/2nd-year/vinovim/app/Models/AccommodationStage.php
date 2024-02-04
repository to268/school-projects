<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class AccommodationStage extends Model
{
    use HasFactory;

    public function accommodation()
    {
        return $this->belongsTo(Accommodation::class);
    }

    public function stage()
    {
        return $this->belongsTo(Stage::class);
    }
}
