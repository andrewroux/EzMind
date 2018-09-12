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
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<ProfileAvailability> ProfileAvailability { get;set; }

        public async Task OnGetAsync()
        {
            var profiles = from c in Context.ProfileAvailability
                           select c;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized)
            {
                profiles = profiles.Where(c => c.OwnerID == currentUserId);
            }
            ProfileAvailability = await Context.ProfileAvailability.ToListAsync();
        }
    }
}
