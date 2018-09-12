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
using Microsoft.AspNetCore.Identity;
using EasyMind.Authorization;

namespace EasyMind.Pages.Profiles.Availability
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public ProfileAvailability ProfileAvailability { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            ProfileAvailability = await Context.ProfileAvailability.SingleOrDefaultAsync(m => m.ProAId == id);

            if (ProfileAvailability == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, ProfileStatus status)
        {
            var profile = await Context.Profile.FirstOrDefaultAsync(
                                                      m => m.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            var contactOperation = (status == ProfileStatus.Approved)
                                                       ? ContactOperations.Approve
                                                       : ContactOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, profile,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            profile.Status = status;
            Context.Profile.Update(profile);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
