using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EasyMind.Authorization;

namespace EasyMind.Pages.Profiles.Availability
{
    public class CreateModel : DI_BasePageModel
    {

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProfileAvailability ProfileAvailability { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ProfileAvailability.OwnerID = UserManager.GetUserId(User);
            Context.ProfileAvailability.Add(ProfileAvailability);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Profiles/Index");
        }
    }
}