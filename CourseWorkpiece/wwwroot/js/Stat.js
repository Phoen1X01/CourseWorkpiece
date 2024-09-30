
var pieChartValues = [];

const columnChartColor = [
    "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd", 
    "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf",
    "#aec7e8", "#ffbb78", "#98df8a", "#ff9896", "#c5b0d5", 
    "#c49c94", "#f7b6d2", "#c7c7c7", "#dbdb8d", "#9edae5",
    "#393b79", "#637939", "#8c6d31", "#843c39", "#7b4173"
];
var columnChartValues = [];

function dataUpdeit(){
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
}

function setDataKrug(data){
    let traffics = data["traffics"];
    pieChartValues = [{
        y: traffics["Missing"],
        exploded: true,
        indexLabel: "",
        color: "#1f77b4"
    }, {
        y: traffics["ThosePresent"],
        indexLabel: "",
        color: "#ff7f0e"
    }, {
        y: traffics["Sick"],
        indexLabel: "",
        color: " #ffbb78"
    }, {
        y: traffics["Respectful"],
        indexLabel: "",
        color: "#d62728"
    },]
}

function setDataStolbic(data){
    let students = data["students"]
    const olStudent = document.getElementById("ol-student");
    olStudent.innerHTML = '';
    columnChartValues = [];

    Object.keys(students).forEach((key, index) => {
        console.log(`Индекс: ${index}, Значение Missing: ${students[key]["traffics"]["Missing"]}`);
        olStudent.insertAdjacentHTML('beforeend', `<li>${students[key]["student"]["firstName"]} ${students[key]["student"]["lastName"]} ${students[key]["student"]["middleName"]}</li>`); // Добавляем новый элемент

        columnChartValues.push({
            y: students[key]["traffics"]["Missing"], 
            label: `${index+1}`,
            color: columnChartColor[index%columnChartColor.length]
        });
    });


}

async function setData(){
    const response = await fetch('/Stat/stats');
    if (!response.ok) {
      throw new Error(`Ошибка: ${response.status}`);
    }
    const data = await response.json(); 
    console.log(data);

    setDataKrug(data);
    setDataStolbic(data);
}


async function main(){
    dataUpdeit();
    await setData();
    dataUpdeit();
}


main();