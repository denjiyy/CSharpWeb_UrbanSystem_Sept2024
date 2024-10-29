﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuggestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var suggestions = await _context.Suggestions
                .ToListAsync();

            return View(suggestions);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Suggestion suggestion)
        {
            if (!ModelState.IsValid)
            {
                return View(suggestion);
            }

            await _context.Suggestions.AddAsync(suggestion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
