import {
  SET_INFO_NOTIFICATION,
  SET_ERROR_NOTIFICATION,
  SET_SUCCESS_NOTIFICATION
} from "@/store/mutation-types";

const state = {
  notification: {
    message: null,
    colour: "",
    show: false,
    timeout: 3000
  }
};

const getters = {};

const actions = {};

const mutations = {
  [SET_INFO_NOTIFICATION](state, message) {
    state.notification.message = message;
    state.notification.colour = "info";
    state.notification.show = true;
  },
  [SET_ERROR_NOTIFICATION](state, message) {
    state.notification.message = message;
    state.notification.colour = "error";
    state.notification.show = true;
  },
  [SET_SUCCESS_NOTIFICATION](state, message) {
    state.notification.message = message;
    state.notification.colour = "info";
    state.notification.show = true;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
