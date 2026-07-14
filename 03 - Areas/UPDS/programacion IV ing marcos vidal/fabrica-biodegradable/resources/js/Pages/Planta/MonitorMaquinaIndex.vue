<template>
  <div class="container mx-auto px-4 py-8">
    <h1 class="text-3xl font-bold mb-8">Monitor de Máquinas - Lista</h1>

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      <div v-for="maquina in maquinas" :key="maquina.id" class="bg-white rounded-lg shadow-md p-6">
        <h2 class="text-xl font-semibold mb-2">{{ maquina.nombre }}</h2>
        <p class="text-gray-600 mb-1">Código: {{ maquina.codigo }}</p>
        <p class="text-gray-600 mb-1">Tipo: {{ maquina.tipo?.nombre }}</p>
        <p class="text-gray-600 mb-1">Estado: {{ maquina.estado }}</p>
        <div v-if="maquina.estado_vivo" class="mt-4">
          <p class="text-sm text-gray-500">Estado Vivo: {{ maquina.estado_vivo.estado }}</p>
          <p class="text-sm text-gray-500">KG Producidos: {{ maquina.estado_vivo.kg_producidos }}</p>
        </div>
        <a :href="`/planta/monitor-maquina/${maquina.id}`" class="mt-4 inline-block bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
          Ver Monitor
        </a>
      </div>
    </div>
  </div>
</template>

<script setup>
import { Head } from '@inertiajs/vue3';

defineProps({
  maquinas: Array,
});
</script>