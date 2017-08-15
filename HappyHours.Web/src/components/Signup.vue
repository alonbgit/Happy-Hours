
<template>
  <div>
    <div class="form-container">

      <div class="form-header">
        <span>Signup</span>
      </div>

      <div class="form-content">

        <div v-show="currentStep == 1">
          <div class="form-item">
            <label>Email</label>
            <input type="text" v-model="user.email"/>
          </div>
          <div class="form-item">
            <label>Password</label>
            <input type="password" v-model="user.password"/>
          </div>
          <div class="form-item">
            <label>Confirm Password</label>
            <input type="password" v-model="user.confirmPassword"/>
          </div>
        </div>

        <div v-show="currentStep == 2">
          <div class="form-item">
            <label>TA Email</label>
            <input type="text" v-model="user.systemEmail"/>
          </div>
          <div class="form-item">
            <label>TA Password</label>
            <input type="password" v-model="user.systemPassword"/>
          </div>
          <div class="form-item">
            <label>TA Employee Number</label>
            <input type="text" v-model="user.systemNumber"/>
          </div>
        </div>

        <div class="buttons-container">
          <button class="btn" @click.prevent="onPrevStep" v-show="currentStep == 2">Previous</button>
          <button class="btn" @click.prevent="onNextStep">Next</button>
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
          password: '',
          confirmPassword: '',
          systemEmail: '',
          systemPassword: '',
          systemNumber: ''
        },
        currentStep: 1
      }
    },

    methods: {

      onNextStep() {
        this.currentStep++;
      },

      onPrevStep() {
        this.currentStep--;
      }

    }

  }

</script>

<style scoped>

  .buttons-container {
    float: right;
    margin-top: 20px;
    margin-right: 70px;
  }

  .next-btn {

  }

</style>
