<template>
  <main>
    <h1>Family Reading History</h1>
    <div class="card">
      <h5 class="card-header">The Family</h5>
      <div class="card-body">
        <h5 class="card-title">Select a family member to check</h5>
        <div>
          <select
            class="form-select form-select-lg mb-3"
            aria-label=".form-select-lg example"
            v-for="child in userChildren" v-bind:key="child.userId"
          >
            <option selected>Select Child</option>
            <div>
                <option>{{child.username}}</option>
            </div>
            <option value="2">Two</option>
            <option value="3">Three</option>
          </select>
        </div>

        <a href="#" class="btn btn-primary">Go somewhere</a>
      </div>
    </div>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "familyProgress",
  computed: {
    allChildren() {
      return this.$store.state.userChildren;
    },
  },
  data() {
    return {
      familyChild: {
        userId: 0,
        username: "string",
        role: "string",
        familyId: 0,
        testProp: 0,
      },
    };
  },
  created() {
    BookService.getFamilyChildren(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_FAMILY_CHILDREN", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
  },
};
</script>

<style>
</style>