$(document).ready(function () {

    var myChart = null;

    $("#Filter,#GraphType").on("change", function () {
        let name = $('#Filter option:selected').val();
        LoadChart(name);

    });

    var LoadChart = function (name) {
        $.ajax({
            url: "/Home/StatisticData/" +name,
            type: "GET",
            contentType: "application/json",
            async: true,
            success: function (data) {
                var formatted = EvalAjaxResult(data.result);
                DrawChart(formatted);
            },
            error: function (error) {
                console.log(error);
            }
        });
    };

    var EvalAjaxResult = function(data){
        var result = {}
        result.dates = new Array();
        result.rates = new Array();
        for (let i in data.rates) {
            result.dates.push(i);
            result.rates.push(data.rates[i]);
        }
        return result;
    }

    var DrawChart = function (ajaxResult) {
        var colors = GenerateRandomColor(ajaxResult.dates.length);
        let type = $('#GraphType option:selected').val().toLowerCase();

        if (myChart != null) {
            myChart.destroy();
        }

        var ctx = document.getElementById("myChart");
         myChart = new Chart(ctx, {
            type: type,
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
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    var GenerateRandomColor = function (length) {
        var result = {};
        result.border = new Array();
        result.bg = new Array();

        for (let j = 0; j < 2; j++) {
            for (let i = 0; i < length; i++) {
                let transp = j == 0 ? "0.2" : "1";
                let rgba = `rgba(${Math.floor(Math.random() * 255)},${Math.floor(Math.random() * 255)},${Math.floor(Math.random() * 255)},${transp})`;
                if (j === 0) result.bg.push(rgba);
                else result.border.push(rgba);
            }
        }
        return result;
    }

    LoadChart("HUF");
});