<template>
  <v-card flat>
    <v-card-subtitle>
      By {{ note.created.by }} on {{ note.created.on }}
    </v-card-subtitle>
    <v-card-text>
      <v-row>
        <v-col md="12">
          <p>
            {{ note.comment }}
          </p>
          <Comments
            v-for="reply in note.replies"
            :note="reply"
            :key="reply.noteId"
          ></Comments>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  name: "Comments",
  props: ["note"]
};
</script>
