using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EasyMind.Pages.Profiles.Availability
{
    public class EditModel : DI_BasePageModel
    {
        private readonly EasyMind.Data.ApplicationDbContext _context;
        public EditModel(
           ApplicationDbContext context,
           IAuthorizationService authorizationService,
           UserManager<ApplicationUser> userManager)
           : base(context, authorizationService, userManager)
        {
            _context = context;

        }

        [BindProperty]
        public ProfileAvailability ProfileAvailability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfileAvailability = await _context.ProfileAvailability.SingleOrDefaultAsync(m => m.ProAId == id);

            if (ProfileAvailability == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProfileAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileAvailabilityExists(ProfileAvailability.ProAId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProfileAvailabilityExists(int id)
        {
            return _context.ProfileAvailability.Any(e => e.ProAId == id);
        }
    }
}
