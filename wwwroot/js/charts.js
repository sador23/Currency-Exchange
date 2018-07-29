$(document).ready(function () {

    var myChart = null;

    $("#Filter,#GraphType").on("change", function () {
        let name = $('#Filter option:selected').val();
        LoadChart(name);
    });

    /*
     * LoadChart
     * Will load resource from the server via ajax.
     * Once the data is available, it will can helper methods
     * to parse the response, and eventually will call the method
     * to draw the graph.
     * 
     * <param name="name">Name of the country to fetch rates from</param>
     * */
    var LoadChart = function (name) {
        $.ajax({
            url: "/Home/StatisticData/" +name,
            type: "GET",
            contentType: "application/json",
            async: false,
            success: function (data) {
                var formatted = EvalAjaxResult(data);
                DrawChart(formatted);
            },
            error: function (error) {
                console.log(error);
            }
        });
    };


    /*
     * EvalAjaxResult
     * Shall parse the given data into a new object.
     * Splits the given date into two different arrays,
     * dates and rates. Will also parse the dates into a more
     * readable format.
     * 
     * <param name="data">Response result from the successful ajax call</param>
     * */
    var EvalAjaxResult = function (data) {
        var rates = data.rates;
        var result = {}
        result.label = data.name;
        result.dates = new Array();
        result.rates = new Array();
        for (let i in rates) {
            result.dates.push(i.replace("T00:00:00", ""));
            result.rates.push(data.rates[i]);
        }
        return result;
    }

    /*
     * DrawChart
     * Shall draw the graph with the given inputs as data.
     * Also reads the selected graph type.
     * 
     * <param name="ajaxResult">The formatted ajax response</param>
     * */
    var DrawChart = function (ajaxResult) {
        var colors = GenerateRandomColor(ajaxResult.dates.length);
        let type = $('#GraphType option:selected').val();

        if (myChart !== null) {
            myChart.destroy();
        }

        var ctx = document.getElementById("myChart");
         myChart = new Chart(ctx, {
             type: type.toLowerCase(),
            data: {
                labels: ajaxResult.dates,
                datasets: [{
                    label: ajaxResult.label,
                    data: ajaxResult.rates,
                    backgroundColor: colors.bg,
                    borderColor: colors.border,
                    borderWidth: 1
                }]
            },
             options: {
                 legend: {
                     labels: {
                         fontColor: "orange",
                         fontSize: 15
                     }
                 },
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: "orange",
                            fontSize: 15,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            
                            fontColor: "orange",
                            fontSize: 15
                        }
                    }]
                }
            }
        });
    }

    /*
     * GenerateRandomColor
     * Shall generate random colors for the graph.
     * 
     * <param name="length">The amount of colors needed</param>
     * */
    var GenerateRandomColor = function (length) {
        var result = {};
        result.border = new Array();
        result.bg = new Array();

        for (let j = 0; j < 2; j++) {
            for (let i = 0; i < length; i++) {
                let transp = j === 0 ? "0.2" : "1";
                let rgba = `rgba(${Math.floor(Math.random() * 255)},${Math.floor(Math.random() * 255)},${Math.floor(Math.random() * 255)},${transp})`;
                if (j === 0) result.bg.push(rgba);
                else result.border.push(rgba);
            }
        }
        return result;
    }

    LoadChart("HUF");
});