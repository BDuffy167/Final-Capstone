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
          <a href="#" class="btn btn-primary">Add A Reading Log</a>
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
      return this.$store.state.userBooks;
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
h1 {
  text-align: center;
}
.bookList {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}

.cardStyling{
  border-radius: 10px;
    width: 250px;
    height: 550px;
    margin: 20px;
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