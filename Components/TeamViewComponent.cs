﻿using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Views.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public TeamViewComponent(BowlingLeagueContext con)
        {
            context = con;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["Team"];
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
