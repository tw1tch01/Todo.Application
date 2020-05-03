<template>
  <v-progress-linear
    id="progress-bar"
    :active="this.show"
    top
    absolute
    indeterminate
  ></v-progress-linear>
</template>

<script>
export default {
  name: "ProgressBar",
  props: ["show"]
};
</script>
