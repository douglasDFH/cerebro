<template>
  <div class="min-h-screen bg-gradient-to-br from-blue-900 via-blue-800 to-cyan-900 text-white font-mono">
    <div class="container mx-auto px-4 py-8">
      <!-- Cabecera -->
      <div class="text-center mb-12">
        <h1 class="text-8xl font-bold mb-4">{{ maquina.nombre }}</h1>
        <p class="text-5xl">Operador: {{ user.name }} | Turno: {{ turnoActual }}</p>
      </div>

      <!-- Contadores gigantes -->
      <div class="grid grid-cols-1 md:grid-cols-3 gap-12 mb-16">
        <div class="bg-black/60 backdrop-blur rounded-3xl p-12 text-center border-4 border-green-500">
          <h2 class="text-5xl mb-6 text-green-400">KG PRODUCIDOS</h2>
          <p class="text-9xl font-bold">{{ Number(estado.kg_producidos).toFixed(1) }}</p>
          <p class="text-4xl mt-4">Objetivo: {{ produccion?.cantidad_objetivo_kg || '---' }} kg</p>
        </div>

        <div class="bg-black/60 backdrop-blur rounded-3xl p-12 text-center border-4 border-yellow-500">
          <h2 class="text-5xl mb-6 text-yellow-400">OEE ACTUAL</h2>
          <p class="text-9xl font-bold" :class="estado.oee_actual >= 85 ? 'text-green-400' : 'text-red-400'">
            {{ estado.oee_actual.toFixed(1) }}%
          </p>
        </div>

        <div class="bg-black/60 backdrop-blur rounded-3xl p-12 text-center border-4 border-cyan-500">
          <h2 class="text-5xl mb-6 text-cyan-400">VELOCIDAD</h2>
          <p class="text-9xl font-bold">{{ Math.round(estado.velocidad_actual) }} <span class="text-6xl">r/min</span></p>
        </div>
      </div>

      <!-- Estado y botones gigantes -->
      <div class="text-center">
        <div class="inline-block bg-black/80 rounded-3xl px-20 py-10 mb-10">
          <p class="text-8xl font-bold" :class="estado.estado === 'Corriendo' ? 'text-green-500' : 'text-red-500'">
            {{ estado.estado.toUpperCase() }}
          </p>
        </div>

        <div class="space-x-12">
          <button
            v-if="estado.estado === 'Parada'"
            @click="iniciarProduccion"
            class="bg-green-600 hover:bg-green-700 text-white text-9xl font-bold px-32 py-24 rounded-3xl shadow-2xl transform hover:scale-105 transition-all">
            INICIAR PRODUCCIÓN
          </button>

          <button
            v-if="estado.estado === 'Corriendo'"
            @click="detenerProduccion"
            class="bg-red-600 hover:bg-red-700 text-white text-9xl font-bold px-32 py-24 rounded-3xl shadow-2xl transform hover:scale-105 transition-all">
            DETENER
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { usePage } from '@inertiajs/vue3'
import Echo from 'laravel-echo'

const maquina = usePage().props.maquina
const user = usePage().props.auth.user
const estado = ref({ kg_producidos: 0, oee_actual: 0, velocidad_actual: 0, estado: 'Parada' })
const produccion = ref(null)

const turnoActual = new Date().getHours() < 14 ? 'Mañana' : new Date().getHours() < 22 ? 'Tarde' : 'Noche'

onMounted(() => {
  window.Echo.channel(`maquina.${maquina.id}`)
    .listen('ProduccionActualizada', (e) => {
      estado.value = e.estado
      produccion.value = e.produccion
    })
})

function iniciarProduccion() {
  // Aquí irá la llamada al controlador para iniciar producción
  alert('¡Producción iniciada en ' + maquina.nombre + '!')
}

function detenerProduccion() {
  alert('¡Producción detenida!')
}
</script>