<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AccountsController;
use App\Http\Controllers\CartController;
use App\Http\Controllers\ChatBotController;
use App\Http\Controllers\PaymentController;
use App\Http\Controllers\TripsController;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', [TripsController::class, 'index']);
Route::post('/', [TripsController::class, 'search']);
Route::get('/trip/{id?}', [TripsController::class, 'read']);
Route::get('/trip/{id}/gift', [TripsController::class, 'gift']);
Route::get('/trip/{id}/booking', [TripsController::class, 'booking']);
Route::get('/trip/{id}/customize', [TripsController::class, 'customize']);
/* This is a CRUD example */
/* Route::post('/trip/create', [TripsController::class, 'create']); */
/* Route::delete('/trip/{id}', [TripsController::class, 'delete']); */
/* Route::delete('/trip/{id}/update', [TripsController::class, 'update']); */

Route::get('/login', [AccountsController::class, 'login'])->name('login');
Route::get('/register', [AccountsController::class, 'register']);
Route::get('/account', [AccountsController::class, 'account'])->middleware('auth');
Route::post('/login', [AccountsController::class, 'handle_login']);
Route::post('/register', [AccountsController::class, 'handle_register']);
Route::post('/logout', [AccountsController::class, 'logout'])->middleware('auth');
Route::post('/anonymize', [AccountsController::class, 'anonymize'])->middleware('auth');

Route::get('/cart', [CartController::class, 'cart']);
Route::post('/cart', [CartController::class, 'modify_cart']);
Route::delete('/cart/{id}', [CartController::class, 'delete_cart']);

Route::get('/payment', [PaymentController::class, 'payment'])->middleware('auth');
Route::post('/payment/promo_code', [PaymentController::class, 'promo_code'])->middleware('auth');
Route::post('/payment/proceed', [PaymentController::class, 'proceed'])->middleware('auth');

Route::get('/api/chatbot/questions', [ChatBotController::class, 'index']);
Route::get('/guide', [ChatBotController::class, 'guide']);
