<?php

namespace App\Providers;

use App\Services\Contracts\ProduccionServiceInterface;
use App\Services\ProduccionService;
use Illuminate\Support\ServiceProvider;


class AppServiceProvider extends ServiceProvider
{
    /**
     * Register any application services.
     */
    public function register(): void
    {
        $this->app->bind(ProduccionServiceInterface::class, ProduccionService::class);
    }

    /**
     * Bootstrap any application services.
     */
    public function boot(): void
    {
        //
    }
}
