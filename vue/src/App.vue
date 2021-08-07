<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" class="position-absolute top-0 start-0">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <div
      class="d-flex flex-column flex-shrink-0 p-3 text-white"
      style="width: 280px"
      id = "sidebar"
    >
      <a
        href="/"
        class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <svg class="bi me-2" width="40" height="32">
          <use xlink:href="#bootstrap"></use>
        </svg>
        <span class="fs-4">Immerse</span>
      </a>
      <hr />
      <div id="nav">
        <ul class="nav nav-pills flex-column mb-auto">
          <li class="nav-item">
            <router-link class="nav-item" v-bind:to="{ name: 'home' }">
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#home"></use>
              </svg>
              <i class="fas fa-home"></i>
              <!-- This is a font awesome icon -->
              Home
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'yourLibrary' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#speedometer2"></use>
              </svg>
              &nbsp;|&nbsp;Family Library
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'personalHistory' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#speedometer2"></use>
              </svg>
              &nbsp;|&nbsp;Reading History
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'register' }"
              v-if="!$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;|&nbsp;Register
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'login' }"
              v-if="!$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;|&nbsp;Login
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'activity' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;|&nbsp;Activity
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'logout' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;|&nbsp;Logout
            </router-link>
          </li>
        </ul>
        <hr />
        
      </div>
    </div>
    <router-view id="routerView"/> 
  </div>
</template>

<script>
import Activity from "./views/Activity.vue";
//import BookList from './components/BookList.vue';
export default {
  name: "app",
  component: {
    Activity,
    //BookList,
  },
};
</script>

<!-- Application-Wide Styles go here.  -->
<style>
.nav-item {
  text-decoration: none;
  color:beige;
}

#app {
  display: flex;
  justify-content: flex-start;
  flex-wrap: wrap;
  height: 100vh;
  width: 100vw;
  max-height: 100vh;
  overflow-x: auto;
  overflow-y: auto;
  background-color: #d7d7db;
  font-family: 'Atkinson Hyperlegible', sans-serif;
  
}

#sidebar{
  display: flex;
  align-items: space-around;
  justify-content: stretch;
  margin-right: 20px;
  background-color: #0fa5db;
}

#routerView{

  
}

</style>
