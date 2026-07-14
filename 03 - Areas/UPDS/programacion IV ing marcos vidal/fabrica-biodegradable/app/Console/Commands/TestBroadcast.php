<?php

namespace App\Console\Commands;

use Illuminate\Console\Command;

class TestBroadcast extends Command
{
    /**
     * The name and signature of the console command.
     *
     * @var string
     */
    protected $signature = 'test:broadcast';

    /**
     * The console command description.
     *
     * @var string
     */
    protected $description = 'Test broadcast event';

    /**
     * Execute the console command.
     */
    public function handle()
    {
        broadcast(new \App\Events\MaquinaEstadoActualizado(\App\Models\MaquinaEstadoVivo::first()));
        $this->info('Broadcast enviado');
    }
}
