<template>
  <div class="bg-white rounded-xl shadow-lg p-6 border-l-4" :class="borderClass">
    <div class="flex items-center">
      <div class="flex-shrink-0">
        <div class="w-12 h-12 rounded-lg flex items-center justify-center" :class="iconBgClass">
          <slot name="icon"></slot>
        </div>
      </div>
      <div class="ml-4">
        <p class="text-sm font-medium text-gray-600">{{ title }}</p>
        <p class="text-2xl font-bold text-gray-900">{{ value }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      required: true
    },
    value: {
      type: [String, Number],
      required: true
    },
    borderClass: {
      type: String,
      default: 'border-gray-500'
    },
    iconBgClass: {
      type: String,
      default: 'bg-gray-100'
    }
  }
};
</script>