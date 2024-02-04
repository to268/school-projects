<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Trip extends Model
{
    use HasFactory;

    public function image()
    {
        return $this->belongsTo(Image::class);
    }

    public function theme()
    {
        return $this->belongsTo(Theme::class);
    }

    public function participant()
    {
        return $this->belongsTo(ParticipantCategory::class, 'participant_categories_id');
    }

    public function vineyard()
    {
        return $this->belongsTo(VineyardCategory::class, 'vineyard_categories_id');
    }

    public function wine_trail()
    {
        return $this->belongsTo(WineTrail::class);
    }

    public function stages()
    {
        return $this->hasMany(Stage::class);
    }

    public function comments()
    {
        return $this->hasMany(Trip::class);
    }
}
