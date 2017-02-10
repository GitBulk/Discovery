document.getElementById('myForm').addEventListener('submit', saveBookmark);

function saveBookmark(e) {
    var siteName = document.getElementById('siteName').value;
    var siteUrl = document.getElementById('siteUrl').value;

    var bookmark = {
        name: siteName,
        url: siteUrl
    };

    if (localStorage.getItem('bookmarks') === null) {
        // init it
        var bookmarks = [];

    } else {
        // get bookmarks from local stoage
        var bookmarks = JSON.parse(localStorage.getItem('bookmarks'));
    }

    bookmarks.push(bookmark);

    // save to local storage
    localStorage.setItem('bookmarks', JSON.stringify(bookmarks));

    e.preventDefault();
}

function fetchBookmarks() {
    var bookmarks = JSON.parse(localStorage.getItem('bookmarks'));
    var bookmarkResults = document.getElementById('bookmarkResults');

    bookmarkResults.innerHTML = 'Hello';

    for (var i = 0; i < bookmarks.length; i++) {
        var name = bookmarks[i].name;
        var url = bookmarks[i].url;
        bookmarkResults.innerHTML += '<div class="well">' +
                                        '<h3>' + name + '<a class="btn btn-default" targets="_blank" href="' + url + '" />Visit' + '</h3>' +
                                     '</div>';
    }
}