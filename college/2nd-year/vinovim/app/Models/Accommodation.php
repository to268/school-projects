<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Accommodation extends Model
{
    use HasFactory;

    public function accommodation_stages()
    {
        return $this->belongsToMany(Stage::class, 'accommodation_stages');
    }
}
