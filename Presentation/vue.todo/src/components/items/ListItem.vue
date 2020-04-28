<template>
  <md-card id="ListItem">
    <md-card-header>
      <div class="md-title">{{ item.name }}</div>
      <div v-if="item.dueDate" class="md-subhead">
        is due on {{ item.dueDate }}
      </div>
    </md-card-header>

    <md-card-content>
      <div class="md-layout">
        <div class="md-layout-item">
          <div class="box" :class="item.importance">{{ item.importance }}</div>
          <div class="box" :class="item.priority">{{ item.priority }}</div>
          <div class="box" :class="item.status">{{ item.status }}</div>
        </div>
        <div class="md-layout-item md-size-15 md-alignment-right">
          <router-link :to="item.itemId" class="md-button md-icon-button grey">
            <md-icon>visibility</md-icon>
          </router-link>

          <router-link :to="item.itemId" class="md-button md-icon-button green">
            <md-icon>create</md-icon>
          </router-link>

          <md-button
            @click="deleteItem(item.itemId)"
            class="md-icon-button red"
          >
            <md-icon>delete</md-icon>
          </md-button>
        </div>
      </div>
    </md-card-content>
  </md-card>
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
.md-card {
  margin-bottom: 20px;
  padding: 10px 20px;
  width: 420px;
  display: inline-block;
}

.md-layout-item {
  text-align: center;
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
