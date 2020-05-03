import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Main from "@/views/Main.vue";
import TodoItemsList from "@/views/TodoItemsList.vue";
import AddItem from "@/components/items/AddItem.vue";
import EditItem from "@/components/items/EditItem.vue";
import List from "@/components/items/List.vue";
import Item from "@/components/items/Item.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Main",
    component: Main,
    meta: {
      breadcrumb: "Main"
    }
  },
  {
    path: "/items",
    component: TodoItemsList,
    meta: {
      breadcrumb: "Items"
    },
    children: [
      {
        path: "",
        name: "items",
        component: List
      },
      {
        path: "add",
        name: "items/add",
        component: AddItem
      },
      {
        path: ":itemId",
        name: "items/view",
        component: Item
      },
      {
        path: ":itemId/edit",
        name: "items/edit",
        component: EditItem
      }
    ]
  }

  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
