import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

import FixedAssetList from "../components/content/fixedAsset/MFixedAssetList.vue";
import ReceiptList from "../components/content/master-detail/MReceiptList.vue";



const routes = [
    { path: "/", component: FixedAssetList },
    { path: "/Receipt", component: ReceiptList },
];

const router = new VueRouter({
    mode: 'history',
    routes,
});

export default router;