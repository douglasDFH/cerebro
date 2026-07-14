<template>
  <div class="bg-white rounded-xl shadow-lg p-6 mb-8">
    <h2 class="text-2xl font-bold text-gray-900 mb-6 flex items-center">
      <svg class="w-6 h-6 mr-2 text-indigo-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"></path>
      </svg>
      Producción por Máquina
    </h2>
    <div v-if="produccionPorMaquina && produccionPorMaquina.length > 0" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <div v-for="prod in produccionPorMaquina" :key="prod?.maquina?.id || Math.random()"
           class="bg-gradient-to-r from-indigo-50 to-blue-50 rounded-lg p-4 border border-indigo-200">
        <div class="flex items-center justify-between">
          <div>
            <h3 class="font-semibold text-gray-900">{{ prod?.maquina?.nombre || 'Máquina desconocida' }}</h3>
            <p class="text-2xl font-bold text-indigo-600">{{ formatNumber(prod?.total_kg) }} KG</p>
            <p class="text-sm text-gray-600">{{ prod?.producciones || 0 }} producciones</p>
          </div>
          <div class="w-12 h-12 bg-indigo-100 rounded-full flex items-center justify-center">
            <svg class="w-6 h-6 text-indigo-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"></path>
            </svg>
          </div>
        </div>
      </div>
    </div>
    <div v-else class="text-center py-8 text-gray-500">
      <svg class="w-12 h-12 mx-auto mb-4 text-gray-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"></path>
      </svg>
      <p>No hay datos de producción disponibles</p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    produccionPorMaquina: {
      type: Array,
      default: () => []
    }
  },

  methods: {
    formatNumber(value) {
      if (value === null || value === undefined) return '0';
      const num = parseFloat(value);
      return isNaN(num) ? '0' : num.toLocaleString('es-ES', { maximumFractionDigits: 2 });
    }
  }
};
</script>