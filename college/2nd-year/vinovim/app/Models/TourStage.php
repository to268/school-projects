<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class TourStage extends Model
{
    use HasFactory;

    public function tour()
    {
        return $this->belongsTo(Tour::class);
    }

    public function stage()
    {
        return $this->belongsTo(Stage::class);
    }
}
