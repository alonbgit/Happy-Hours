
<template>
  <div>
    <div class="form-container">

      <div class="form-header">
        <span>Activation completed</span>
      </div>

      <div class="form-content">

        <div class="form-item">
          <p>The activation was completed successfully</p>
        </div>

        <div class="form-item">
          <button class="btn" @click="onSigninClick">Sign in</button>
        </div>

      </div>

    </div>

  </div>
</template>

<script>

  export default {

    methods: {

      onSigninClick() {
        this.$router.push('/Signin');
      }

    }

  }

</script>

<style scoped>

</style>
