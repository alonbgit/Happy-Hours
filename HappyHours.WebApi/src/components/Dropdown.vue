<template>
  <div class="container" @mouseover="onMouseover" @mouseout="onMouseout">
    <div class="item">{{ selectedItem.name }}</div>
    <div class="dropdown-container" v-show="showMenu == true">
      <template v-for="item in items">
        <div @click="onItemClick(item)" class="sub-item">{{ item.name }}</div>
      </template>
    </div>
  </div>
</template>

<script>

  export default {

    props: {
      data: {
        type: Array
      },
      selectedIndex: {
        type: Number,
        default: 0
      }
    },

    data() {

      let items = [];
      this.data.map((d) => {
        items.push({
          name: d.name,
          id: d.id
        })
      });

      let selectedItem = items[this.selectedIndex];

      return {
        items: items,
        selectedItem: selectedItem,
        showMenu: false
      };

    },

    methods: {

      onItemClick(item) {
        this.selectedItem = item;
        this.showMenu = false;
        this.$emit('changed', item);
      },

      onMouseover() {
        if (!this.showMenu)
          this.showMenu = true;
      },

      onMouseout() {
        if (this.showMenu)
          this.showMenu = false;
      }

    }

  }

</script>

<style scoped>

  .container {
    font-family: Arial;
    width: 150px;
    height: 30px;
  }

  .item {
    border: 1px solid black;
    height: 28px;
    line-height: 28px;
    padding-left: 5px;
    cursor: pointer;
    background: white;
    border-radius: 2px;
  }

  .sub-item {
    height: 28px;
    line-height: 28px;
    cursor: pointer;
    color: #919396;
    padding-left: 8px;
    border-top: 1px solid black;
  }

  .sub-item:first-child {
    border-top: 0px;
  }

  .sub-item:hover {
    background: #a0bdf7;
    color: white;
  }

  .dropdown-container {
    border: 1px solid black;
    border-top: 0px;
  }

</style>
