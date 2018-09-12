using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyMind.Data;
using EasyMind.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyMind.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly EasyMind.Data.ApplicationDbContext _context;

        public CreateModel(EasyMind.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProfileVmModel> ProfileVmModel { get; set; }
        List<Profile> profile = new List<Profile>();
        List<Image> image = new List<Image>();
        private ActionResult View(IEnumerable<ProfileVmModel> profileViewModel)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.SingleOrDefaultAsync(m => m.ProfileId == id);

            Booking = new Booking
            {
                UserName = Profile.UserName,
                Amount = 1
                
            };
            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }
        public Profile Profile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}