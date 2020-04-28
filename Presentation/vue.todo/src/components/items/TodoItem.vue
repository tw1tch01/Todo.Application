<template>
  <form
    novalidate
    class="md-layout"
    @submit.prevent
    ref="editForm"
    v-if="item != null"
  >
    <md-card class="md-layout-item md-size-50 md-medium-size-100">
      <md-card-header>
        <div class="md-title">Item</div>
      </md-card-header>

      <md-card-content>
        <md-field :class="getValidationClass('name')">
          <label for="name">Name</label>
          <md-input
            name="name"
            v-model="item.name"
            :disabled="!editMode || isSaving"
          />
          <span class="md-error" v-if="failures.Name">
            {{ failures.Name.join(", ") }}</span
          >
        </md-field>

        <md-field :class="getValidationClass('description')">
          <label for="description">Description</label>
          <md-textarea
            name="description"
            v-model="item.description"
            :disabled="!editMode || isSaving"
          />
          <span class="md-error" v-if="failures.Description">
            {{ failures.Description.join(", ") }}</span
          >
        </md-field>

        <md-field :class="getValidationClass('rank')">
          <label for="rank">Rank</label>
          <md-input
            v-model.number="item.rank"
            name="rank"
            type="number"
            min="0"
            :disabled="!editMode || isSaving"
          />
          <span class="md-error" v-if="failures.Rank">
            {{ failures.Rank.join(", ") }}</span
          >
          {{ failures.Rank }}
        </md-field>

        <md-field>
          <md-datepicker
            v-model="item.dueDate"
            :class="getValidationClass('dueDate')"
            :disabled="!editMode || isSaving"
          />
          <span class="md-error" v-if="failures.DueDate">
            {{ failures.DueDate.join(", ") }}</span
          >
        </md-field>

        <md-field :class="getValidationClass('importance')">
          <label for="importance">Importance</label>
          <md-select
            v-model="item.importance"
            name="importance"
            :disabled="!editMode || isSaving"
          >
            <md-option value="Trivial">Trivial</md-option>
            <md-option value="Minor">Minor</md-option>
            <md-option value="Major">Major</md-option>
            <md-option value="Critical">Critical</md-option>
          </md-select>
          <span class="md-error" v-if="failures.Importance">
            {{ failures.Importance.join(", ") }}</span
          >
        </md-field>

        <md-field :class="getValidationClass('priority')">
          <label for="priority">Priority</label>
          <md-select
            v-model="item.priority"
            name="priority"
            :disabled="!editMode || isSaving"
          >
            <md-option value="Lowest">Lowest</md-option>
            <md-option value="Low">Low</md-option>
            <md-option value="Medium">Medium</md-option>
            <md-option value="High">High</md-option>
            <md-option value="Urgent">Urgent</md-option>
          </md-select>
          <span class="md-error" v-if="failures.Priority">
            {{ failures.Priority.join(", ") }}</span
          >
        </md-field>
      </md-card-content>

      <md-progress-bar md-mode="indeterminate" v-if="isSaving" />

      <md-card-actions>
        <md-button v-if="!editMode" class="md-primary" @click="editMode = true"
          >Edit Item</md-button
        >
        <md-button v-if="editMode" @click="cancelEdits()">Cancel</md-button>
        <md-button
          v-if="editMode"
          type="submit"
          class="md-primary"
          :disabled="!editMode || isSaving"
          >Update</md-button
        >
      </md-card-actions>
      <md-snackbar
        :md-active.sync="showMessage"
        :md-duration="4000"
        :class="!apiResponse.successful ? 'error' : ''"
        >{{ apiResponse.message }}</md-snackbar
      >
    </md-card>
    <md-card class="md-layout-item md-size-50 md-medium-size-100">
      <p>item: {{ item }}</p>
      <p>apiResponse: {{ apiResponse }}</p>
      <p>isSaving: {{ isSaving }}</p>
      <p>failures: {{ failures }}</p>
      <p>hasMessage: {{ hasMessage }}</p>
    </md-card>
  </form>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
import { mapState } from "vuex";
import { GET_ITEM, ADD_ITEM } from "@/store/action-types";

export default {
  name: "TodoItem",
  data: () => ({
    form: {
      name: null,
      description: null,
      rank: 0,
      dueDate: null,
      importance: "Trivial",
      priority: "Lowest"
    },
    showMessage: false,
    editMode: false
  }),
  mixins: [validationMixin],
  computed: mapState({
    item: state => state.todoItems.item?.item,
    canCancel: state => state.todoItems.item?.canCancel,
    canComplete: state => state.todoItems.item?.canComplete,
    canStart: state => state.todoItems.item?.canStart,
    apiResponse: state => state.todoItems.apiResponse,
    isSaving: state => state.todoItems.isSaving,
    failures: state => state.todoItems.apiResponse.data.Failures ?? {},
    hasMessage: state => (state.todoItems.apiResponse.message ? true : false)
  }),
  validations: {
    form: {
      name: {
        required,
        maxLength: maxLength(64)
      },
      description: {
        required
      },
      rank: {
        required
      },
      importance: {
        required
      },
      priority: {
        required
      }
    }
  },
  methods: {
    getValidationClass(fieldName) {
      const field = this.$v.form[fieldName];

      if (field) {
        return {
          "md-invalid": field.$invalid && field.$dirty
        };
      }
    },
    clearForm() {
      this.$v.$reset();
      this.item.name = null;
      this.item.description = null;
      this.item.rank = null;
      this.item.dueDate = null;
      this.item.importance = null;
      this.item.priority = null;
    },
    saveItem() {
      this.$store
        .dispatch(`todoItems/${ADD_ITEM}`, this.form)
        .then(() => {
          if (this.apiResponse.successful) {
            this.clearForm();
            setTimeout(() => {
              this.$router.push({
                name: "items/item",
                params: { itemId: this.apiResponse.data.ItemId }
              });
            }, 3000);
          }
        })
        .finally(() => {
          this.showMessage = true;
        });
    },
    validateItem() {
      this.$v.$touch();

      if (!this.$v.$invalid) {
        this.saveItem();
      }
    },
    cancelEdits() {
      this.editMode = false;
    }
  },
  created() {
    this.$store.dispatch(GET_ITEM, this.$route.params.itemId);
  }
};
</script>

<style lang="scss" scoped>
.md-progress-bar {
  position: absolute;
  top: 0;
  right: 0;
  left: 0;
}
</style>
