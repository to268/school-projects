<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Activity extends Model
{
    use HasFactory;

    public function activity_stages()
    {
        return $this->belongsToMany(Stage::class, 'activity_stages');
    }
}
