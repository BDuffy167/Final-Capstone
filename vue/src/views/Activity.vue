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
    
    <button v-if="!showForm" v-on:click.prevent="showForm = true">Record Activity</button>
  <form class="row g-3" v-show="showForm" v-on:submit.prevent="addToList">
  <div class="col-12">
    <label for="Title" class="form-label">Book Title</label>
    <input type="text" class="form-control" id="title" placeholder="Enter Book Title" v-model="newItem.">
  </div>
  <div class="col-md-6">
    <label for="Author" class="form-label">Author First</label>
    <input type="text" class="form-control" id="authorFirstName">
  </div>
  <div class="col-md-6">
    <label for="Author" class="form-label">Author Last</label>
    <input type="text" class="form-control" id="authorLastName">
  </div>
 

  <div class="col-md-4">
    <label for="timeRead" class="form-label">Session Time</label>
    <input type="number" class="form-control" id="timeRead" placeholder="minutes">
  </div>
  <div class="col-md-6">
    <label for="formatType" class="form-label">Book Type</label>
    <input type="text" class="form-control" id="formatType">
  </div>
  <div class="col-12">
    <div class="form-check">
      <input class="form-check-input" type="checkbox" id="gridCheck">
      <label class="form-check-label" for="gridCheck">
        Add To List
      </label>
    </div>
  </div>
  <div class="col-12">
    <button type="submit" class="btn btn-primary">Submit</button>
  </div>
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
    return {
      showForm: false,
      filter: {
        Title: "",
        Author: "",
        timeRead: "",
        formatType: ""
      },
      newItem: {
        Title: "",
        Author: "",
        timeRead: "",
        formatType: ""
      },
      activityLog: [
        Title
      ]
    };
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
  methods: {
    addToList(){
      let itemToAdd = this.newItem;
      this.
    }
  },
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