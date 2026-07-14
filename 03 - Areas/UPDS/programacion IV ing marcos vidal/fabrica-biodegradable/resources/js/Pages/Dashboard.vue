<template>
  <div class="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100">
    <div class="py-8">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <!-- Header -->
        <div class="mb-8">
          <h1 class="text-4xl font-bold text-gray-900 mb-2">🏭 Dashboard de Producción</h1>
          <p class="text-gray-600">Monitoreo en tiempo real de la fábrica biodegradable</p>
          <div class="mt-2 text-sm text-gray-500">
            Última actualización: {{ ultimaActualizacion }}
          </div>
        </div>

        <!-- Loading State -->
        <div v-if="loading" class="text-center py-12">
          <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600 mx-auto"></div>
          <p class="mt-4 text-gray-600">Cargando datos...</p>
        </div>

        <!-- Error State -->
        <div v-else-if="error" class="bg-red-50 border border-red-200 rounded-lg p-6 mb-8">
          <div class="flex">
            <div class="flex-shrink-0">
              <svg class="h-5 w-5 text-red-400" fill="currentColor" viewBox="0 0 20 20">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd"/>
              </svg>
            </div>
            <div class="ml-3">
              <h3 class="text-sm font-medium text-red-800">Error al cargar datos</h3>
              <div class="mt-2 text-sm text-red-700">{{ error }}</div>
            </div>
          </div>
        </div>

        <!-- Dashboard Content -->
        <div v-else>
          <!-- Métricas Principales -->
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            <!-- Total KG -->
            <MetricCard
              title="Total KG Producidos"
              :value="formatNumber(reactiveEstadisticas?.total_kg)"
              border-class="border-green-500"
              icon-bg-class="bg-green-100"
            >
              <template #icon>
                <svg class="w-6 h-6 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4"></path>
                </svg>
              </template>
            </MetricCard>

            <!-- Total Producciones -->
            <MetricCard
              title="Total Producciones"
              :value="reactiveEstadisticas?.total_producciones || 0"
              border-class="border-blue-500"
              icon-bg-class="bg-blue-100"
            >
              <template #icon>
                <svg class="w-6 h-6 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"></path>
                </svg>
              </template>
            </MetricCard>

            <!-- Máquinas Activas -->
            <MetricCard
              title="Máquinas Activas"
              :value="reactiveEstadisticas?.maquinas_activas || 0"
              border-class="border-purple-500"
              icon-bg-class="bg-purple-100"
            >
              <template #icon>
                <svg class="w-6 h-6 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"></path>
                </svg>
              </template>
            </MetricCard>

            <!-- OEE Promedio -->
            <MetricCard
              title="OEE Promedio"
              :value="formatOEE(reactiveEstadisticas?.oee_promedio)"
              border-class="border-yellow-500"
              icon-bg-class="bg-yellow-100"
            >
              <template #icon>
                <svg class="w-6 h-6 text-yellow-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6"></path>
                </svg>
              </template>
            </MetricCard>
          </div>

          <!-- Producción por Máquina -->
          <ProductionByMachine :produccion-por-maquina="reactiveProduccionPorMaquina" />

          <!-- Estados de Máquinas en Tiempo Real -->
          <MachineStates :estados-maquinas="reactiveEstadosMaquinas" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MetricCard from '../Components/MetricCard.vue';
import ProductionByMachine from '../Components/ProductionByMachine.vue';
import MachineStates from '../Components/MachineStates.vue';

export default {
  components: {
    MetricCard,
    ProductionByMachine,
    MachineStates
  },
  props: {
    estadisticas: {
      type: Object,
      default: () => ({})
    },
    produccionPorMaquina: {
      type: Array,
      default: () => []
    },
    estadosMaquinas: {
      type: Array,
      default: () => []
    },
  },

  data() {
    return {
      loading: false,
      error: null,
      ultimaActualizacion: new Date().toLocaleString('es-ES'),
      echoListeners: [],
      // Copias reactivas de las props
      reactiveEstadisticas: {},
      reactiveProduccionPorMaquina: [],
      reactiveEstadosMaquinas: []
    }
  },

  mounted() {
    console.log('Dashboard Vue mounted correctamente');
    console.log('Datos iniciales:', {
      estadisticas: this.estadisticas,
      produccionPorMaquina: this.produccionPorMaquina,
      estadosMaquinas: this.estadosMaquinas
    });

    // Copiar props a estado reactivo
    this.reactiveEstadisticas = { ...this.estadisticas };
    this.reactiveProduccionPorMaquina = [...this.produccionPorMaquina];
    this.reactiveEstadosMaquinas = [...this.estadosMaquinas];

    this.setupWebSocketListeners();
  },

  beforeUnmount() {
    this.cleanupWebSocketListeners();
  },

  methods: {
    setupWebSocketListeners() {
      if (typeof window.Echo === 'undefined') {
        console.warn('Echo no está disponible, omitiendo listeners de WebSocket');
        return;
      }

      try {
        // Listener para nuevas producciones
        const produccionListener = window.Echo.channel('produccion')
          .listen('.produccion.registrada', (e) => {
            console.log('Nueva producción registrada via WS:', e);
            this.actualizarDesdeEventoProduccion(e);
          });

        // Listener para cambios de estado de máquinas
        const maquinaListener = window.Echo.channel('maquina-estado')
          .listen('.maquina.estado.actualizado', (e) => {
            console.log('Estado de máquina actualizado via WS:', e);
            this.actualizarDesdeEventoMaquina(e);
          });

        this.echoListeners = [produccionListener, maquinaListener];
        console.log('WebSocket listeners configurados correctamente');
      } catch (error) {
        console.error('Error configurando WebSocket listeners:', error);
      }
    },

    cleanupWebSocketListeners() {
      this.echoListeners.forEach(listener => {
        if (listener && typeof listener.stopListening === 'function') {
          listener.stopListening();
        }
      });
      this.echoListeners = [];
    },

    async actualizarDatos() {
      try {
        this.loading = true;
        this.error = null;

        // Hacer petición AJAX para obtener datos actualizados
        const response = await fetch('/dashboard-data', {
          method: 'GET',
          headers: {
            'X-Requested-With': 'XMLHttpRequest',
            'Accept': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error('Error al obtener datos actualizados');
        }

        const data = await response.json();

        // Actualizar datos reactivamente
        this.reactiveEstadisticas = data.estadisticas || {};
        this.reactiveProduccionPorMaquina = data.produccionPorMaquina || [];
        this.reactiveEstadosMaquinas = data.estadosMaquinas || [];

        // Actualizar timestamp de última actualización
        this.ultimaActualizacion = new Date().toLocaleString('es-ES');

        console.log('Datos actualizados via AJAX:', data);

      } catch (err) {
        console.error('Error actualizando datos:', err);
        this.error = 'Error al actualizar los datos';
      } finally {
        this.loading = false;
      }
    },

    actualizarDesdeEventoProduccion(evento) {
      try {
        console.log('Actualizando desde evento producción:', evento);

        // Actualizar estadísticas globales
        if (evento.produccion) {
          // Incrementar total KG
          this.reactiveEstadisticas.total_kg = (this.reactiveEstadisticas.total_kg || 0) + (evento.produccion.cantidad_kg || 0);

          // Incrementar total producciones
          this.reactiveEstadisticas.total_producciones = (this.reactiveEstadisticas.total_producciones || 0) + 1;

          // Actualizar producción por máquina
          this.actualizarProduccionPorMaquina(evento.produccion);
        }

        // Actualizar timestamp
        this.ultimaActualizacion = new Date().toLocaleString('es-ES');

      } catch (error) {
        console.error('Error procesando evento producción:', error);
      }
    },

    actualizarDesdeEventoMaquina(evento) {
      try {
        console.log('Actualizando desde evento máquina:', evento);

        if (evento.maquina && evento.estado) {
          // Actualizar estado de la máquina específica
          this.actualizarEstadoMaquina(evento.maquina, evento.estado);

          // Recalcular estadísticas que dependen de estados
          this.recalcularEstadisticasMaquinas();
        }

        // Actualizar timestamp
        this.ultimaActualizacion = new Date().toLocaleString('es-ES');

      } catch (error) {
        console.error('Error procesando evento máquina:', error);
      }
    },

    actualizarProduccionPorMaquina(produccion) {
      const maquinaId = produccion.maquina_id;
      const cantidadKg = produccion.cantidad_kg || 0;

      // Buscar si ya existe la máquina en produccionPorMaquina
      let maquinaProd = this.reactiveProduccionPorMaquina.find(p => p.maquina?.id === maquinaId);

      if (maquinaProd) {
        // Actualizar existente
        maquinaProd.total_kg = (maquinaProd.total_kg || 0) + cantidadKg;
        maquinaProd.producciones = (maquinaProd.producciones || 0) + 1;
      } else {
        // Agregar nueva
        this.reactiveProduccionPorMaquina.push({
          maquina: { id: maquinaId, nombre: produccion.maquina?.nombre || `Máquina ${maquinaId}` },
          total_kg: cantidadKg,
          producciones: 1
        });
      }
    },

    actualizarEstadoMaquina(maquina, estado) {
      // Buscar la máquina en estadosMaquinas
      let maquinaEstado = this.reactiveEstadosMaquinas.find(e => e.maquina?.id === maquina.id);

      if (maquinaEstado) {
        // Actualizar existente
        Object.assign(maquinaEstado, {
          estado: estado.estado,
          oee_actual: estado.oee_actual,
          velocidad_actual: estado.velocidad_actual,
          kg_producidos: estado.kg_producidos,
          updated_at: estado.updated_at || new Date().toISOString()
        });
      } else {
        // Agregar nueva
        this.reactiveEstadosMaquinas.push({
          maquina: maquina,
          estado: estado.estado,
          oee_actual: estado.oee_actual,
          velocidad_actual: estado.velocidad_actual,
          kg_producidos: estado.kg_producidos,
          updated_at: estado.updated_at || new Date().toISOString()
        });
      }
    },

    recalcularEstadisticasMaquinas() {
      // Contar máquinas activas (operando)
      this.reactiveEstadisticas.maquinas_activas = this.reactiveEstadosMaquinas.filter(e =>
        e.estado === 'operando' || e.estado === 'Operando'
      ).length;

      // Calcular OEE promedio
      const maquinasConOEE = this.reactiveEstadosMaquinas.filter(e => e.oee_actual != null);
      if (maquinasConOEE.length > 0) {
        const sumaOEE = maquinasConOEE.reduce((sum, e) => sum + (e.oee_actual || 0), 0);
        this.reactiveEstadisticas.oee_promedio = sumaOEE / maquinasConOEE.length;
      } else {
        this.reactiveEstadisticas.oee_promedio = 0;
      }
    },

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