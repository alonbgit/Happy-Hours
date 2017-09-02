<template>
  <div class="hours-report-container">

    <div>
      <app-hours-grid class="hours-grid"/>
      <app-dropdown :data="dropdownData" :selectedIndex="dropdownData.length - 1" class="dropdown" @changed="onMonthChange"/>
    </div>

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
  import Dropdown from './Dropdown.vue';

  import { mapGetters } from 'vuex';

  export default {

    components: {
      appHoursGrid: HoursGrid,
      appDropdown: Dropdown
    },

    data() {

      let dropdownData = this.getDropdownData();

      return {
        dropdownData: dropdownData
      };
    },

    methods: {

      onMonthChange(item) {
        debugger;
      },

      getDropdownData() {
        let months = this.$store.getters.userInfo.Months;
        let obj = [];
        months.map((m) => {
          obj.push({
            id: m.Month,
            name: m.Name
          });
        });
        return obj;
      }

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
      clear: both;
      float: left;
  }

  label {
    width: 200px;
    display: inline-block;
    text-decoration: underline;
  }

  .hours-grid {
    float: left;
  }

  .dropdown {
    float: left;
    margin-left: 100px;
  }

</style>
