<template>
  <main>
    <h1>BOOKS ARE COOL KIDS!!!!!!!!!!!!!!!!!!!</h1>
    <table class="bookTable">
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>ISBN</th>
          <!-- maybe? -->
          <th>Minutes Read</th>
          <th>Completed</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book of allBooks" v-bind:key="book.logID">
          <td>{{ book.loggedBook.title }}</td>
          <td>
            {{ book.loggedBook.authorFirstName }}
            {{ book.loggedBook.authorLastName }}
          </td>
          <td>{{ book.loggedBook.ISBN }}</td>
          <td>{{ book.timeRead }}</td>
          <td><input type="checkbox" /></td>
        </tr>
      </tbody>
    </table>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "yourLibrary",
  components: {
    // BookList
  },
  computed: {
    allBooks() {
      return this.$store.state.book;
    }
  },
  data() {
      return {};
  },
  created() {
    BookService.get(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_BOOK", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
  },
}
</script>

<style scoped>
h1 {
  text-align: center;
}
.bookTable {
  margin-left: auto;
  margin-right: auto;
}
td,
th {
  padding: 0.5em;
  text-align: center;
}
th {
  border: 2px solid black;
}
</style>