import Home from './components/Home.vue';
import Signin from './components/Signin.vue';
import Signup from './components/Signup/Signup.vue';
import ActivateEmail from './components/Signup/ActivateEmail.vue';
import ForgotPassword from './components/ForgotPassword.vue';

export const routes = [
  { path: '/', component: Home },
  { path: '/Signin', component: Signin },
  { path: '/Signup', component: Signup },
  { path: '/ForgotPassword', component: ForgotPassword },
  { path: '/ActivateEmail', component: ActivateEmail }
];
