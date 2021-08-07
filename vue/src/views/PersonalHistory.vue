<template>
  <main>
    <h1>Personal Library</h1>
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Title</th>
          <th>Author</th>
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
          <td></td>
          <td><input type="checkbox" /></td>
        </tr>
      </tbody>
    </table>
  </main>
</template>

<script>
// import BookService from "../services/BookService.js";

export default {
  name: "personalHistory",
  components: {},
};
</script>

<style>
</style>