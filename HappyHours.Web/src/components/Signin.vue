
<template>
  <div>
    <div class="form-container">

      <div class="form-header">
        <span>Signin</span>
      </div>

      <div class="form-content">

        <div class="form-item">
          <label>Email</label>
          <div class="input-item">
            <input type="text" placeholder="Email" v-model="user.email" name="email" v-validate="'required|email'" data-vv-as="Email"/>
            <div class="field-error" v-show="errors.has('email') && fields.email.touched">{{ errors.first('email') }}</div>
          </div>
        </div>

        <div class="form-item">
          <label>Password</label>
          <div class="input-item">
            <input type="password" placeholder="Password" v-model="user.password" name="password" v-validate="'required'" data-vv-as="Password"/>
            <div class="field-error" v-show="errors.has('password') && fields.password.touched">{{ errors.first('password') }}</div>
          </div>
        </div>

        <!--<router-link to="/forgotpassword" class="forgot-password">Forgot password?</router-link>-->

        <div class="form-item">
          <div class="field-error" v-show="errors.has('globalError')">{{ errors.first('globalError') }}</div>
        </div>

        <div class="form-item">
          <button class="btn btn-right" @click.prevent="signin">Signin</button>
        </div>

      </div>

    </div>

  </div>
</template>

<script>

  import { ApiCredentials } from '../ApiData';
  import { validatorMixin } from '../mixins/validatorMixins';

  export default {

    mixins: [validatorMixin],

    apiCredentials: null,

    data() {
      return {
        user: {
          email: '',
          password: ''
        }
      }
    },

    methods: {

      signin() {

        // validates step 2
        this.$validator.validateAll().then((isValid) => {

          // mark the step1 fields as touched, as we want to see the errors
          this.markFieldsAsTouched(['email', 'password']);

          if (!isValid)
            return;

          // in case all validations passed, we perform ajax call to signin
          this.performSignin().then(({data}) => {

            this.clearErrors();

            if (data.ErrorCode == 1)
              this.errors.add('globalError', 'Invalid Email/Password');
            else if (data.ErrorCode == 6)
              this.errors.add('globalError', 'Please verify your email.');
            else
              this.$router.push('/');
          });

        });

      },

      performSignin() {

        return this.$http.post('http://HappyHours.Web/api/Signin', {
          credentials: this.apiCredentials,
          email: this.user.email,
          password: this.user.password
        });

      }

    },

    created() {
      this.apiCredentials = ApiCredentials;
    }

  }

</script>

<style scoped>

  .forgot-password {
    float: right;
    margin-right: 70px;
    margin-top: 5px;
    font-size: 13px;
  }


</style>
