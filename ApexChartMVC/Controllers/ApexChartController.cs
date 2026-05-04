using ApexChartMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApexChartMVC.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ChartModel chartModel = new ChartModel();
            chartModel.Series = [44, 55, 13, 43, 22];
            chartModel.Labels = ["Team Alpha", "Team Beta", "Team Cemax", "Team Delta", "Team Elion"];
            return View(chartModel);
        }

        public IActionResult LineChart()
        {
            ChartModel chartModel = new ChartModel();

            chartModel.Labels = new[]
            {
        "Jan","Feb","Mar","Apr","May","Jun",
        "Jul","Aug","Sep","Oct","Nov","Dec"
    };

            chartModel.Series = new object[]
            {
        new {
            name = "Reggane",
            data = new double[]
            {
                16.0,18.2,23.1,27.9,32.2,36.4,
                39.8,38.4,35.5,29.2,22.0,17.8
            }
        },
        new {
            name = "Tallinn",
            data = new double[]
            {
                -2.9,-3.6,-0.6,4.8,10.2,14.5,
                17.6,16.5,12.0,6.5,2.0,-0.9
            }
        }
            };

            return View(chartModel);
        }
    }
}
