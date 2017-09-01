<template>
  <div class="container">
    <app-header/>
    <router-view/>
    <app-footer/>
  </div>
</template>

<script>

  import Header from './components/Header.vue';
  import Footer from './components/Footer.vue';

  import storageManager from './storageManager';

  import { mapActions } from 'vuex';

  export default {

    components: {
      appHeader: Header,
      appFooter: Footer
    },

    methods: {

      ...mapActions([
        'setLogged'
      ])

    },

    created() {

      if (storageManager.getTokenBearer()) {
        this.setLogged(true);
      }

    }

  }

</script>

<style scoped>
  @import 'style/style.css';
  @import 'style/forms.css';
  @import 'style/buttons.css';

  .container {
    height: 100%;
  }
</style>
