using System.Diagnostics;
using RunGroopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.ViewModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace RunGroopWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IClubRepository _clubRepository;

    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
    {
        _logger = logger;
        _clubRepository = clubRepository;
    }

    public async Task<IActionResult> Index()
    {
        var homeViewModel = new HomeViewModel();
        try
        {
            string url = "https://ipinfo.io?token=b5caef588e9d5c";
            var info = new WebClient().DownloadString(url);
            var ipInfo = new IPInfo();
            ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
            RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;
            homeViewModel.City = ipInfo.City;
            homeViewModel.State = ipInfo.Region;
            if (homeViewModel.City != null)
            {
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
            }
            else
            {
                homeViewModel.Clubs = null;
            }

            return View(homeViewModel);
        }
        catch
        {
            homeViewModel.Clubs = null;
        }

        return View(homeViewModel);
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