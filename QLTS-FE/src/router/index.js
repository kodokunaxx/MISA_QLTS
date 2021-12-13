import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

import FixedAssetList from "../components/content/MFixedAssetList.vue";



const routes = [
    { path: "/", component: FixedAssetList },
];

const router = new VueRouter({
    mode: 'history',
    routes,
});

export default router;