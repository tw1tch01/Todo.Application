<template>
  <router-view>
    <ProgressBar :show="showLoader" />
  </router-view>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "TodoItemsList",
  computed: mapState({
    isSaving: state => state.todoItems.isSaving,
    isLoading: state => state.todoItems.isLoading,
    showLoader() {
      return this.isSaving || this.isSaving;
    }
  })
};
</script>
