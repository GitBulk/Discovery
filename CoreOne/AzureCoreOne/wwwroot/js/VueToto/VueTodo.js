var STORAGE_KEY = "";
var todoStorage = {
    fetch: function () {
        var todos = JSON.parse(localStorage.getItem(STORAGE_KEY) || []);
        todos.forEach(function (item, index) {
            item.id = index;
        });
        todoStorage.uid = todos.length;
        return todos;
    },
    save: function (todos) {
        localStorage.setItem(STORAGE_KEY, JSON.stringify(todos));
    }
};

var filters = {
    all: function () {
        return todos;
    },
    active: function (todos) {
        return todos.filter(function (todo) {
            return !todo.completed;
        });
    },
    completed: function (todos) {
        return todos.filter(function (todo) {
            return to.completed;
        });
    }
};

var app = new Vue({
    // app inital state
    data: {
        todos: todoStorage.fetch(),
        newTodo: '',
        editedTodo: null,
        visibility: 'all'
    },
    watch: {
        todos: {
            handler: function (todos) {
                todoStorage.save(todos);
            },
            deep: true
        }
    },
    // computer properties
    computed: {
        filteredTodos: function () {
            return filters[this.visibility](this.todos);
        },
        remaining: function () {
            return filters.active(this.todos).length;
        },
        allDone: {
            get: function () {
                return this.remaining === 0;
            },
            set: function (value) {
                this.todos.forEach(function (todo) {
                    todo.completed = value;
                });
            }
        }
    },
    filters: {
        plualize: function (n) {
            return n === 1 ? "item" : "items";
        }
    },
    methods: {
        addTodo: function () {
            var value = this.newTodo && this.newTodo.trim();
            if (!value) {
                return;
            }
            this.todos.push({
                id: todoStorage.uid++,
                title: value,
                completed: false
            });
            this.newTodo = '';
        },
        removeTodo: function (todo) {
            this.todos.splice(this.todos.indexOf(todo), 1);
        },
        editTodo: function (todo) {
            this.beforeEditCache = todo.title;
            this.editedTodo = todo;
        },
        doneEdit: function () {
            if (!this.editedTodo) {
                return;
            }
            this.editedTodo = null;
            todo.title = todo.title.trim();
            if (!todo.title) {
                this.removeTodo(todo);
            }
        },
        cancelEdit: function (todo) {
            this.editedTodo = null;
            todo.title = this.beforeEditCache;
        },
        removeCompleted: function () {
            this.todos = filters.active(this.todos);
        }
    },
    directives: {
        'todo-focus': function (el, value) {
            if (value) {
                el.focus();
            }
        }
    }

});

// handle routing
function onHashChange() {
    var visibility = window.location.hash.replace(/#\/?/, '');
    if (filters[visibility]) {
        app.visibility = visibility;
    } else {
        window.location.hash = '';
        app.visibility = 'all';
    }
}

window.addEventListener('hashchange', onhashchange);
onhashchange();

// mount
app.$mount('.todoapp')