using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EasyMind.Pages.Profiles.Availability
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public ProfileAvailability ProfileAvailability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfileAvailability = await Context.ProfileAvailability.SingleOrDefaultAsync(m => m.ProAId == id);

            if (ProfileAvailability == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfileAvailability = await Context.ProfileAvailability.FindAsync(id);

            if (ProfileAvailability != null)
            {
                Context.ProfileAvailability.Remove(ProfileAvailability);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
