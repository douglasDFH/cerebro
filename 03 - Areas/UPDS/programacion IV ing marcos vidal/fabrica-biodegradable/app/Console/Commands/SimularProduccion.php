<?php

namespace App\Console\Commands;

use App\Events\MaquinaEstadoActualizado;
use App\Models\Maquina;
use App\Models\MaquinaEstadoVivo;
use Illuminate\Console\Command;

class SimularProduccion extends Command
{
    /**
     * The name and signature of the console command.
     *
     * @var string
     */
    protected $signature = 'simular:produccion {maquina_id}';

    /**
     * The console command description.
     *
     * @var string
     */
    protected $description = 'Simula producción en una máquina específica';

    /**
     * Execute the console command.
     */
    public function handle()
    {
        $maquinaId = $this->argument('maquina_id');
        $maquina = Maquina::find($maquinaId);

        if (! $maquina) {
            $this->error("Máquina con ID {$maquinaId} no encontrada.");

            return 1;
        }

        $this->info("Iniciando simulación de producción para máquina: {$maquina->nombre}");

        // Obtener o crear estado vivo
        $estado = MaquinaEstadoVivo::firstOrCreate(
            ['maquina_id' => $maquinaId],
            [
                'estado' => 'Produciendo',
                'kg_producidos' => 0,
                'velocidad_actual' => 100, // rpm
                'oee_actual' => 85.5,
            ]
        );

        $this->info("Estado inicial - KG: {$estado->kg_producidos}, OEE: {$estado->oee_actual}%");

        // Simulación en loop
        while (true) {
            // Incrementar producción
            $incremento = rand(5, 15) / 10; // 0.5 a 1.5 kg por segundo
            $estado->kg_producidos += $incremento;

            // Variar OEE ligeramente
            $estado->oee_actual = max(70, min(95, $estado->oee_actual + (rand(-5, 5) / 10)));

            // Variar velocidad
            $estado->velocidad_actual = max(80, min(120, $estado->velocidad_actual + rand(-2, 2)));

            $estado->save();

            // Broadcast el evento
            broadcast(new MaquinaEstadoActualizado($estado));

            $this->info('KG: '.number_format($estado->kg_producidos, 1).' | OEE: '.number_format($estado->oee_actual, 1)."% | Vel: {$estado->velocidad_actual} rpm");

            sleep(1); // Esperar 1 segundo
        }
    }
}
