import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Vue from 'vue'
import App from './App.vue'
import router from "./router";
import { store } from './store'

Vue.config.productionTip = false
Vue.use(require('vue-shortkey'))

new Vue({
    store,
    router,
    render: h => h(App),
}).$mount('#app')