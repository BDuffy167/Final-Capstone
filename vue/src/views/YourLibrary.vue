<template>
    <main> 
        <h1>BOOKS ARE COOL KIDS!!!!!!!!!!!!!!!!!!!</h1>
        <table class="bookTable">
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Author</th>
                    <th>ISBN</th> <!-- maybe? -->
                    <th>Minutes Read</th>
                    <th>Finished?</th>
                </tr>
            </thead>
        </table>
    </main>
</template>

<script>
// import BookService from "../services/BookService.js";

export default {
    
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
td, th {
  padding: .5em;
  text-align: center;
}
th {
  border: 2px solid black;
}
</style>