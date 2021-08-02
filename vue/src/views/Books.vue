<template>
  <main>
      <h1>Book List</h1>
  </main>
</template>

<script>
import bookservice from '../services/BookService.js';

export default {
    created() {
        bookservice.GetAllBooks()
        .then(response => {
            console.log(response);
        })
        .catch(response => {
            console.error(response);
        })
    }

}
</script>

<style>

</style>