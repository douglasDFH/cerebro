<?php

namespace App\Services\Contracts;

interface ProduccionServiceInterface
{
    /**
     * Registrar una producción simulada
     *
     * @param int $maquinaId
     * @param float $kgIncremento
     * @param float $oee
     * @param float $velocidad
     * @return array
     */
    public function registrarProduccion(int $maquinaId, float $kgIncremento, float $oee, float $velocidad): array;

    /**
     * Obtener estadísticas de producción del día
     *
     * @return array
     */
    public function getEstadisticasDia(): array;

    /**
     * Obtener producción por máquina del día
     *
     * @return array
     */
    public function getProduccionPorMaquina(): array;
}