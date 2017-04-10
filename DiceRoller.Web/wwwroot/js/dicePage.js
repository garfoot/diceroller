var dicePage = function () {
    let viewModel = {
        players: ko.observableArray(),
        availableDice: ko.observableArray(),
        selectedDice: ko.observableArray(),
        mySelectedDice: ko.observableArray(),
        rollResults: ko.observableArray(),
        playerRolling: ko.observable(),
        dieClicked: dieClicked,
        dieRemoveClicked: dieRemoveClicked,
        dieRollClicked: dieRollClicked,
        me: ko.observable(),
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
                    error: errorMessage,
                    rolledDice: rolledDice,
                })
                .then(function() {
                    dice.connect({
                        roomId: roomId,
                        player: player
                    });

                    dice.getDiceList();
                    viewModel.me = player;
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
        if (player === viewModel.me) {
            viewModel.mySelectedDice(dice);
        }
    }

    function rolledDice(player, results) {
        ///logToConsole(`player ${player}: rolled ${results}`);
        viewModel.rollResults.push(`player ${player}: rolled ${results}`);
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

    function dieRollClicked(data, evt) {
        dice.rollDice(data);
    }


    function logToConsole(message) {
        var currentMessage = $("#consoleMessages").val();
        $("#consoleMessages").val(currentMessage + message + "\r\n");
    }

    return {
        init
    };
}();
