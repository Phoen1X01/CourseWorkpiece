var pieChartValues = [{
    y: 25,
    exploded: true,
    indexLabel: "",
    color: "#1f77b4"
}, {
    y: 25,
    indexLabel: "",
    color: "#ff7f0e"
}, {
    y: 25,
    indexLabel: "",
    color: " #ffbb78"
}, {
    y: 25,
    indexLabel: "",
    color: "#d62728"
},];
renderPieChart(pieChartValues);

function renderPieChart(values) {

    var chart = new CanvasJS.Chart("pieChart", {
        backgroundColor: "",
        colorSet: "colorSet2",

        title: {
            fontFamily: "Verdana",
            fontSize: 25,
            fontWeight: "normal",
        },
        animationEnabled: true,
        data: [{
            indexLabelFontSize: 15,
            indexLabelFontFamily: "Monospace",
            indexLabelFontColor: "darkgrey",
            indexLabelLineColor: "darkgrey",
            indexLabelPlacement: "outside",
            type: "pie",
            showInLegend: false,
            toolTipContent: "<strong>#percent%</strong>",
            dataPoints: values
        }]
    });
    chart.render();
}
var columnChartValues = [{
    y: 25,
    label: "1",
    color: "#1f77b4"
}, {
    y: 12,
    label: "2",
    color: "#ff7f0e"
}, {
    y: 16,
    label: "3",
    color: " #ffbb78"
}, {
    y: 3,
    label: "4",
    color: "#d62728"
}, {
    y: 43,
    label: "5",
    color: "#98df8a"
}, {
    y: 33,
    label: "6",
    color: "#bcbd22"
}, {
    y: 21,
    label: "7",
    color: "#f7b6d2"
}, {
    y: 16,
    label: "8",
    color: "#8AD9DF"
}, {
    y: 51,
    label: "9",
    color: "#A98ADF"
}, {
    y: 5,
    label: "10",
    color: "#DF8A93"
}, {
    y: 8,
    label: "11",
    color: "#DF9F8A"
}, {
    y: 12,
    label: "12",
    color: "#DFD78A"
}, {
    y: 7,
    label: "13",
    color: "#DF8A8A"
}, {
    y: 3,
    label: "14",
    color: "#6656DF"
}];
renderColumnChart(columnChartValues);

function renderColumnChart(values) {

    var chart = new CanvasJS.Chart("columnChart", {
        backgroundColor: "white",
        colorSet: "colorSet3",
        title: {
            fontFamily: "Verdana",
            fontSize: 25,
            fontWeight: "normal",
        },
        animationEnabled: true,
        legend: {
            verticalAlign: "bottom",
            horizontalAlign: "center"
        },
        theme: "theme2",
        data: [

            {
                indexLabelFontSize: 15,
                indexLabelFontFamily: "Monospace",
                indexLabelFontColor: "darkgrey",
                indexLabelLineColor: "darkgrey",
                indexLabelPlacement: "outside",
                type: "column",
                showInLegend: false,
                legendMarkerColor: "grey",
                dataPoints: values
            }
        ]
    });

    chart.render();
}