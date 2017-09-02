<template>
  <div class="app-container" v-if="!fetchUserInfo">
    <app-header/>
    <div class="layout">
      <router-view/>
    </div>
    <app-footer/>
  </div>
</template>

<script>

  import Header from './components/Header.vue';
  import Footer from './components/Footer.vue';

  import storageManager from './storageManager';

  import { mapGetters, mapActions } from 'vuex';

  export default {

    components: {
      appHeader: Header,
      appFooter: Footer
    },

    computed: {

      ...mapGetters([
        'fetchUserInfo'
      ])

    },

    methods: {

      ...mapActions([
        'setLogged',
        'fetchUserDetails'
      ])

    },

    created() {

      if (storageManager.getTokenBearer()) {
        this.fetchUserDetails({startup: true});
        //this.setLogged(true);
      }

    }

  }

</script>

<style scoped>

  @import 'style/style.css';
  @import 'style/forms.css';
  @import 'style/buttons.css';

  .app-container {
    background: #E9EBEE;
  }

  .layout {
    width: 70%;
    margin: auto;
    min-height: 600px;
    margin-top: 50px;
    margin-bottom: 50px;
  }

</style>
