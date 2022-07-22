const CustomTooltip = (tooltipItems) => {
    return `${tooltipItems.raw[0]} ${tooltipItems.raw[1]}`;
}

const RandomRgbValue = () => {
    return Math.ceil(Math.random() * 255);
}

const CAR_BRANDS = $(".lorryBrand");
const data = {
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
    datasets: [
        {
            label: 'Trips schedule',
            data: [
                ['2022-02-01', '2022-02-03', '2022-02-05', '2022-02-06'],
                ['2022-02-03', '2022-02-05'],
                ['2022-02-07', '2022-02-09'],
                ['2022-02-10', '2022-02-11'],
                ['2022-02-11', '2022-02-14'],
                ['2022-02-14', '2022-02-19'],
                ['2022-02-19', '2022-02-27']
            ],
            backgroundColor: [
                'rgba(255, 26, 104, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(0, 0, 0, 0.2)'
            ],
            borderColor: [
                'rgba(255, 26, 104, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
                'rgba(0, 0, 0, 1)'
            ],
            borderWidth: 1,
            barPercentage: 0.9,
        }]
};

const config = {
    type: 'bar',
    data,
    options: {
        indexAxis: 'y',
        scales: {
            x: {
                min: '2022-02-01',
                type: 'time',
                time: {
                    unit: 'day'
                }
            },
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            tooltip: {
                callbacks: {
                    label: CustomTooltip,
                }
            }
        }
    }
};

const myChart = new Chart(document.getElementById('Chart'), config);