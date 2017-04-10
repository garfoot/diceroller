var dice = (function () {
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

    diceHub.client.updateDiceList = function(dice) {
        callerOptions.diceListUpdated && callerOptions.diceListUpdated(dice);
    }

    diceHub.client.selectedDice = function(player, dice) {
        callerOptions.selectedDiceUpdated && callerOptions.selectedDiceUpdated(player, dice);
    }

    diceHub.client.rolledDice = function (player, dice) {
        callerOptions.rolledDice && callerOptions.rolledDice(player, dice);
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

    function connect(options) {
        diceHub.server.connect(options.roomId, options.player);
    }

    function getDiceList() {
        diceHub.server.getDiceList();
    }

    function addDie(die) {
        diceHub.server.addDie(die);
    }

    function removeDie(die) {
        diceHub.server.removeDie(die);
    }
    function rollDice(die) {
        diceHub.server.rollDice();
    }


    return {
        init: init,
        connect: connect,
        getDiceList: getDiceList,
        addDie: addDie,
        removeDie: removeDie,
        rollDice: rollDice
    };
})();

