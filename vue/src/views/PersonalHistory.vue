<template>
  <main>
    <h1>Personal Library</h1>
    <book-list></book-list>
  </main>
</template>

<script>
//import BookService from "../services/BookService.js";
import BookList from "../components/BookList.vue"

export default {
  name: "personalHistory",
  components: {
    BookList
  },
};
</script>

<style>
</style>