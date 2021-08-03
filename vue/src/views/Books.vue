<template>
  <main>
    <h1>Book List</h1>
  </main>
</template>

<script>
import bookservice from "../services/BookService.js";

export default {
  name: "books",
  data() {
    return {};
  },
    created() {
      bookservice
        .get(this.$store.state.user.userId)
        .then((response) => {
          console.log(response);
        })
        .catch((response) => {
          console.error(response);
        });
    },
  methods: {
  },
};
</script>

<style>
</style>