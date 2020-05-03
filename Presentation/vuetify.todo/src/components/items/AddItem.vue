<template>
  <v-row>
    <v-col lg="5" md="8" sm="12">
      <v-form ref="addItemForm" @submit.prevent="validateItem">
        <v-text-field
          v-model="form.name"
          :counter="64"
          label="Name"
          required
          :error-messages="getErrors($v.form.name, 'Name')"
          @input="$v.form.name.$touch()"
          @blur="$v.form.name.$touch()"
          :disabled="isSaving"
        />

        <v-textarea
          v-model="form.description"
          label="Description"
          required
          :error-messages="getErrors(this.$v.form.description, 'Description')"
          @input="$v.form.description.$touch()"
          @blur="$v.form.description.$touch()"
          :disabled="isSaving"
        />

        <v-text-field
          v-model.number="form.rank"
          type="number"
          min="0"
          label="Rank"
          required
          :error-messages="getErrors(this.$v.form.rank, 'Rank')"
          @input="$v.form.rank.$touch()"
          @blur="$v.form.rank.$touch()"
          :disabled="isSaving"
        />

        <v-menu
          ref="menu"
          v-model="menu"
          :close-on-content-click="false"
          :return-value.sync="form.dueDate"
          transition="scale-transition"
          offset-y
          min-width="290px"
          :disabled="isSaving"
        >
          <template v-slot:activator="{ on }">
            <v-text-field
              v-model="form.dueDate"
              label="Due Date"
              readonly
              v-on="on"
              :disabled="isSaving"
            />
          </template>
          <v-date-picker
            v-model="form.dueDate"
            no-title
            scrollable
            :disabled="isSaving"
          >
            <v-spacer />
            <v-btn text color="primary" @click="menu = false">Cancel</v-btn>
            <v-btn text color="primary" @click="$refs.menu.save(form.dueDate)"
              >OK</v-btn
            >
          </v-date-picker>
        </v-menu>

        <v-select
          :items="importanceSelection"
          label="Importance"
          v-model="form.importance"
          @blur="$v.form.importance.$touch()"
          :error-messages="getErrors(this.$v.form.importance, 'Importance')"
          :disabled="isSaving"
        ></v-select>

        <v-select
          :items="prioritySelection"
          label="Priority"
          v-model="form.priority"
          @blur="$v.form.priority.$touch()"
          :error-messages="getErrors(this.$v.form.priority, 'Priority')"
          :disabled="isSaving"
        ></v-select>

        <div>
          <v-spacer />
          <v-btn @click="$router.go(-1)">
            Cancel
          </v-btn>
          <v-btn
            class="mr-4"
            type="submit"
            :disabled="!isValid || isSaving"
            :loading="isSaving"
          >
            Create
          </v-btn>
        </div>
      </v-form>
    </v-col>
    <v-col lg="5" md="3" sm="0" offset="1">
      <json-viewer boxed :value="form" />
    </v-col>
  </v-row>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
import { mapState } from "vuex";
import { ADD_ITEM, ADD_CHILD_ITEM } from "@/store/action-types";

export default {
  name: "AddItem",
  props: ["parentItemId"],
  data: () => ({
    form: {
      name: null,
      description: null,
      rank: null,
      dueDate: null,
      importance: null,
      priority: null
    },
    importanceSelection: ["Trivial", "Minor", "Major", "Critical"],
    prioritySelection: ["Lowest", "Low", "Medium", "High", "Urgent"],
    menu: false
  }),
  mixins: [validationMixin],
  computed: mapState({
    apiResponse: state => state.todoItems.apiResponse,
    isSaving: state => state.todoItems.isSaving,
    failures: state => state.todoItems.apiResponse.data.Failures ?? {},
    isValid() {
      return !this.$v.form.$invalid;
    }
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
    getErrors(formElement, elementName) {
      const errors = [];
      if (!formElement.$dirty) return errors;

      if (formElement.maxLength !== undefined) {
        !formElement.maxLength &&
          errors.push(
            `${elementName} must be at most ${formElement.$params.maxLength.max} characters long`
          );
      }

      if (formElement.required !== undefined) {
        !formElement.required && errors.push(`${elementName} is required.`);
      }

      return errors;
    },
    clearForm() {
      this.$refs.addItemForm.reset();
      this.$v.$reset();
    },
    saveItem() {
      if (!this.parentItemId) {
        this.$store.dispatch(ADD_ITEM, this.form).then(() => {
          if (this.apiResponse.successful) {
            this.clearForm();
            setTimeout(() => {
              this.$router.push(`/items/${this.apiResponse.data.ItemId}`);
            }, 3000);
          }
        });
      } else {
        this.$store
          .dispatch(ADD_CHILD_ITEM, {
            itemId: this.parentItemId,
            item: this.form
          })
          .then(() => {
            if (this.apiResponse.successful) {
              this.clearForm();
              // setTimeout(() => {
              //   this.$router.push({
              //     name: "item",
              //     params: { itemId: this.apiResponse.data.ItemId }
              //   });
              // }, 3000);
            }
          });
      }
    },
    validateItem() {
      this.$v.$touch();

      if (!this.$v.$invalid) {
        this.saveItem();
      }
    }
  }
};
</script>
