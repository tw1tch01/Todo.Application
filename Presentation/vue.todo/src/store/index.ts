import Vue from "vue";
import Vuex from "vuex";
import todoItems from "@/store/modules/todoItems.module";

Vue.use(Vuex);

// const debug = process.env.NODE_ENV !== "production";

export default new Vuex.Store({
  modules: {
    todoItems
  },
  strict: false
});
