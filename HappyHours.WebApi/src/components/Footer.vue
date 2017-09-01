<template>
  <div class="footer">

    <div class="inner-footer">

      <ul class="footer-list">
        <router-link to="/Signup" tag="li" v-if="!logged">Sign Up</router-link>
        <router-link to="/Signin" tag="li" v-if="!logged">Sign In</router-link>
        <router-link to="/" tag="li">About</router-link>
        <li @click="openTA">Time Attendance</li>
      </ul>

      <div class="copy-right">HappyHours © 2017</div>

    </div>

  </div>
</template>

<script>

  import { mapGetters } from 'vuex';

  export default {

    computed: {
      ...mapGetters([
        'logged'
      ])
    },

    methods: {

      openTA() {
        window.open('https://rt-pay.com/WebRP/');
      }

    }

  }

</script>

<style>

  .footer {
    height: 200px;
    width: 100%;
    background: white;
    position: absolute;
    bottom: 0;
    left: 0;
  }

  .inner-footer {
    width: 50%;
    margin: auto;
  }

  .footer-list {
    list-style-type: none;
    font-size: 13px;
    color: #3B5998;
    margin: 0;
    padding: 0;
    margin-top: 10px;
  }

  .footer-list > li {
    display: inline-block;
    margin-left: 30px;
    cursor: pointer;
  }

  .footer-list > li:first-child {
    margin-left: 0px;
  }

  .copy-right {
    margin-top: 30px;
    font-size: 13px;
    color: #8e8e8e;
  }

</style>
