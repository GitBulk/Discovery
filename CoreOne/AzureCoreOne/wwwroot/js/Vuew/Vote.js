﻿new Vue({
    el: '#voteApp',
    data: {
        upvotes: 0
    },
    methods: {
        upvote: function () {
            this.upvotes++;
        }
    }
});