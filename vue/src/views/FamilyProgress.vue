<template>
  <main>
    <h1>Family Reading History</h1>
    <div class="card">
      <h5 class="card-header">The Family</h5>
      <div class="card-body">
        <h5 class="card-title">Select a family member to check</h5>

          <form>
          <select
            class="form-control" name="Select Child" 
            @change="onChange(this.value)"
            v-model="allChildren"
          
          >
            <option value="" selected disabled>Choose</option>
            <option v-for="child in allChildren" v-bind:key="child.userId" :value="child.userId"
            
            >{{child.username}}</option>
          </select>
          </form>
          
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
  methods: {
    onChang(kid) {
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
        this.$store.commit("SET_USER_HISTORY", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
    },
  },
};

</script>

<style>
</style>