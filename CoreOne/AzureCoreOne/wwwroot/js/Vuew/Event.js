﻿var eventApp = new Vue({
    el: '#events',
    data: {
        event: {
            name: 'toan',
            description: 'description',
            date: null
        }
    },
    methods: {
        addEvent: function () {
            alert(this.event.date);
        }
    }
});