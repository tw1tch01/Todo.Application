import itemQueries from "@/api/items/itemsQueries";
import itemCommands from "@/api/items/itemCommands";
import {
  GET_ALL_ITEMS,
  GET_ITEM,
  ADD_ITEM,
  UPDATE_ITEM,
  DELETE_ITEM,
  ADD_CHILD_ITEM,
  CANCEL_ITEM,
  COMPLETE_ITEM,
  RESET_ITEM,
  START_ITEM
} from "@/store/action-types";
import {
  SET_ITEMS,
  SET_ITEM,
  SET_ISLOADING,
  SET_ISSAVING,
  SET_APIRESPONSE_SUCCESS,
  SET_APIRESPONSE_ERROR,
  CLEAR_APIRESPONSE,
  REMOVE_ITEM
} from "@/store/mutation-types";

const state = {
  isLoading: false,
  isSaving: false,
  items: [],
  item: null,
  apiResponse: {
    status: null,
    successful: false,
    message: null,
    data: {}
  }
};

const getters = {};

const actions = {
  async [GET_ALL_ITEMS]({ commit }) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISLOADING, true);
    await itemQueries
      .all()
      .then(response => {
        commit(SET_ITEMS, response.data);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error);
      })
      .finally(() => {
        commit(SET_ISLOADING, false);
      });
  },

  async [GET_ITEM]({ commit }, itemId: string) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISLOADING, true);
    await itemQueries
      .get(itemId)
      .then(response => {
        commit(SET_ITEM, response.data);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISLOADING, false);
      });
  },

  async [ADD_ITEM]({ commit }, item) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .addItem(item)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [UPDATE_ITEM]({ commit }, { itemId, item }) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .updateItem(itemId, item)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [DELETE_ITEM]({ commit }, itemId) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .deleteItem(itemId)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
        commit(REMOVE_ITEM, itemId);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [ADD_CHILD_ITEM]({ commit }, { itemId, item }) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .addChildItem(itemId, item)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [CANCEL_ITEM]({ commit }, itemId) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .cancelItem(itemId)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [COMPLETE_ITEM]({ commit }, itemId) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .completeItem(itemId)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [RESET_ITEM]({ commit }, itemId) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .resetItem(itemId)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  },

  async [START_ITEM]({ commit }, itemId) {
    commit(CLEAR_APIRESPONSE);
    commit(SET_ISSAVING, true);
    await itemCommands
      .startItem(itemId)
      .then(response => {
        commit(SET_APIRESPONSE_SUCCESS, response);
      })
      .catch(error => {
        commit(SET_APIRESPONSE_ERROR, error.response);
      })
      .finally(() => {
        commit(SET_ISSAVING, false);
      });
  }
};

const mutations = {
  [SET_ITEMS](state, items) {
    state.items = items;
  },

  [SET_ITEM](state, item) {
    state.item = item;
  },

  [SET_ISLOADING](state, isLoading) {
    state.loading = isLoading;
  },

  [SET_ISSAVING](state, isSaving) {
    state.isSaving = isSaving;
  },

  [SET_APIRESPONSE_SUCCESS](state, response) {
    state.apiResponse = {
      status: response.status,
      successful: true,
      message: response.data.message,
      data: response.data.data
    };
  },

  [SET_APIRESPONSE_ERROR](state, error) {
    state.apiResponse = {
      status: error.status,
      successful: false,
      message: error.data.message
        ? error.data.message
        : "An unexpected error has occurred!",
      data: error.data.data ? error.data.data : {}
    };
  },

  [CLEAR_APIRESPONSE](state) {
    state.apiResponse = {
      status: null,
      successful: false,
      message: null,
      data: {}
    };
  },

  [REMOVE_ITEM](state, itemId) {
    state.items = state.items.filter(item => item.itemId !== itemId);
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
