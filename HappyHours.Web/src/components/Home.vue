<template>
  <div class="container">
    <!--<div class="signin-btn">ENTER</div>-->
    <span class="header-bold">Integrated to the Time Attendance system</span>
    <p>
      Signin with the Time Attendance system credentials, and be able to track your work time<br>
      without having to do any manual calculations. The system automatically fetch your personal<br>
      information and do all the calculations for you.
    </p>

    <span class="header-bold">Receive automatic emails to your personal inbox</span>
    <p>
      Every time enter/leave the office, you'll receive a verification email after the data<br>
      has been successfully updated in the Time Attendance system. You won't need to worry anymore<br>
      if you forgot puting your finger on the finger's reader.
    </p>

    <span class="header-bold">Smart reminder</span>
    <p>
      Forgot to put your finger after you left the office yesterday? The system<br>
      will automatically detect it, and will send you an email so that you'll be able<br>
      to manually set your exit time from yesterday.
    </p>

    <span class="header-bold">See who is in the office!</span>
    <p>
      The system monitors who is entering/leaving the office, so you'll now be able to<br>
      know if someone arrived this morning, or left this evening without having to look<br>
      for him.
    </p>

    <button class="create-account-btn">Create Account</button>

  </div>
</template>

<script>

  export default {

  }

</script>

<style scoped>

  .create-account-btn {
    width: 170px;
    height: 34px;
    background-color: #68c92c;
    color: white;
    font-size: 14px;
    font-weight: bold;
    padding: 0;
    border: 1px solid #327c04;
    border-radius: 4px;
    cursor: pointer;
    -webkit-transition: background-color 140ms linear;
  }

  .create-account-btn:hover {
    background-color: #81d34e;
  }

  .container {
    width: 40%;
    margin: auto;
    margin-top: 100px;
  }

  .header-bold {
    font-weight: bold;
    font-size: 17px;
  }

  p {
    color: #929390;
    font-size: 13px;
  }


</style>
