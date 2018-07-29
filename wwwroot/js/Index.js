$(document).ready(function () {

    /*
     * Shall read the inputs, and send it to the server
     * via ajax call. After the response is available,
     * it shall display the result on the page.
     * 
     * */
    $("#Submit").on("click", function () {
        let From = $('#From option:selected').val();
        let To = $('#To option:selected').val();
        let curr = $('#Change').val();
        let result = $("#result");
        result.html("");

        $.ajax({
            url: "/Home/CalculateExchange",
            type: "POST",
            contentType: "application/json",
            async: false,
            data: JSON.stringify({
                "From": From, "To": To, "Value": curr
            }),
            success: function (data) {
                result.append(`<h2 class="result">${curr} ${From} is ${data} ${To}<h2>`);
            },
            error: function (ex) {
                console.log(ex);
            }
        })
    });
});