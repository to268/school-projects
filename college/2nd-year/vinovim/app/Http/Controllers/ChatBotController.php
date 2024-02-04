<?php

namespace App\Http\Controllers;

use App\Models\ChatBotQuestion;
use Illuminate\Support\Facades\Request;

class ChatBotController extends Controller
{
    public function index()
    {
        $topic = Request::input('topic');

        if ($topic === null)
            $questions = ChatBotQuestion::all();
        else
            $questions = ChatBotQuestion::where('topic', '=', $topic)->get();

        return $questions->toJson();
    }

    public function guide()
    {
        return view('guide');
    }
}
