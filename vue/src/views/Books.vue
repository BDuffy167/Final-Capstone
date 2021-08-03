<template>
  <main>
    <h1>Book List</h1>
    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th> <!-- firstname lastname concat -->
          <th>Minutes Read</th>
          <th>Finished?</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for=
      </tbody>
    </table>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";
import Book from '../components/Book.vue';

export default {
  name: "books",
  components: {
    Book
  },
  data() {
    return {};
  },
  created() {
    BookService
      .get(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
      })
      .catch((response) => {
        console.error(response);
      });
  },
  methods: {},
};
</script>

<style>
</style>