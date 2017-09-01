
<template>

  <div class="header">

    <div class="inner-header">

      <router-link to="/">
        <img src="../../images/logo-small.png" class="logo"/>
      </router-link>

      <ul>
        <router-link to="/Signup" tag="li" v-if="!logged">
          <span>Sign Up</span>
        </router-link>
        <router-link to="/Signin" tag="li" v-if="!logged">
          <span>Sign In</span>
        </router-link>
        <li @click="logout" v-if="logged">
          <span>Logout</span>
        </li>
      </ul>

      <div class="user-info" v-if="logged">
        Hello {{ fullName }}
      </div>

    </div>

  </div>

</template>

<script>

  import { mapGetters, mapActions } from 'vuex';

  export default {

    computed: {
      ...mapGetters([
        'logged',
        'fullName'
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
    background-color: #3B5998;
    border-bottom: 1px solid #367a0c;
    line-height: 75px;
    color: white;
  }

  .inner-header {
    width: 70%;
    margin: auto;
  }

  .user-info {
    float: left;
    margin-left: 30px;
    font-weight: bold;
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
    background-color: #6179a5;
    opacity: 0.7;
    cursor: pointer;
  }

</style>
