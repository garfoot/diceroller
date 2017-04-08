var dicePage = function () {
    let viewModel = {
        players: ko.observableArray(),
        availableDice: ko.observableArray(),
        selectedDice: ko.observableArray(),
        playerRolling: ko.observable(),
        dieClicked: dieClicked,
        dieRemoveClicked: dieRemoveClicked
};


    function init(roomId, player) {
        $(document).ready(function () {
            ko.applyBindings(viewModel);

            logToConsole("Connecting...");

            dice.init({
                    playerJoined: newPlayer,
                    playerLeft: playerLeft,
                    playerList: updatedPlayerList,
                    diceListUpdated: diceListUpdated,
                    selectedDiceUpdated: selectedDiceUpdated,
                    error: errorMessage
                })
                .then(function() {
                    dice.connect({
                        roomId: roomId,
                        player: player
                    });

                    dice.getDiceList();

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

    function diceListUpdated(dice) {
        viewModel.availableDice(dice);
    }

    function selectedDiceUpdated(player, dice) {
        viewModel.selectedDice(dice);
        viewModel.playerRolling(player);
    }

    function errorMessage(message) {
        logToConsole(`ERROR: ${message}`);
    }

    function dieClicked(data, evt) {
        dice.addDie(data);
    }

    function dieRemoveClicked(data, evt) {
        dice.removeDie(data);
    }

    function logToConsole(message) {
        var currentMessage = $("#consoleMessages").val();
        $("#consoleMessages").val(currentMessage + message + "\r\n");
    }

    return {
        init
    };
}();
