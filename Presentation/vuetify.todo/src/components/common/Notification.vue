<template>
  <v-snackbar
    top
    right
    :color="notification.colour"
    v-model="notification.show"
    :timeout="notification.timeout"
    >{{ notification.message }}</v-snackbar
  >
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: mapState({
    notification: state => state.common.notification
  })
};
</script>
