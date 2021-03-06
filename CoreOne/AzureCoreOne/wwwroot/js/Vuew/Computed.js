﻿new Vue({
    el: '#computedApp',
    data: {
        num1: 1,
        num2: 2,
        operator: "+"
    },
    computed: {
        result: function () {
            switch (this.operator) {
                case "+":
                    return this.num1 + this.num2;
                case "-":
                    return this.num1 - this.num2;
                case "*":
                    return this.num1 * this.num2;
                case "/":
                    return this.num1 / this.num2;
            }
        }
    }
});