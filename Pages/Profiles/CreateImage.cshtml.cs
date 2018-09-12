using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using EasyMind.Authorization;

namespace EasyMind.Pages.Profiles
{
    public class CreateImageModel : DI_BasePageModel
    {
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CreateImageModel(ApplicationDbContext context, IHostingEnvironment environment, IAuthorizationService authorizationService,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //Image = new Image
            //{
            //    UserProfileId = Profile.ProfileId

            //};
            return Page();

        }


        [BindProperty]
        public Image Image { get; set; }
        public Profile Profile { get; set; }

        public string StatusMessage { get; private set; }
        private async Task UploadPhoto()
        {
            var uploadsDirectoryPath = Path.Combine(_environment.WebRootPath, "Uploads");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, Image.UploadImage.FileName);

            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await Image.UploadImage.CopyToAsync(fileStream);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Image.OwnerID = UserManager.GetUserId(User);
            Image.ImagePath = Image.UploadImage.FileName;
            await UploadPhoto();
            Context.Image.Add(Image);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Profiles/Availability/Create");
        }
    }
}