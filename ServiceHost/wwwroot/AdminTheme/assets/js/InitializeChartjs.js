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

const pieChart = document.getElementById("pieChart");

const pieChartElement = new Chart(pieChart,
    {
        type: 'pie',
        data: {
            labels: ['df', 'dsf', 'f' , "fdfd"],
            datasets: [
                {
                    label: ["Pie Chart"],
                    data: [100, 200, 300 , 70],
                    borderColor: "#00BEC5",
                    backgroundColor: ["#0F1300", "#B4F8C8", "#F24E70", "#07D2BE"],
                    hoverOffset: 11,
                }
            ]
        },
        options: {
            elements: {
                bar: {
                    borderWidth: 1
                }
            }
        }

    });