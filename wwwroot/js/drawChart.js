function addDataToDataTable(dataTableObj) {
    const TABLE_DATA = document.querySelectorAll('td');
    for (let i = 0; i < TABLE_DATA.length; i += 7) {
        let lorry = TABLE_DATA[i].innerText;
        let dateOfDepartue = TABLE_DATA[i + 3].innerText;
        let dateOfArrival = TABLE_DATA[i + 4].innerText;
        dataTableObj.addRow([lorry,
            new Date(dateOfDepartue.substr(6, 4), dateOfDepartue.substr(3, 2) - 1, dateOfDepartue.substr(0, 2), dateOfDepartue.substr(11, 2), dateOfDepartue.substr(14, 2)),
            new Date(dateOfArrival.substr(6, 4), dateOfArrival.substr(3, 2) - 1, dateOfArrival.substr(0, 2), dateOfArrival.substr(11, 2), dateOfArrival.substr(14, 2))
            ]);
    }
}

function drawChart() {
    var container = document.getElementById('Chart');
    var chart = new google.visualization.Timeline(container);
    var dataTable = new google.visualization.DataTable();
    dataTable.addColumn({ type: 'string', id: 'Lorry' });
    dataTable.addColumn({ type: 'date', id: 'DateOfDepartue' });
    dataTable.addColumn({ type: 'date', id: 'DateOfArrival' });
    addDataToDataTable(dataTable);
    chart.draw(dataTable);
}

if (document.querySelector('td') != null) {
    google.charts.load("current", { packages: ["timeline"] });
    google.charts.setOnLoadCallback(drawChart);
} 