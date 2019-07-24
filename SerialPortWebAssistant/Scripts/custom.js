function clearReceivedText() {
    $('#Receive').val("");
}

function clearSentText() {
    $('#Send').val("");
}

function getModel() {
    return {
        'SelectSerialPort': $('#SelectSerialPort').val(),
        'SelectBaudRate': $('#SelectBaudRate').val(),
        'SelectDataBits': $('#SelectDataBits').val(),
        'SelectParity': $('#SelectParity').val(),
        'SelectStopBits': $('#SelectStopBits').val(),
        'Receive': $('#Receive').val(),
        'Send': $('#Send').val()
    };
}

function updateState(opened) {
    $('#SelectSerialPort').attr('disabled', opened);
    $('#SelectBaudRate').attr('disabled', opened);
    $('#SelectDataBits').attr('disabled', opened);
    $('#SelectParity').attr('disabled', opened);
    $('#SelectStopBits').attr('disabled', opened);
    $('#quicksend').find('input').each(function () { $(this).attr('disabled', !opened) });
    $('#send').attr('disabled', !opened);

    if (opened) {
        $('#open').hide();
        $('#close').show();
    } else {
        $('#open').show();
        $('#close').hide();
    }
}

$(function () {
    // Reference the auto-generated proxy for the hub.
    var serialPort = $.connection.serialPortHub;
    // Create a function that the hub can call back to display messages.
    serialPort.client.receivedMessage = function (name, message) {
        // Add the message to the page.
        if ($('#SelectSerialPort').val() === name) {
            var msg = $('#Receive').val();
            $('#Receive').val(msg + message);
        }
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        //$('#Receive').val("connected.");
        $('#open').click(function () {
            serialPort.server.open(getModel()).done(function (opened) {
                if (!opened) {
                    window.alert(errorMessage.cantOpenSerialPort);
                }
                updateState(opened);
            });
        });
        $('#close').click(function () {
            serialPort.server.close(getModel()).done(function (opened) {
                if (opened) {
                    window.alert(errorMessage.cantCloseSerialPort);
                }
                updateState(opened);
            });
        });
        $('#send').click(function () {
            serialPort.server.sendMessage(getModel());
        });
        $('#quicksend').find('input').each(function () {
            $(this).click(function () {
                $('#Send').val($(this).attr('title'));
                $('#send').click();
            })
        });
    });
});