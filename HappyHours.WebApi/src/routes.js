import Home from './components/Home.vue';
import Signin from './components/Signin.vue';
import Signup from './components/Signup/Signup.vue';
import ActivateEmail from './components/Signup/ActivateEmail.vue';
import ForgotPassword from './components/ForgotPassword.vue';

import { router } from './main';
import storageManager from './storageManager';

export const routes = [

  { path: '/', component: Home },

  { path: '/Signin', component: Signin, beforeEnter: (to, from, next) => {

    // if the user is already logged in, pass him to the main page
    if (storageManager.getTokenBearer()) {
      router.push('/');
      return
    }

    next();

  } },

  { path: '/Signup', component: Signup, beforeEnter: (to, from, next) => {

    if (storageManager.getTokenBearer()) {
      router.push('/');
      return
    }

    next();

  } },

  { path: '/ForgotPassword', component: ForgotPassword },

  { path: '/ActivateEmail', component: ActivateEmail }
  
];
