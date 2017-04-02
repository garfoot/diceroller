var dice = function() {
    let diceHub = $.connection.diceHub;
    let callerOptions;


    diceHub.client.playerJoined = function(player) {
        callerOptions.playerJoined && callerOptions.playerJoined(player);
    };

    diceHub.client.playerList = function(players) {
        callerOptions.playerList && callerOptions.playerList(players);
    }

    diceHub.client.error = function(message) {
        callerOptions.error && callerOptions.error(message);
    }

    diceHub.client.playerLeft = function(player) {
        callerOptions.playerLeft && callerOptions.playerLeft(player);
    }

    function init(options) {
        callerOptions = options;

        let promise = new Promise(function(fulfill, reject) {
            $.connection.hub.logging = true;
            $.connection.hub.start()
                .done(function() {
                    fulfill();
                });
        });
        
        return promise;
    }

    diceHub.client.connectedToRoom = function (roomName, members) {
        alert(`Connected to room ${roomName} with members ${members}`);
    };


    function connect(options) {
        diceHub.server.connect(options.roomId, options.player);
    }

    return {
        init: init,
        connect: connect
    };
}();

