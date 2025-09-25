function createPieChart(selector, series, labels) {
    var options = {
        series: series,
        chart: {
            fontFamily: "inherit",
            width: 380,
            type: "pie",
        },
        colors: [
            "var(--bs-primary)",
            "var(--bs-secondary)",
            "#ffae1f",
            "#fa896b",
            "#39b69a",
        ],
        labels: labels,
        responsive: [
            {
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200,
                    },
                    legend: {
                        position: "bottom",
                    },
                },
            },
        ],
        legend: {
            labels: {
                colors: ["#a1aab2"],
            },
        },
    };

    var chart = new ApexCharts($(selector)[0], options);
    chart.render();
    return chart;
}