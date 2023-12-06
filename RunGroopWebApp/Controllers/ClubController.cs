﻿using RunGroopWebApp.Models;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data.Enum;

namespace RunGroopWebApp.Controllers;

public class ClubController : Controller
{
    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;
    public ClubController(IClubRepository clubRepository, IPhotoService photoService)
    {
        _clubRepository = clubRepository;
        _photoService = photoService;
    }

    public async Task<IActionResult> Index()
    {
        var clubs = await _clubRepository.GetAll();
        return View(clubs);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        return View(club);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClubViewModel clubVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoService.AddPhotoAsync(clubVM.Image);

            if (result.Error == null)
            {
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };

                _clubRepository.Add(club);

                return RedirectToAction("Index");
            }
        }

        ModelState.AddModelError("Image", "Photo upload failed");

        return View(clubVM);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);

        if (club == null)
        {
            return View("Error");
        }

        var clubVM = new EditClubViewModel
        {
            Title = club.Title,
            Description = club.Description,
            AddressId = club.AddressId,
            Address = club.Address,
            URL = club.Image,
            ClubCategory = club.ClubCategory
        };

        return View(clubVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", clubVM);
        }

        var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

        if (userClub != null)
        {
            try
            {
                await _photoService.DeletePhonoAsync(userClub.Image);
            }
            catch
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(clubVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }

        return View(clubVM);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var clubDetails = await _clubRepository.GetByIdAsync(id);
        if (clubDetails == null) return View("Error");
        return View(clubDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var clubDetails = await _clubRepository.GetByIdAsync(id);
        if (clubDetails == null) return View("Error");

        _clubRepository.Delete(clubDetails);

        return RedirectToAction("Index");
    }
}