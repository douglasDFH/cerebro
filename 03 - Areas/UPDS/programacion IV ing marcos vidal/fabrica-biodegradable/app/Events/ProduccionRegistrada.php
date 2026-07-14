<?php

namespace App\Events;

use App\Models\Produccion;
use App\Models\MaquinaEstadoVivo;
use Illuminate\Broadcasting\Channel;
use Illuminate\Broadcasting\InteractsWithSockets;
use Illuminate\Foundation\Events\Dispatchable;
use Illuminate\Queue\SerializesModels;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class ProduccionRegistrada implements ShouldBroadcastNow
{
    use Dispatchable, InteractsWithSockets, SerializesModels;

    public Produccion $produccion;
    public MaquinaEstadoVivo $estado;

    /**
     * Create a new event instance.
     */
    public function __construct(Produccion $produccion, MaquinaEstadoVivo $estado)
    {
        $this->produccion = $produccion;
        $this->estado = $estado;
    }

    /**
     * Get the channels the event should broadcast on.
     *
     * @return array<int, \Illuminate\Broadcasting\Channel>
     */
    public function broadcastOn(): array
    {
        return [
            new Channel('dashboard'),
        ];
    }

    /**
     * The event's broadcast name.
     */
    public function broadcastAs(): string
    {
        return 'produccion.registrada';
    }

    /**
     * Get the data to broadcast.
     */
    public function broadcastWith(): array
    {
        return [
            'produccion' => $this->produccion->load('maquina'),
            'estado' => $this->estado,
            'estadisticas' => app(\App\Services\Contracts\ProduccionServiceInterface::class)->getEstadisticasDia(),
        ];
    }
}
