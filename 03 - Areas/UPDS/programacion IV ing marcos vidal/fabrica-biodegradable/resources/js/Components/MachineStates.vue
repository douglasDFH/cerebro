<template>
  <div class="bg-white rounded-xl shadow-lg p-6">
    <h2 class="text-2xl font-bold text-gray-900 mb-6 flex items-center">
      <svg class="w-6 h-6 mr-2 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
      </svg>
      Estados de Máquinas (Tiempo Real)
    </h2>
    <div v-if="estadosMaquinas && estadosMaquinas.length > 0" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <div v-for="estado in estadosMaquinas" :key="estado?.maquina?.id || Math.random()"
           class="bg-gray-50 rounded-lg p-4 border">
        <div class="flex items-center justify-between mb-2">
          <h3 class="font-semibold text-gray-900">{{ estado?.maquina?.nombre || 'Máquina desconocida' }}</h3>
          <span :class="getEstadoClass(estado?.estado)"
                class="px-2 py-1 rounded-full text-xs font-medium">
            {{ getEstadoText(estado?.estado) }}
          </span>
        </div>
        <div class="space-y-1 text-sm text-gray-600">
          <p>OEE: <span class="font-medium">{{ formatOEE(estado?.oee_actual) }}</span></p>
          <p>Velocidad: <span class="font-medium">{{ formatNumber(estado?.velocidad_actual) }}</span></p>
          <p>KG Producidos: <span class="font-medium">{{ formatNumber(estado?.kg_producidos) }}</span></p>
          <p class="text-xs text-gray-500">Última actualización: {{ formatTime(estado?.updated_at) }}</p>
        </div>
      </div>
    </div>
    <div v-else class="text-center py-8 text-gray-500">
      <svg class="w-12 h-12 mx-auto mb-4 text-gray-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-2.5L13.732 4c-.77-.833-1.964-.833-2.732 0L4.082 16.5c-.77.833.192 2.5 1.732 2.5z"/>
      </svg>
      <p>No hay datos de máquinas disponibles</p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    estadosMaquinas: {
      type: Array,
      default: () => []
    }
  },

  methods: {
    formatNumber(value) {
      if (value === null || value === undefined) return '0';
      const num = parseFloat(value);
      return isNaN(num) ? '0' : num.toLocaleString('es-ES', { maximumFractionDigits: 2 });
    },

    formatOEE(value) {
      if (value === null || value === undefined) return '0.0%';
      const num = parseFloat(value);
      return isNaN(num) ? '0.0%' : num.toFixed(1) + '%';
    },

    formatTime(timestamp) {
      if (!timestamp) return 'Nunca';
      try {
        return new Date(timestamp).toLocaleTimeString('es-ES', {
          hour: '2-digit',
          minute: '2-digit',
          second: '2-digit'
        });
      } catch (error) {
        return 'Fecha inválida';
      }
    },

    getEstadoClass(estado) {
      switch (estado) {
        case 'operando':
        case 'Operando':
          return 'bg-green-100 text-green-800';
        case 'parada':
        case 'Parada':
          return 'bg-red-100 text-red-800';
        case 'mantenimiento':
        case 'Mantenimiento':
          return 'bg-yellow-100 text-yellow-800';
        default:
          return 'bg-gray-100 text-gray-800';
      }
    },

    getEstadoText(estado) {
      switch (estado) {
        case 'operando':
        case 'Operando':
          return 'Operando';
        case 'parada':
        case 'Parada':
          return 'Parada';
        case 'mantenimiento':
        case 'Mantenimiento':
          return 'Mantenimiento';
        default:
          return estado || 'Desconocido';
      }
    }
  }
};
</script>