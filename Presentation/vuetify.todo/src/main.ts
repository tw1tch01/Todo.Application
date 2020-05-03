import apiService from "@/api/apiService";
import Vue from "vue";
import App from "@/App.vue";
import router from "@/router";
import store from "@/store";
import vuetify from "@/plugins/vuetify";
import JsonViewer from "vue-json-viewer";
import Notification from "@/components/common/Notification.vue";
import ProgressBar from "@/components/common/ProgressBar.vue";

Vue.config.productionTip = false;

// Services
apiService.init();

// Global Components
Vue.component("JsonViewer", JsonViewer);
Vue.component("Notification", Notification);
Vue.component("ProgressBar", ProgressBar);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
