
<template>
  <div>
    <div class="form-container" :class="{ 'disabled': isLoading }">

      <div class="form-header">
        <span>Signup</span>
      </div>

      <div class="form-content">

        <div v-show="currentStep == 1">
          <div class="form-item">
            <label>First Name</label>
            <div class="input-item">
              <input type="text" placeholder="First Name" v-model="user.firstName" name="firstName" v-validate="'required'" data-vv-as="First Name"/>
              <div class="field-error" v-show="errors.has('firstName') && fields.firstName.touched">{{ errors.first('firstName') }}</div>
            </div>
          </div>

          <div class="form-item">
            <label>Last Name</label>
            <div class="input-item">
              <input type="text" placeholder="Last Name" v-model="user.lastName" name="lastName" v-validate="'required'" data-vv-as="Last Name"/>
              <div class="field-error" v-show="errors.has('lastName') && fields.lastName.touched">{{ errors.first('lastName') }}</div>
            </div>
          </div>

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

          <div class="form-item">
            <label>Confirm Password</label>
            <div class="input-item">
              <input type="password" placeholder="Confirm Password" v-model="user.confirmPassword" name="confirmPassword" v-validate="'required'" data-vv-as="Confirm Password"/>
              <div class="field-error" v-show="errors.has('confirmPassword') && fields.confirmPassword.touched">{{ errors.first('confirmPassword') }}</div>
            </div>
          </div>

        </div>

        <div v-if="currentStep == 2">
          <div class="form-item">
            <label>TA Email</label>
            <div class="input-item">
              <input type="text" placeholder="TA Email" v-model="user.systemEmail" name="taEmail" v-validate="'required'" data-vv-as="TA Email"/>
              <div class="field-error" v-show="errors.has('taEmail') && fields.taEmail.touched">{{ errors.first('taEmail') }}</div>
            </div>
          </div>
          <div class="form-item">
            <label>TA Password</label>
            <div class="input-item">
              <input type="password" placeholder="TA Password" v-model="user.systemPassword" name="taPassword" v-validate="'required'" data-vv-as="TA Password"/>
              <div class="field-error" v-show="errors.has('taPassword') && fields.taPassword.touched">{{ errors.first('taPassword') }}</div>
            </div>
          </div>
          <div class="form-item">
            <label>TA Employee Number</label>
            <div class="input-item">
              <input type="text" placeholder="TA Employee Number" v-model="user.systemNumber" name="taNumber" v-validate="'required'" data-vv-as="TA Employee Number"/>
              <div class="field-error" v-show="errors.has('taNumber') && fields.taPassword.touched">{{ errors.first('taNumber') }}</div>
            </div>
          </div>

        </div>

        <div v-if="currentStep == 3">
          <div class="form-item">
            <p>An email with activation link was sent to your email - {{ user.email }}</p>
          </div>

          <div class="form-item">
            <button class="btn btn-right" @click="onSigninClick">Sign in</button>
          </div>
        </div>

        <div v-if="currentStep == 4">
          <div class="form-item">
            <p>Thank you. The registeration process completed successfully</p>
          </div>

          <div class="form-item">
            <button class="btn btn-right" @click="onSigninClick">Sign in</button>
          </div>
        </div>

        <div class="form-item">
          <div class="field-error" v-show="errors.has('globalError')">{{ errors.first('globalError') }}</div>
        </div>

        <div class="form-item">
          <button class="btn" @click.prevent="onPrevStep" v-show="currentStep == 2">Previous</button>
          <button class="btn btn-right" @click.prevent="onNextStep" v-show="currentStep < 3">Next</button>
          <app-loading isLoading="isLoading" v-if="isLoading"/>
        </div>

      </div>
    </div>

  </div>
</template>

<script>

  import { validatorMixin } from '../../mixins/validatorMixins';
  import storageManager from '../../storageManager';
  import Loading from '../Loading.vue';

  export default {

    components: {
      appLoading: Loading
    },

    mixins: [validatorMixin],

    data() {
      return {
        currentStep: 1,
        user: {
          firstName: '',
          lastName: '',
          email: '',
          password: '',
          confirmPassword: '',
          systemEmail: '',
          systemPassword: '',
          systemNumber: ''
        },
        isLoading: false
      }
    },

    methods: {

      onNextStep() {

        this.clearErrors();

        if (this.currentStep == 1) {
          this.handleStep1();
        }
        else if (this.currentStep == 2) {
          this.handleStep2();
        }

      },

      handleStep1() {

        // validates step 1
        this.$validator.validateAll().then((isValid) => {

          // mark the step1 fields as touched, as we want to see the errors
          this.markFieldsAsTouched(['firstName', 'lastName', 'email', 'password', 'confirmPassword']);

          if (!isValid)
            return;

          if (this.user.password != this.user.confirmPassword) {
            this.errors.add('globalError', 'Password and Confirm Password must be the same');
            return;
          }

          this.isLoading = true;

          // in case step1 sync validations passed, we now check if the email already exist
          // using http ajax request
          this.isEmailExist().then(({data}) => {
            this.isLoading = false;
            if (data.Exist)
              this.errors.add('email', 'email already exist');
            else
              this.currentStep++;
          }, (error) => {
            this.isLoading = false;
            console.log(error);
          });

        });

      },

      handleStep2() {

        // validates step 2
        this.$validator.validateAll().then((isValid) => {

          // mark the step1 fields as touched, as we want to see the errors
          this.markFieldsAsTouched(['taEmail', 'taPassword', 'taNumber']);

          if (!isValid)
            return;

          this.isLoading = true;

          // in case step1 sync validations passed, we now check if the TA credentials
          // correct
          this.perfornSignup().then(({data}) => {

            this.isLoading = false;

            if (data.ErrorCode) {
              if (data.ErrorCode == 3)
                this.errors.add('globalError', 'Invalid TA Credentials');
              else if (data.ErrorCode == 2)
                this.errors.add('globalError', 'Email already exist');
              else
                this.errors.add('globalError', 'Internal server error');
            }
            else {
              if (data.IsEmailVerificationRequired)
                this.currentStep++;
              else
                this.currentStep+=2;
            }

          }, (error) => {

            this.isLoading = false;

            let errorResponse = JSON.parse(error.bodyText);
            let errorCode =  errorResponse.error_description;

            switch(errorCode) {
              case '1':
                this.errors.add('globalError', 'Invalid Email/Password');
                break;
              case '2':
                this.errors.add('globalError', 'Please verify your email.');
                break;

            }

          });

        });

      },

      perfornSignup() {

        return this.$http.post('api/Signup', {
          firstName: this.user.firstName,
          lastName: this.user.lastName,
          email: this.user.email,
          password: this.user.password,
          systemEmail: this.user.systemEmail,
          systemPassword: this.user.systemPassword,
          systemNumber: this.user.systemNumber,
          credentials: this.apiCredentials
        });

      },

      isEmailExist() {

        return this.$http.post('api/Signup/CheckEmailExist', {
          email: this.user.email,
          credentials: this.apiCredentials
        });

      },

      onPrevStep() {

        this.clearErrors();

        this.currentStep--;

      },

      onSigninClick() {
        this.$router.push('/Signin');
      }

    }

}



</script>

<style scoped>


</style>
