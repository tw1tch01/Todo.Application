import Vue from "vue";
import App from "@/App.vue";
import VueMaterial from "vue-material";
import router from "@/router";
import store from "@/store";
import ApiService from "./api/apiService";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";

// Theming
Vue.use(VueMaterial);

Vue.config.productionTip = false;

// Services
ApiService.init();

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
