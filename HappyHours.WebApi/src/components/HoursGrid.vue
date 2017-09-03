<template>
  <div class="grid-container">
    <table border="1" class="hours-table" cellpadding="5">
      <tr class="headers-row">
        <th>Date</th>
        <th>Day</th>
        <th>Entrance Time</th>
        <th>Exit Time</th>
        <th>Extra Time</th>
        <th>Lack Time</th>
      </tr>
      <tr v-for="day in userInfo.Days">
        <td>{{ day.Date }}</td>
        <td>{{ day.Day }}</td>
        <td>{{ day.StartTime }}</td>
        <td>{{ day.EndTime }}</td>
        <td>{{ day.ExtraMinutes | minutes }}</td>
        <td>{{ day.LackMinutes | minutes }}</td>
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

  .hours-table {
    border-collapse: collapse;
    min-width: 700px;
  }

  .headers-row {
    text-align: left;
    background: #3B5998;
    color: white;
  }

  tr {
    cursor: pointer;
  }

  tr:hover {
    background: #3B5998;
    color: white;
  }

</style>
