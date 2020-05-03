<template>
  <v-row v-if="!isLoading">
    <v-col md="12">
      <v-tabs grow>
        <v-tab key="details">Details</v-tab>
        <v-tab key="comments">Comments</v-tab>
        <v-tab key="childItems" v-if="hasChildren">Children</v-tab>

        <v-tab-item key="details">
          <v-row>
            <v-col md="7">
              <v-card flat>
                <v-card-title>{{ viewModel.item.name }}</v-card-title>
                <v-card-subtitle
                  >is due on {{ viewModel.item.dueDate }}
                </v-card-subtitle>
                <v-card-text>
                  <v-textarea readonly :value="viewModel.item.description" />
                  <p>
                    <span>has {{ viewModel.item.priority }}</span> priority and
                    is of
                    <span>{{ viewModel.item.importance }}</span> importance.
                  </p>
                </v-card-text>
              </v-card>
            </v-col>
            <v-col md="4" offset="1">
              <json-viewer
                boxed
                :value="viewModel"
                expanded
                :expand-depth="2"
              ></json-viewer>
            </v-col>
          </v-row>
        </v-tab-item>

        <v-tab-item key="comments">
          <Comments
            v-for="note in viewModel.item.notes"
            :note="note"
            :key="note.noteId"
          ></Comments>
        </v-tab-item>

        <v-tab-item key="childItems">
          <ListItem
            v-for="childItem in viewModel.item.childItems"
            :key="childItem.itemId"
            :item="childItem"
          ></ListItem>
        </v-tab-item>
      </v-tabs>
    </v-col>
  </v-row>
</template>

<script>
import { mapState } from "vuex";
import { GET_ITEM } from "@/store/action-types";
import Comments from "@/components/items/Comments.vue";
import ListItem from "@/components/items/ListItem.vue";

export default {
  name: "Item",
  components: {
    Comments,
    ListItem
  },
  computed: mapState({
    viewModel: state => state.todoItems.item,
    apiResponse: state => state.todoItems.apiResponse,
    isSaving: state => state.todoItems.isSaving,
    isLoading: state => state.todoItems.isLoading,
    hasChildren() {
      if (this.viewModel.item?.childItems === undefined) return false;

      return this.viewModel.item.childItems.length > 0;
    }
  }),
  methods: {
    viewItem() {
      this.$store.dispatch(GET_ITEM, this.$route.params.itemId);
    }
  },
  created() {
    this.viewItem();
  }
};
</script>

<style lang="scss">
.v-card {
  margin-bottom: 5px;
}
</style>
