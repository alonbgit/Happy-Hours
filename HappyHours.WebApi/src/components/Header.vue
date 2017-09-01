
<template>

  <div class="header">
    
    <router-link to="/">
      <img src="../../images/logo-small.png" class="logo"/>
    </router-link>

    <ul>
      <router-link to="/Signup" tag="li" v-if="!logged">
        <span>Signup</span>
      </router-link>
      <router-link to="/Signin" tag="li" v-if="!logged">
        <span>Signin</span>
      </router-link>
      <li @click="logout" v-if="logged">
        <span>Logout</span>
      </li>
    </ul>

  </div>

</template>

<script>

  import { mapGetters, mapActions } from 'vuex';

  export default {

    computed: {
      ...mapGetters([
        'logged'
      ])
    },

    methods: {

      ...mapActions([
        'logout'
      ])

    }

  }

</script>

<style scoped>

  .header {
    height: 75px;
    width: 100%;
    margin: 0px;
    background-color: #70c93c;
    border-bottom: 1px solid #367a0c;
    line-height: 75px;
  }

  .logo {
    margin-top: 3px;
    margin-left: 20px;
    float: left;
  }

  ul {
    list-style-type: none;
    float: left;
    margin: 0;
    padding: 0;
    margin-left: 40px;
  }

  ul li {
    float: left;
    width: 130px;
    text-align: center;
    font-weight: bold;
    border-right: 1px solid #367a0c;
    -webkit-transition: background-color 140ms linear;
  }

  ul li:first-child {
    border-left: 1px solid #367a0c;
  }

  ul li:hover {
    background-color: #b3f28c;
    color: #41443e;
    opacity: 0.7;
    cursor: pointer;
  }

</style>
