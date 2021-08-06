<template>
  <main>
    <h1>Activity List</h1>
    <div class="accordion">
    <h2>Your Book List</h2>
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>Total Time</th>
          <th>Completed</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book of allMyBooks" v-bind:key="book.bookId">
          <td>{{ book.title }}</td>
          <td>
            {{ book.authorFirstName }}
            {{ book.authorLastName }}
          </td>
          <td>{{ book.totalTime }}</td>
          <td>{{ book.isCompleted }}</td>
        </tr>
      </tbody>
    </table>

    <h2>Your Reading History</h2>
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>ISBN</th>
          <!-- firstname lastname concat -->
          <th>Time Read</th>
          <th>Format Type</th>
          <th>Notes</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="activity of allActivity" v-bind:key="activity.logID" >
          <td>{{ activity.loggedBook.title }}</td>
          <td>
            {{ activity.loggedBook.authorFirstName }}
            {{ activity.loggedBook.authorLastName }}
          </td>
          <td>{{ activity.loggedBook.isbn }}</td>
          <td>{{ activity.timeRead }}</td>
          <td>{{ activity.formatType }}</td>
          <td>{{activity.note}}</td>
        </tr>
        <tr>
          
        </tr>
      </tbody>
    </table>
    </div>

    <button v-if="!showForm" v-on:click.prevent="showForm = true">
      Record Activity
    </button>
    <form class="row g-3" v-show="showForm" v-on:submit.prevent="addALog">
      <div class="col-12">
        <label for="Title" class="form-label">Book Title</label>
        <input
          type="text"
          class="form-control"
          id="title"
          placeholder="Enter Book Title"
          v-model="newLog.loggedBook.title"
        />
      </div>
      <div class="col-md-6">
        <label for="Author" class="form-label">Author First</label>
        <input
          type="text"
          class="form-control"
          id="authorFirstName"
          v-model="newLog.loggedBook.authorFirstName"
        />
      </div>
      <div class="col-md-6">
        <label for="Author" class="form-label">Author Last</label>
        <input
          type="text"
          class="form-control"
          id="authorLastName"
          v-model="newLog.loggedBook.authorLastName"
        />
      </div>
      <div class="col-md-4">
        <label for="isbn" class="form-label">ISBN</label>
        <input
          type="number"
          class="form-control"
          id="isbn"
          v-model.number="newLog.loggedBook.isbn"
        />
      </div>
      <div class="col-md-4">
        <label for="timeRead" class="form-label">Session Time</label>
        <input
          type="number"
          class="form-control"
          id="timeRead"
          placeholder="minutes"
          v-model.number="newLog.timeRead"
        />
      </div>
      <div class="col-md-4">
        <label for="formatType" class="form-label">Format</label>
        <select
          class="form-control"
          id="formatSelect"
          v-model="newLog.formatType"
        >
          <option>Paperback</option>
          <option>Ebook</option>
          <option>Audiobook</option>
          <option>Read-Aloud (Reader)</option>
          <option>Read-Aloud (Listener)</option>
          <option>Other</option>
        </select>
      </div>
      <div class="input-group">
        <span class="input-group-text">Notes</span>
        <textarea class="form-control" maxlength="250" aria-label="With textarea" placeholder="Reading log note"
        v-model="newLog.note"></textarea>
      </div>
      <div class="col-12">
        <div class="form-check">
          <input class="form-check-input" type="checkbox" id="gridCheck" />
          <label class="form-check-label" for="gridCheck"> Add To List </label>
        </div>
      </div>
      <div class="col-12">
        <input
          type="submit"
          value="Save"
          class="col-md-1"
          v-on:click="showForm = false"
        />
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
    allMyBooks(){
      return this.$store.state.userHistory;
    }
  },
  data() {
    return {
      showForm: false,
      filter: {
        title: "",
        author: "",
        timeRead: "",
        formatType: "",
      },
      myBooks: {
        bookId: 0,
        title: "",
        authorFirstName: "",
        authorLastName: "",
        totalTime: 0,
        isCompleted: ""
      },
      newLog: {
        logID: 0,
        readerId: null,
        loggedBook: {
          bookId: 0,
          title: "",
          authorFirstName: "",
          authorLastName: "",
          isbn: 0,
        },
        formatType: "",
        timeRead: "",
        notes: "",
      },
      activityLog: [],
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
      BookService.getUserHistory(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_USER_HISTORY", response.data)
      })
      .catch((response) => {
        console.error(response);
      });
  },
  methods: {
    addALog() {
      BookService.postLog(this.$store.state.user.userId, this.newLog)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_READINGLOG", response.data);
        })
        .catch((respone) => {
          console.error(respone);
        });
    },
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

</style>