import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import TodoItemsList from "@/views/TodoItemsList.vue";
import AddTodoItem from "@/components/items/AddTodoItem.vue";
import TodoItem from "@/components/items/TodoItem.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "TodoItemsList",
    component: TodoItemsList
  },
  {
    path: "/add",
    name: "AddTodoItem",
    component: AddTodoItem
  },
  {
    path: "/:itemId",
    name: "TodoItem",
    component: TodoItem
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
