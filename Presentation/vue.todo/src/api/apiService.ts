import { API_URL, API_VERSION } from "@/api/apiConfig";
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";

const ApiService = {
  init() {
    Vue.use(VueAxios, axios);
    Vue.axios.defaults.baseURL = API_URL;
    this.setHeader("X-Api-Version", API_VERSION);
  },

  setHeader(name: string, value: string) {
    Vue.axios.defaults.headers[name] = value;
  },

  async query(resource: string, params = null) {
    return Vue.axios.get(resource, params);
  },

  async get(resource: string, id: string) {
    return Vue.axios.get(`${resource}/${id}`);
  },

  async post(resource: string, data = null) {
    return Vue.axios.post(resource, data);
  },

  async put(resource: string, data) {
    return Vue.axios.put(resource, data);
  },

  async delete(resource: string) {
    return Vue.axios.delete(resource);
  }
};

export default ApiService;
