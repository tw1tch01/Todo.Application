<template>
  <div id="app">
    <md-app>
      <md-app-toolbar class="md-primary">
        <span class="md-title">Items</span>
        <router-link to="add" class="md-button md-icon-button">
          <md-icon>add_circle</md-icon>
        </router-link>
      </md-app-toolbar>

      <md-app-drawer md-permanent="full">
        <NavigationSideMenu></NavigationSideMenu>
      </md-app-drawer>

      <md-app-content>
        <router-view />
      </md-app-content>
    </md-app>
  </div>
</template>

<script>
import NavigationSideMenu from "@/views/NavigationSideMenu.vue";

export default {
  name: "App",
  components: {
    NavigationSideMenu
  }
};
</script>

<style lang="scss">
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}

.md-app {
  min-height: 100vh;
}

a.md-button.md-icon-button {
  line-height: 2.5;
  margin: 0;
}
</style>
