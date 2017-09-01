
<template>
  <div>
    <div class="form-container" :class="{ 'disabled': isLoadingSignin }">

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

        <div class="form-item" v-if="resendEmail">
          <a href="#">Resend email</a>
        </div>

        <div class="form-item">
          <button class="btn btn-right" @click.prevent="signin">Signin</button>
          <app-loading isLoading="isLoadingSignin" v-if="isLoadingSignin"/>
        </div>

      </div>

    </div>

  </div>
</template>

<script>

  import { validatorMixin } from '../mixins/validatorMixins';
  import queryStringBuilder from '../queryStringBuilder.js';
  import storageManager from '../storageManager';
  import Loading from './Loading.vue';

  import { mapGetters, mapActions } from 'vuex';

  export default {

    components: {
      appLoading: Loading
    },

    mixins: [validatorMixin],

    data() {
      return {
        user: {
          email: '',
          password: ''
        },
        resendEmail: false
      }
    },

    computed: {

      ...mapGetters([
        'isLoadingSignin'
      ])

    },

    methods: {

      ...mapActions([
        'fetchUserDetails',
        'setIsLoadingSignin'
      ]),

      signin() {

        // validates step 2
        this.$validator.validateAll().then((isValid) => {

          // mark the step1 fields as touched, as we want to see the errors
          this.markFieldsAsTouched(['email', 'password']);

          if (!isValid)
            return;

          this.setIsLoadingSignin(true);

          // in case all validations passed, we perform ajax call to signin
          this.performSignin().then(({data}) => {

            this.resendEmail = false;
            this.clearErrors();

            storageManager.setTokenBearer(data.access_token);

            this.fetchUserDetails();

            //this.$router.push('/');

          }, (error) => {

            this.setIsLoadingSignin(false);

            this.resendEmail = false;
            this.clearErrors();

            let errorResponse = JSON.parse(error.bodyText);
            let errorCode =  errorResponse.error_description;

            switch(errorCode) {

              case '1':
                this.errors.add('globalError', 'Invalid Email/Password');
                break;
              case '2':
                this.errors.add('globalError', 'Please verify your email.');
                break;
              case '6':
                this.errors.add('globalError', 'Please activate the link that was sent to your email');
                this.resendEmail = true;
                break;
              default:
                this.errors.add('globalError', 'Internal server error');
                break;

            }

          });

        });

      },

      performSignin() {

        var request = queryStringBuilder({
          grant_type: 'password',
          username: this.user.email,
          password: this.user.password
        });

        return this.$http.post('Token', request, {
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            }
        });

      }

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
