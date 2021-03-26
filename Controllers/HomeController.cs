using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index(long? teamid, string team, int pagenum = 0)
        {
            int pagesize = 5;
            return View(new IndexViewModel
            {
                Bowlers = context.Bowlers
                    .Where(x => x.TeamId == teamid || teamid == null)
                    .OrderBy(x => x.BowlerLastName)
                    .Skip((pagenum - 1) * pagesize)
                    .Take(pagesize)
                    .ToList(),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pagesize,
                    CurrentPage = pagenum,
                    //get full count if no team selected
                    //otherwise count from team
                    TotalNumItems = teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamid).Count()
                },

                TeamName = team
            }) ;
        }

        private IActionResult View(Func<string, object[], IQueryable<Bowler>> fromSqlRaw)
        {
            throw new NotImplementedException();
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
    }
}
