<template>
  <div class="grid-container">
    <table>
      <tr>
        <th>Date</th>
        <th>Day</th>
        <th>Entrance Time</th>
        <th>Exit Time</th>
      </tr>
      <tr v-for="day in userInfo.Days">
        <td>{{ day.Date }}</td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
    </table>
  </div>
</template>

<script>

  import { mapGetters } from 'vuex';

  export default {

    computed: {

      ...mapGetters([
        'userInfo'
      ])

    }

  }

</script>

<style scoped>

</style>
