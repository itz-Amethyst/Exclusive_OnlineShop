using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    [PermissionChecker(Roles.Administrator)]
    public class IndexModel : PageModel
    {
        public List<DashboardChart> DashboardCharts { get; set; }
        public DashboardChart PieChart { get; set; }

        public void OnGet()
        {
            DashboardCharts = new List<DashboardChart>()
            {
                new DashboardChart
                {
                    Label = "Apple",
                    Data = new List<int> { 100, 200, 250, 170, 50 },
                    BackgroundColor = new[] { "#ffcdb2" },
                    BorderColor = "#b5838d"
                },
                new DashboardChart
                {
                    Label = "Samsung",
                    Data = new List<int> { 200, 300, 350, 270, 100 },
                    BackgroundColor = new[] { "#ffc8dd" },
                    BorderColor = "#ffafcc"
                },
                new DashboardChart
                {
                    Label = "Total",
                    Data = new List<int> { 300, 500, 600, 440, 150 },
                    BackgroundColor = new[] { "#0077b6" },
                    BorderColor = "#023e8a",
                },
            };

            PieChart = new DashboardChart
            {
                Label = "Test",
                Data = new List<int>() { 100, 200, 300, 70 },
                BackgroundColor = new []{"#0F1300", "#B4F8C8", "#F24E70", "#07D2BE"},
                BorderColor = "#00BEC5",
                HoverOffset = 11
            };
        }
    }

    public class DashboardChart
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<int> Data { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string[] BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "borderColor")]
        public string BorderColor { get; set; }

        [JsonProperty(PropertyName = "hoverOffset")]
        public int HoverOffset { get; set; }
    }
}
