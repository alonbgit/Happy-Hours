<template>
  <div class="hours-report-container">
    <app-hours-grid/>
  </div>
</template>

<script>

  import HoursGrid from './HoursGrid.vue';

  export default {

    components: {
      appHoursGrid: HoursGrid
    }

  }

</script>

<style scoped>

  .hours-report-container {

  }

</style>
