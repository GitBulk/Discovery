﻿new Vue({
    el: '#calculatorApp',
    data: {
        num1: 1,
        num2: 2,
        result: null,
        operator: "+"
    },
    methods: {
        calculate: function (e) {
            e.preventDefault();
            switch (this.operator) {
                case "+":
                    this.result = this.num1 + this.num2;
                    break;
                case "-":
                    this.result = this.num1 - this.num2;
                    break;
                case "*":
                    this.result = this.num1 * this.num2;
                    break;
                case "/":
                    this.result = this.num1 / this.num2;
                    break;
            }
        }
    }
});