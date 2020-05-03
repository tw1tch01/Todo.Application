<template>
  <v-card>
    <v-card-title>
      {{ item.name }}
    </v-card-title>
    <v-card-subtitle v-if="item.dueDate">
      is due on {{ item.dueDate }}
    </v-card-subtitle>
    <v-card-text>
      <v-row>
        <v-col md="3" sm="12">
          {{ item.importance }} | {{ item.priority }}
        </v-col>
        <v-col md="8" offset="1" sm="0">
          <json-viewer boxed :value="item" />
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-actions>
      <v-btn icon :to="`/items/${item.itemId}`">
        <v-icon>mdi-eye</v-icon>
      </v-btn>
      <v-btn icon :to="`/items/${item.itemId}/edit`">
        <v-icon>mdi-circle-edit-outline</v-icon>
      </v-btn>
      <v-btn icon @click="deleteItem(item.itemId)">
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { DELETE_ITEM } from "@/store/action-types";

export default {
  name: "ListItem",
  props: ["item"],
  methods: {
    deleteItem(itemId) {
      this.$store.dispatch(DELETE_ITEM, itemId);
    }
  }
};
</script>

<style lang="scss" scoped>
.v-card {
  margin-bottom: 10px;
  padding: 10px 20px;
}

.box {
  width: 160px;
  text-align: center;
  padding: 15px;
  margin: 5px 5px;
  display: inline-block;
  box-shadow: 1px 1px 2px 2px lightgrey;
}

.Critical,
.Urgent,
.Overdue {
  background: salmon;
  color: white;
}

.Completed {
  background: lightgreen;
  color: white;
}
</style>
