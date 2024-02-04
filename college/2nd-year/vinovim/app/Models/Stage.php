<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Stage extends Model
{
    use HasFactory;

    public function trip()
    {
        return $this->belongsTo(Trip::class);
    }

    public function tour_stages()
    {
        return $this->belongsToMany(Tour::class, 'tour_stages');
    }

    public function accommodation_stages()
    {
        return $this->belongsToMany(Accommodation::class,
                                    'accommodation_stages');
    }

    public function meal_stages()
    {
        return $this->belongsToMany(Meal::class, 'meal_stages');
    }

    public function activity_stages()
    {
        return $this->belongsToMany(Activity::class,
                                    'activity_stages');
    }

    public function image()
    {
        return $this->belongsTo(Image::class);
    }
}
