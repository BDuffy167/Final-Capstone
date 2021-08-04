<template>
  <div>
      <h1>{{ this.title }}</h1>
  </div>
</template>

<script>
export default {
    name: 'BookList',
    data(){
       return {
           title: "All Books"
       }
    }
}
</script>

<style>
    h1 {
        font-weight: 300;
    }
</style>