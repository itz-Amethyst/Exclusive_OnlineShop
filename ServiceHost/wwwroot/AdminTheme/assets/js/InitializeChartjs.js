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
                },
                {
                    label: ["Bar Chart 2"],
                    data: [50, 20, 350],
                    backgroundColor: "#1C3F60",
                    borderColor: "#0B1320"
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