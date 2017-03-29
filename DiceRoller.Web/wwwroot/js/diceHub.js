var dice = $.connection.diceHub;


dice.client.connectedToRoom = function(roomName, members) {
    alert(`Connected to room ${roomName} with members ${members}`);
};


$.connection.hub.logging = true;
$.connection.hub.start().done(function() {
    dice.server.connect("test room");
});