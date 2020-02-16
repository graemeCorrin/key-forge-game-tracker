using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KeyForgeGameTracker.Models;
using Microsoft.AspNetCore.Authorization;
using KeyForgeGameTracker.ViewModels;
using System.Collections.Generic;
using KeyForgeGameTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KeyForgeGameTracker.Areas.Identity.Models;
using System.Linq;

namespace KeyForgeGameTracker.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly KeyForgeContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(KeyForgeContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<JsonResult> GetPlayerWins()
        {
            var players = await _context.Users.ToListAsync();

            var data = new List<int>();

            foreach (AppUser player in players)
            {
                data.Add(await _context.Game.Where(x => x.WinningPlayerId == player.Id).CountAsync());
            }

            var barCharViewModel = new BarChartViewModel()
            {
                Labels = players.Select(x => x.FullName).ToList(),
                DataSets = new List<DataSet>()
                {
                    new DataSet()
                    {
                        Label = "Wins",
                        Data = data,
                        BackgroundColor = new List<string>()
                        {
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(54, 162, 235, 0.2)",
                            "rgba(255, 206, 86, 0.2)",
                            "rgba(75, 192, 192, 0.2)",
                            "rgba(153, 102, 255, 0.2)",
                            "rgba(255, 159, 64, 0.2)"
                        },
                        BorderColor = new List<string>()
                        {
                            "rgba(255, 99, 132, 1)",
                            "rgba(54, 162, 235, 1)",
                            "rgba(255, 206, 86, 1)",
                            "rgba(75, 192, 192, 1)",
                            "rgba(153, 102, 255, 1)",
                            "rgba(255, 159, 64, 1)"
                        },
                        BorderWidth = 1
                    }
                }
            };


            return Json(barCharViewModel);

        }


        public async Task<JsonResult> GetDeckWins()
        {
            var decks = await _context.Deck.ToListAsync();

            var data = new List<int>();

            foreach (Deck deck in decks)
            {
                data.Add(await _context.Game.Where(x => x.WinningDeckId == deck.Id).CountAsync());
            }

            var barCharViewModel = new BarChartViewModel()
            {
                Labels = decks.Select(x => x.Name).ToList(),
                DataSets = new List<DataSet>()
                {
                    new DataSet()
                    {
                        Label = "Wins",
                        Data = data,
                        BackgroundColor = new List<string>()
                        {
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(54, 162, 235, 0.2)",
                            "rgba(255, 206, 86, 0.2)",
                            "rgba(75, 192, 192, 0.2)",
                            "rgba(153, 102, 255, 0.2)",
                            "rgba(255, 159, 64, 0.2)"
                        },
                        BorderColor = new List<string>()
                        {
                            "rgba(255, 99, 132, 1)",
                            "rgba(54, 162, 235, 1)",
                            "rgba(255, 206, 86, 1)",
                            "rgba(75, 192, 192, 1)",
                            "rgba(153, 102, 255, 1)",
                            "rgba(255, 159, 64, 1)"
                        },
                        BorderWidth = 1
                    }
                }
            };


            return Json(barCharViewModel);

        }
    }
}
