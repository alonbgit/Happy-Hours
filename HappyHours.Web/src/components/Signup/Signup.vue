
<template>
  <div>
    <div class="form-container">

      <div class="form-header">
        <span>Signup</span>
      </div>

      <div class="form-content">

        <div v-show="currentStep == 1">
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
          <p>An email with activation link was sent to your email - {{ user.email }}</p>
        </div>

        <div class="form-item">
          <button class="btn" @click.prevent="onPrevStep" v-show="currentStep == 2">Previous</button>
          <button class="btn btn-right" @click.prevent="onNextStep">Next</button>
        </div>

      </div>
    </div>

  </div>
</template>

<script>

  import { ApiCredentials } from '../../ApiData';

  export default {

    data() {
      return {
        currentStep: 1,
        user: {
          email: '',
          password: '',
          confirmPassword: '',
          systemEmail: '',
          systemPassword: '',
          systemNumber: ''
        }
      }
    },

    methods: {

      onNextStep() {

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
          this.markFieldsAsTouched(['email', 'password', 'confirmPassword']);

          if (!isValid)
            return;

          // in case step1 sync validations passed, we now check if the email already exist
          // using http ajax request
          this.isEmailExist().then(({data}) => {
            if (data.Exist)
              this.errors.add('email', 'email already exist');
            else
              this.currentStep++;
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

          // in case step1 sync validations passed, we now check if the TA credentials
          // correct
          this.isTACredentialsExist().then(({data}) => {
            if (data.Exist)
              this.errors.add('email', 'email already exist');
            else
              this.currentStep++;
          });

        });

      },

      markFieldsAsTouched(fields) {

        fields.map((field) => {
          this.$validator.flag(field, {
            touched: true
          });
        });

      },

      isTACredentialsExist() {

        return this.$http.post('http://HappyHours.Web/api/Signup/CheckTACredentials', {
          taEmail: this.user.taEmail,
          taPassword: this.user.taPassword,
          taNumber: this.user.taNumber,
          credentials: ApiCredentials
        });

      },

      isEmailExist() {

        return this.$http.post('http://HappyHours.Web/api/Signup/CheckEmailExist', {
          email: this.user.email,
          credentials: ApiCredentials
        });

      },

      onPrevStep() {
        this.currentStep--;
      }

    }

}



</script>

<style scoped>

</style>
