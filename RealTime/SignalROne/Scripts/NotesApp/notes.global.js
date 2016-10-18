function signalR() { }

signalR.onConnected = 'signalRConnected';

function notesSignalR() { }

// hub
notesSignalR.hubName = 'notesHub';

// client calls
notesSignalR.addNote = 'addNote';
notesSignalR.removeNote = 'removeNote';
notesSignalR.getAllNotes = 'getAllNotes';

// client callbacks
notesSignalR.onNewNote = 'broadcaseNewNote';
notesSignalR.onRemoveNote = 'broadcaseRemoveNote';

