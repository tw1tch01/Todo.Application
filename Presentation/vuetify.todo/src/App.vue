<template>
  <v-app>
    <Notification />

    <v-navigation-drawer :clipped="true" :permanent="true" app overflow>
      <v-list>
        <v-list-item @click.prevent to="/items">
          <v-list-item-action>
            <v-icon>mdi-clipboard-text</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Items</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar :clipped-left="true" app color="dark-grey">
      <v-toolbar-title @click="$router.push('/')">
        Todo Application
      </v-toolbar-title>
      <v-spacer />
      <v-switch v-model="$vuetify.theme.dark" />
    </v-app-bar>

    <v-content>
      <v-container fluid>
        <v-row>
          <v-breadcrumbs :items="crumbs"></v-breadcrumbs>
        </v-row>
        <router-view />
      </v-container>
    </v-content>

    <v-footer app>
      <span class="px-4">&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>

    <!-- <v-bottom-navigation app></v-bottom-navigation> -->
  </v-app>
</template>

<script lang="ts">
export default {
  name: "App",
  computed: {
    crumbs() {
      const pathArray = this.$route.path.split("/");
      pathArray[0] = "home";
      // const breadcrumbs = pathArray.reduce((breadcrumbArray, href, idx) => {
      //   const to = breadcrumbArray[idx - 1]
      //     ? "/" + breadcrumbArray[idx - 1].href + "/" + href
      //     : "/" + href;

      //   console.log(this.$route.matched);
      //   console.log(this.$route.matched[idx]);
      //   const text = this.$route.matched[idx]?.meta.breadcrumb || href;
      //   console.log(to, text, href, idx);
      //   breadcrumbArray.push({
      //     disabled: false,
      //     href: href,
      //     text: text,
      //     to: to
      //   });
      //   return breadcrumbArray;
      // }, []);
      // console.log(breadcrumbs);
      return pathArray.map(p => {
        return {
          text: p
        };
      });
    }
  }
};
</script>

<style lang="scss">
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.v-toolbar__title {
  cursor: pointer;
}
</style>
