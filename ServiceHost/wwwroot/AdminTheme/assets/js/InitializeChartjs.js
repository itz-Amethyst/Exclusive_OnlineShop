const barChart = document.getElementById("barChart");

const barChartElement = new Chart(barChart,
    {
        type: 'bar',
        data: {
            labels: ['df', 'dsf', 'f'],
            datasets: [
                {
                    label: ["Bar Chart"],
                    data: [100, 200, 300],
                    backgroundColor: "#00BEC5",
                    borderColor: "#050833"
                }
            ]
        },
        options: {
            elements: {
                bar: {
                    borderWidth: 2
                }
            }
        }

    });