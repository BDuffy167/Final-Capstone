<template>
  <main>
    <h1>Activity List</h1>
    <table class="readingActivityTable">
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
          <!-- firstname lastname concat -->
          <th>Time Read</th>
          <th>Format Type</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="activity of allActivity" v-bind:key="activity.logID">
          <td>{{ activity.loggedBook.title }}</td>
          <td>
            {{ activity.loggedBook.authorFirstName }}
            {{ activity.loggedBook.authorLastName }}
          </td>
          <td>{{ activity.timeRead }}</td>
          <td>{{ activity.formatType }}</td>
        </tr>
      </tbody>
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
    },
  },
  data() {
    return {};
  },
  created() {
    BookService.get(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_READINGLOG", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
  },
  methods: {},
};
</script>

<style scoped>
div {
  /* background-color: aqua; */
}
h1 {
  text-align: center;
}
.readingActivityTable  {
margin-left: auto;
margin-right: auto;
}
td, th {
  padding: .5em;
  text-align: center;
}
th {
  border: 2px solid black;
}
</style>