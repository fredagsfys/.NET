var activity;

// Hämtar alla följare som man har
Start = function (id) {

    activity = $.connection.athleteHub;




    // Visar hur meddelandet ska se ut
    activity.client.addNewMessageToPage = function (name, message) {
        $('#messages').prepend('<ul><li>' + name + " säger: " + message + '</li></ul>'); // when the Hub calls this function it appends a new li item with the text 

    };

    activity.client.message = function (data) {
        $('#messages').prepend(data); // when the Hub calls this function it appends a new li item with the text 


    };
}

// Hämtar alla följare som man har
GetFollowers = function (array, id) {

    $.connection.hub.start(function () {
        for (var y = 0; y < array.length; y++) {

            activity.server.startFollowAthlete(array[y].Id);

        }
        activity.server.startFollowAthlete(id);
    });

};
// Skickar meddelande 
SendSomeMessage = function (id) {

    $('#button').click(function () {
        activity.server.send(id, $('#msg').val());
    });
}

SendResult = function (data) {

    activity = $.connection.athleteHub;

    $.connection.hub.start(function () {
        $('#ReportWod').click(function () {

            data.result = $('#Result_Value').val();
            activity.server.sendWodResult(data);
        });
    });

}












