using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.AspNetCore.Authorization;
using EasyMind.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EasyMind.Pages.Profiles
{
    #region snippet
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
        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await Context.Profile.FirstOrDefaultAsync(
                                                 m => m.ProfileId == id);

            if (Profile == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Profile,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Profile = await Context.Profile.FindAsync(id);

            var profile = await Context
                .Profile.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, profile,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            Context.Profile.Remove(Profile);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    #endregion
}
