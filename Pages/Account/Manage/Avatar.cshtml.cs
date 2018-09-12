using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyMind.Data;
using EasyMind.Models;
using EasyMind.Pages.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyMind.Pages.Account.Manage
{
    public class AvatarModel : DI_BasePageModel
    {
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AvatarModel(ApplicationDbContext context, IHostingEnvironment environment, IAuthorizationService authorizationService,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [BindProperty]
        //public Customer Customer { get; set; }
        public Profile Profile { get; set; }
        public string StatusMessage { get; private set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Profile = new Profile
            {
                //UserName = user.UserName
                
            };
            return Page();

        }
        private async Task UploadPhoto()
        {
            var uploadsDirectoryPath = Path.Combine(_environment.WebRootPath, "Uploads");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, Profile.Upload.FileName);
            if (!Directory.Exists(uploadsDirectoryPath))
            {
                Directory.CreateDirectory(uploadsDirectoryPath);
            }
            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await Profile.Upload.CopyToAsync(fileStream);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var profile = Profile;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile.OwnerID = UserManager.GetUserId(User);
            //Profile.Pho = Profile.Upload.FileName;
            await UploadPhoto();
            Context.Profile.Add(Profile);
            await Context.SaveChangesAsync();

            StatusMessage = "Your profile picture has been set.";
            return RedirectToPage("./Index");
        }
        
    }
}