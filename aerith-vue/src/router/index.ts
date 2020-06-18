import Vue from "vue";
import VueRouter, {
  RouteConfig,
  Route,
  NavigationGuardNext,
  RouteRecord
} from "vue-router";
import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import AdminRoot from "../views/admin/AdminRoot.vue";
import Teams from "../views/admin/Teams.vue";
import store from "@/store";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: { requiresAuth: true }
  },
  {
    path: "/admin",
    component: AdminRoot,
    meta: { requiresAuth: true },
    children: [
      {
        path: "teams",
        component: Teams,
        // a meta field
        meta: { requiresAuth: true }
      }
    ]
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: "/callback/google",
    name: "Google Callback",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../views/callbacks/GoogleCallback.vue"
      )
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to: Route, from: Route, next: NavigationGuardNext) => {
  if (to.matched.some((record: RouteRecord) => record.meta.requiresAuth)) {
    // this route requires auth, check if logged in
    // if not, redirect to login page.
    if (!store.getters["auth/isAuthenticated"]) {
      next({
        path: "/login",
        query: { redirect: to.fullPath }
      });
    } else {
      next();
    }
  } else {
    next(); // make sure to always call next()!
  }
});

export default router;
