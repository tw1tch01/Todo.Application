import Vue from "vue";
import Vuex from "vuex";
import todoItems from "@/store/modules/todoItem.module";
import common from "@/store/modules/common.module";

Vue.use(Vuex);
// const debug = process.env.NODE_ENV !== "production";

export default new Vuex.Store({
  modules: {
    todoItems,
    common
  },
  strict: false
});
