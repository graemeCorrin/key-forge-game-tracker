let backgroundColor = [
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)'
],
    borderColor = [
        'rgba(255, 99, 132, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)'
    ],
    borderWidth = 1;


let barChartElem = document.getElementById('barChart').getContext('2d');
let lineChartElem = document.getElementById('lineChart').getContext('2d');


function populateBarChart(elem, result) {
    let barChart = new Chart(elem, {
        type: 'bar',
        data: {
            labels: result.labels,
            datasets: [{
                label: result.dataSets[0].label,
                data: result.dataSets[0].data,
                backgroundColor: result.dataSets[0].backgroundColor,
                borderColor: result.dataSets[0].borderColor,
                borderWidth: result.dataSets[0].borderWidth
            }]
        },
        options: { scales: { yAxes: [{ ticks: { beginAtZero: true } }] } }
    });
}


$(document).ready(function () {
    console.log("ready!");

    $.ajax({
        url: "/Home/GetPlayerWins", success: function (result) {
            populateBarChart(barChartElem, result);
        }
    });

    $.ajax({
        url: "/Home/GetDeckWins", success: function (result) {
            populateBarChart(lineChartElem, result);
        }
    });

});