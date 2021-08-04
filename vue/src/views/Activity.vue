<template>
  <main>
    <h1>Activity List</h1>
    <table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th><!-- firstname lastname concat -->
          <th>Time Read</th>
          <th>Format Type</th>
        </tr>
      </thead>
     <!-- <tbody>
        <tr v-for="activity of allActivity" v-bind:key="activity.loggedbook.title"></tr>v-for, loop,tr for each
        <td>{{book.loggedbook.title}}</td>
       <td>{{book.loggedbook.authorFirstName}} {{book.loggedbook.authorlastName}}</td>
        <td>{{book.loggedbook.timeRead}}</td>
       <td>{{formatType}}</td>
     </tbody>-->

    </table>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";
//import BookList from "../components/BookList.vue";

export default {
  name: "activity",
  components: {
   // BookList
  },
  computed: {
    allActivity() {
      return this.$store.state.readingLog;
    }
  },
  data() {
    return {};
  },
  created() {
    BookService
      .get(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit('SET_READINGLOG', response.data);
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