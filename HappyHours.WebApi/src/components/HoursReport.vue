<template>
  <div class="hours-report-container">
    <app-hours-grid/>
    <div class="summary">

      <div>
        <label>Extra Monthly Time: </label>
        <span>{{ userInfo.ExtraMinutes | minutes }}</span>
      </div>

      <div>
        <label>Lack Monthly Time: </label>
        <span>{{ userInfo.LackMinutes | minutes }}</span>
      </div>

    </div>
  </div>
</template>

<script>

  import HoursGrid from './HoursGrid.vue';

  import { mapGetters } from 'vuex';

  export default {

    components: {
      appHoursGrid: HoursGrid
    },

    computed: {
      ...mapGetters([
        'userInfo'
      ])
    }

  }

</script>

<style scoped>

  .summary {
      margin-top: 40px;
      font-weight: bold;
      font-size: 18px;
  }

  label {
    width: 200px;
    display: inline-block;
    text-decoration: underline;
  }

</style>
