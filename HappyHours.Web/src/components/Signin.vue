
<template>
  <div>
    <div class="form-container">

      <div class="form-header">
        <span>Signin</span>
      </div>

      <div class="form-content">

        <div class="form-item">
          <label>Email</label>
          <input type="text" v-model="user.email"/>
        </div>
        <div class="form-item">
          <label>Password</label>
          <input type="password" v-model="user.password"/>
        </div>

        <router-link to="/forgotpassword" class="forgot-password">Forgot password?</router-link>
        <div class="buttons-container">
          <button class="btn" @click.prevent="signin">Signin</button>
        </div>

      </div>
    </div>

  </div>
</template>

<script>

  export default {

    data() {
      return {
        user: {
          email: '',
          password: ''
        }
      }
    },

    methods: {

      signin() {
      }

    }

  }

</script>

<style scoped>

  .forgot-password {
    float: right;
    margin-right: 70px;
    margin-top: 5px;
    font-size: 13px;
  }

  .buttons-container {
    float: right;
    margin-top: 20px;
    margin-right: 70px;
    clear: both;
  }


</style>
