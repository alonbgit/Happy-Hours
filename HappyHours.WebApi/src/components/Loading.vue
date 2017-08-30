<template>
  <div class="loading-container">
    <span class="loading">Loading...</span><span class="loading-img"></span>
  </div>
</template>

<script>

  export default {

    props: ['isLoading']

  }

</script>

<style scoped>

  .loading-container {
    float: right;
    clear: both;
    font-size: 14px;
    margin-top: 8px;
    pointer-events:all;
  }

  .loading {
   margin-right: 10px;
  }

  .loading-img {
   background: url('/images/ajax-loader.gif');
   width: 16px;
   height: 16px;
   display: inline-block;
  }

</style>
