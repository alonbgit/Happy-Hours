
export const validatorMixin = {

  methods: {

    markFieldsAsTouched(fields) {

      fields.map((field) => {
        this.$validator.flag(field, {
          touched: true
        });
      });

    },

    clearErrors() {
      this.errors.clear();
    }

  }

};
