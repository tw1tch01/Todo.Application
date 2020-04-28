<template>
  <div id="List">
    <p v-if="!items">There are no items.</p>
    <ListItem v-for="item in items" :key="item.itemId" :item="item"></ListItem>
  </div>
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
    items: state => state.todoItems.items
  }),
  created() {
    this.$store.dispatch(`${GET_ALL_ITEMS}`);
  }
};
</script>

<style lang="scss">
#List {
  text-align: left;
}
</style>
