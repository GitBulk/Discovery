﻿Vue.component('child', {
    props: ['player'],
    template: '<li>{{player.fullName}} - {{player.club}}</li>'
});

var helloApp = new Vue({
    el: '#helloApp',
    data: {
        message: 'You loaded this page on ' + new Date(),
        seen: true,
        footballPlayers: [
            {
                fullName: 'Cris Ronaldo', club: 'Real'
            },
            {
                fullName: 'Messi', club: 'Barca'
            }
        ],
        message: 'toan tran'
    },
    methods: {
        reverseMessage: function () {
            this.message = this.message.split('').reverse().join('');
        }
    }
});

