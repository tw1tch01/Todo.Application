<template>
  <v-row id="List">
    <ProgressBar :show="this.isLoading" />
    <v-btn to="/items/add" large icon class="add-item">
      <v-icon large>mdi-sticker-plus</v-icon>
    </v-btn>
    <v-col cols="12" v-if="!this.isLoading">
      <p v-if="noItems">There are no items.</p>
      <ListItem
        v-else
        v-for="item in items"
        :key="item.itemId"
        :item="item"
      ></ListItem>
    </v-col>
  </v-row>
</template>

<script>
import { mapState } from "vuex";
import { GET_ALL_ITEMS } from "@/store/action-types";
import ListItem from "@/components/items/ListItem.vue";

export default {
  name: "TodoList",
  components: {
    ListItem
  },
  computed: mapState({
    items: state => state.todoItems.items,
    isLoading: state => state.todoItems.isLoading,
    noItems() {
      return !this.items || this.items.length < 1;
    }
  }),
  created() {
    this.$store.dispatch(GET_ALL_ITEMS);
  }
};
</script>

<style lang="scss" scoped>
#List {
  text-align: left;
}

.add-item {
  position: absolute;
  z-index: 0;
  top: 0;
  right: 0;
  margin: 25px;
}
</style>
