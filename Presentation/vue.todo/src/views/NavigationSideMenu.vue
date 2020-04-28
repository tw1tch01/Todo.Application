<template>
  <div>
    <md-toolbar class="md-transparent" md-elevation="0">
      Navigation
    </md-toolbar>

    <md-list>
      <md-list-item>
        <md-icon>assignment</md-icon>
        <router-link class="md-list-item-text" to="/">Items</router-link>
      </md-list-item>
    </md-list>
  </div>
</template>

<script>
export default {
  name: "NavigationSideMenu"
};
</script>
