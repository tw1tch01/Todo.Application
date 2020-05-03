<template>
  <v-row :key="$route.params.itemId">
    <ProgressBar :show="this.isLoading" />
    <v-col md="12" v-if="!isLoading">
      <v-tabs grow>
        <v-tab ref="details" key="details">Details</v-tab>
        <v-tab key="comments">Comments</v-tab>
        <v-tab key="childItems">Children</v-tab>

        <v-tab-item key="details">
          <v-row>
            <v-col md="7">
              <v-card flat>
                <v-text-field
                  v-model="viewModel.item.name"
                  label="Name"
                  readonly
                />

                <v-textarea
                  v-model="viewModel.item.description"
                  label="Description"
                  readonly
                />

                <v-text-field
                  v-if="viewModel.item.dueDate"
                  v-model="viewModel.item.dueDate"
                  label="Due Date"
                  readonly
                />

                <v-text-field
                  label="Importance"
                  :value="viewModel.item.importance"
                  readonly
                ></v-text-field>

                <v-text-field
                  label="Priority"
                  :value="viewModel.item.priority"
                  readonly
                ></v-text-field>
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
          <v-input>
            <v-text-field type="text" />
            <v-btn icon @click="addComment">
              <v-icon>mdi-comment-plus-outline</v-icon>
            </v-btn>
          </v-input>
        </v-tab-item>

        <v-tab-item key="childItems">
          <ListItem
            v-for="childItem in viewModel.item.childItems"
            :key="childItem.itemId"
            :item="childItem"
          ></ListItem>
        </v-tab-item>
      </v-tabs>
      <v-btn @click="$router.go(-1)">Back</v-btn>
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
    },
    addComment() {
      console.log("add comment");
    }
  },
  created() {
    this.viewItem();
  },
  watch: {
    "$route.params.itemId": function() {
      console.log("render");
      this.viewItem();
    }
  }
};
</script>

<style lang="scss">
.v-card {
  margin-bottom: 5px;
}
</style>
