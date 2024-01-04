using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Repository;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsers();
        var usersViewModel = new List<UserViewModel>();

        foreach (var user in users)
        {
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl
            };
            usersViewModel.Add(userViewModel);
        }

        return View(usersViewModel);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var user = await _userRepository.GetUserById(id);

        var userDetailViewModel = new UserDetailViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            Pace = user.Pace,
            Mileage = user.Mileage,
        };

        return View(userDetailViewModel);
    }
}