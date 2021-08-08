<template>
  <main>
    <h1>Personal Library</h1>
    <div class="bookList">
      <div
        class="card cardStyling"
        style="width: 18rem"
        v-for="book of allBooks"
        v-bind:key="book.logID"
      >
        <img
          v-if="book.isbn"
          v-bind:src="
            'http://covers.openlibrary.org/b/isbn/' + book.isbn + '-M.jpg'
          "
        />
        <div class="card-body">
          <h5 class="card-title">{{ book.title }}</h5>
          <p class="card-text">
            {{ book.authorFirstName }} {{ book.authorLastName }}
          </p>
          <a href="#" class="btn btn-primary">Add to library</a>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";


export default {
  name: "personalHistory",
  components: {
    
  },
  computed: {
    allBooks() {
      return this.$store.state.books;
  },
  },
  data() {
    return {

    }
  },
  created() {
    BookService.getPersonalLibrary(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_PERSONAL_LIBRARY", response.data);
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