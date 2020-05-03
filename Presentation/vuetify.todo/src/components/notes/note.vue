<template>
  <v-card flat>
    <v-card-text>
      <v-textarea :readonly="!editComment" v-model="note.comment"></v-textarea>
      <span> By {{ note.created.by }} on {{ note.created.on }} </span>
    </v-card-text>
    <v-card-actions>
      <v-btn icon @click="editComment = !editComment">
        <v-icon>mdi-comment-edit-outline</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-comment-remove-outline</v-icon>
      </v-btn>
      <v-btn icon @click="addReply = !addReply">
        <v-icon>mdi-comment-arrow-left-outline</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "Comments",
  data: () => ({
    editComment: false,
    addReply: false
  }),
  props: ["note"]
};
</script>

<style lang="scss" scoped>
.v-card__subtitle,
.v-card__text,
.v-card__title {
  padding: 0px;
  padding-left: 20px !important;
}

// .v-card__actions {
//   position: absolute;
//   top: 0;
//   right: 20px;
// }
</style>
