using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMind.Data;
using EasyMind.Models;
using EasyMind.Pages.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyMind.Pages
{
    public class ListingProModel : DI_BasePageModel
    {
        public ListingProModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }
        [BindProperty]
        public IList<Profile> Profile { get; set; }
        [BindProperty]
        public IList<Image> Image { get; set; }
        [BindProperty]
        public IEnumerable<ProfileVmModel> ProfileVmModel { get; set; }
        


        List<Profile> profile = new List<Profile>();
        List<Image> image = new List<Image>();

      

        private ActionResult View(IEnumerable<ProfileVmModel> profileViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task OnGetAsync(string searchString)
        {
            var profileViewModel = from p in Context.Profile
                                   join i in Context.Image on p.OwnerID equals i.OwnerID into st2
                                   from i in st2.DefaultIfEmpty()
                                   select new ProfileVmModel { profileVm = p, imageVm = i };
            


            var profiles = from c in Context.Profile
                           select c;

            var pic = from c in Context.Image
                      select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(c => c.City.Contains(searchString));
            }


            Image = await pic.ToListAsync();
            Profile = await profiles.ToListAsync();
            ProfileVmModel= await profileViewModel.ToListAsync();
        }

    }
}