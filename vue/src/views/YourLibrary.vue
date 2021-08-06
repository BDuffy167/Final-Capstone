<template>
  <main>
    <h1>The Family Library</h1>
    <table class="table table-hover">
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
          <td>{{ book.title }}</td>
          <td>
            {{ book.authorFirstName }}
            {{ book.authorLastName }}
          </td>
          <td>{{ book.isbn }}</td>
          <td></td>
          <td><input type="checkbox" /></td>
        </tr>
      </tbody>
    </table>

    <button v-if="!isAddBookVisible" v-on:click.prevent="isAddBookVisible = true">Add a Book</button>

    <form id="addBookForm"
        class="row g-3"
        v-show="isAddBookVisible" 
        v-on:submit.prevent="addABook">
      <div class="col-12">
        <label for="inputTitle" class="form-label">Title</label>
        <input type="text" class="form-control" id="inputTitle" v-model="newBook.title">
      </div>
      <div class="col-md-4">
        <label for="inputAuthorFirstName" class="form-label">Author's First Name</label>
        <input type="text" class="form-control" id="inputAuthorFirstName" v-model="newBook.authorFirstName">
      </div>
      <div class="col-md-4">
        <label for="inputAuthorLastName" class="form-label">Author's Last Name</label>
        <input type="text" class="form-control" id="inputAuthorLastName" v-model="newBook.authorLastName">
      </div>
      <div class="col-md-4">
        <label for="inputISBN" class="form-label">ISBN</label>
        <input type="text" class="form-control" id="inputISBN" v-model.number="newBook.isbn">
      </div>
      <input type="submit" value="Save" class="col-md-1" v-on:click ="isAddBookVisible = false"> 
      <input type="button" value="Cancel" class="col-md-1" v-on:click="isAddBookVisible = false">
    </form>

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
      return this.$store.state.books;
    }
  },
  data() {
      return {
        isAddBookVisible: false,
        newBook: {
          bookId: null,
          title: "",
          authorFirstName: "",
          authorLastName: "",
          isbn: null
        }

      };
  },
  created() {
    BookService.getFamilyBooks(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_FAMILY_BOOKS", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
  },
  methods: {
    addABook() {
      BookService.post(this.$store.state.user.userId, this.newBook)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_FAMILY_BOOKS", response.data);
        })
        .catch((response) => {
        console.error(response);
      });
    }
  }
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