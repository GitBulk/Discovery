var navigationApp = new Vue({
    el: '#main',
    data: {
        active: 'home'
    },
    methods: {
        makeActive: function (item) {
            this.active = item;
        }
    }
});