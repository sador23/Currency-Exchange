$(document).ready(function () {

    $("#Submit").on("click", function () {
        let From = $('#From option:selected').val();
        let To = $('#To option:selected').val();
        let curr = $('#Change').val();
        let result = $("#result");
        result.html("");
        //curr.replace(/./g, ",");

        $.ajax({
            url: "/Home/CalculateExchange",
            type: "POST",
            contentType: "application/json",
            async: false,
            data: JSON.stringify({
                "From": From, "To": To, "Value": curr
            }),
            success: function (data) {
                result.append(`${curr} ${From} is ${data} ${To}`);
            },
            error: function (ex) {
                console.log(ex);
            }
        })
    });
});