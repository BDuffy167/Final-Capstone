<template>
  <main>
    <h1>Book List</h1>
    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th><!-- firstname lastname concat -->
          <th>Time Read</th>
          <th>Format Type</th>
        </tr>
      </thead>
     <!-- <tbody>-->
        <!--<tr v-for="book of Books" v-bind:key="book.loggedbook.bookId"></tr>v-for, loop,tr for each-->
       <!-- <td>{{book.loggedbook.title}}</td>-->
       <!-- <td>{{book.loggedbook.authorFirstName}} {{book.loggedbook.authorlastName}}</td>-->
        <!--<td>{{book.loggedbook.timeRead}}</td>-->
       <!-- <td>{{formatType}}</td>-->
     <!-- </tbody>-->

    </table>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";
//import BookList from "../components/BookList.vue";

export default {
  name: "books",
  components: {
   // BookList
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