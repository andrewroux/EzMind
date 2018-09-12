using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyMind.Data;
using EasyMind.Models;
using EasyMind.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace EasyMind.Pages.Profiles
{

    #region snippetCtor
    public class CreateModel : DI_BasePageModel
    {

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {

        }
        #endregion


        public IActionResult OnGet()
        {
            Profile = new Profile
            {
                //Name = "Rick",
                //Address = "123 N 456 S",
                //City = "GF",
                //State = "MT",
                //Zip = "59405",
                //Email = "rick@rick.com"
            };
            return Page();
        }

        [BindProperty]
        public Profile Profile { get; set; }

        #region snippet_Create
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile.OwnerID = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Profile,
                                                        ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            Context.Profile.Add(Profile);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Profiles/CreateImage");
        }
        #endregion
        //public ActionResult OnPostUpload(List<IFormFile> files)
        //{
        //    if (files != null && files.Count > 0)
        //    {
        //        string folderName = "Upload";
        //        string webRootPath = _hostingEnvironment.WebRootPath;
        //        string newPath = Path.Combine(webRootPath, folderName);
        //        if (!Directory.Exists(newPath))
        //        {
        //            Directory.CreateDirectory(newPath);
        //        }
        //        foreach (IFormFile item in files)
        //        {
        //            if (item.Length > 0)
        //            {
        //                string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
        //                string fullPath = Path.Combine(newPath, fileName);
        //                using (var stream = new FileStream(fullPath, FileMode.Create))

        //                {
        //                    item.CopyTo(stream);
        //                }
        //            }
        //        }
        //        return this.Content("Success");
        //    }
        //    return this.Content("Fail");
        //}
    }
}