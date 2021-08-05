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
    <button>Record Activity</button>
    <form>
      <div class="field">
        <label for="title">Title</label>
        <input type="text" name="Title" id="bookTitle"/>
      </div>
        <br>
      <div class="field">
        <label for="author">Author</label>
        <input type="text" name="First NAme" id="authorFirstName"/>
        <input type="text" name="Last Name" id="authorLastName"/>
      </div>
        <br>
      <div class="field">
        <label for="timeRead">Time Read</label>
        <input type="number" name="Minutes Spent Reading" id="timeRead"/>
      </div>
        <br>
      <div class="fiels">
        <label for="formatType">Format Type</label>
        <input type="text" name="Format" id="formatType"/>
      </div>
      <br>
      <button>Submit</button>
    </form>
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
.readingActivityTable {
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