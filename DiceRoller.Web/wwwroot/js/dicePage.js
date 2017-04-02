var dicePage = function () {
    let viewModel = {
        players: ko.observableArray()
    };


    function init(roomId, player) {
        $(document).ready(function () {
            ko.applyBindings(viewModel);

            logToConsole("Connecting...");

            dice.init({
                    playerJoined: newPlayer,
                    playerLeft: playerLeft,
                    playerList: updatedPlayerList,
                    error: errorMessage
                })
                .then(function() {
                    dice.connect({
                        roomId: roomId,
                        player: player
                    });

                    logToConsole("Connected to the room.");
                });
        });
    }


    function newPlayer(player) {
        logToConsole(`New player ${player} joined!`);
        viewModel.players.push(player);
    };

    function playerLeft(player) {
        logToConsole(`Player left the room: ${player}`);
        viewModel.players.remove(player);
    }

    function updatedPlayerList(players) {
        viewModel.players(players);
    }

    function errorMessage(message) {
        logToConsole(`ERROR: ${message}`);
    }

    function logToConsole(message) {
        var currentMessage = $("#consoleMessages").val();
        $("#consoleMessages").val(currentMessage + message + "\r\n");
    }

    return {
        init: init
    };
}();
