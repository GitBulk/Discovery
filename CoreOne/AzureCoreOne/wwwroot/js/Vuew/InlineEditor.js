var inlineEditorApp = new Vue({
    el: '#main',
    data: {
        showTooltip: false,
        textContent: 'Edit me'
    },
    methods: {
        hideTooltip: function () {
            this.showTooltip = false;
        },
        toggleTootip: function () {
            this.showTooltip = !this.showTooltip;
        }
    }
});