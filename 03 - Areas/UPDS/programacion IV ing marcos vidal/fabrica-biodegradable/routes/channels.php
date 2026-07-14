<?php

use Illuminate\Support\Facades\Broadcast;

Broadcast::channel('App.Models.User.{id}', function ($user, $id) {
    return (int) $user->id === (int) $id;
});

Broadcast::channel('maquina.{id}', function ($user, $id) {
    return true;
});

Broadcast::channel('dashboard', function ($user) {
    return true;
});

Broadcast::channel('produccion', function ($user) {
    return true;
});

Broadcast::channel('maquina-estado', function ($user) {
    return true;
});
