import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router';
import { routes } from './routes';
import VeeValidate from 'vee-validate';
import VueResource from 'vue-resource';
import storageManager from './storageManager';
import { store } from './store/store';

Vue.use(VueRouter);
Vue.use(VeeValidate);
Vue.use(VueResource);

export const router = new VueRouter({
  routes,
  mode: 'history'
});

Vue.http.options.root = 'http://HappyHours.Web/';

Vue.http.interceptors.push((request, next) => {
  var token = storageManager.getTokenBearer();
  if (token) {
    request.headers.set('Authorization', `Bearer ${token}`);
  }
  next((response) => {
    debugger;
    if (response.status == 401) {
      storageManager.clearTokenBearer();
      router.push('/');
    }
  });
});

new Vue({
  el: '#app',
  store: store,
  router: router,
  render: h => h(App)
})
