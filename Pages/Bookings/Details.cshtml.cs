using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyMind.Data;
using EasyMind.Models;

namespace EasyMind.Pages.Bookings
{
    public class DetailsModel : PageModel
    {
        private readonly EasyMind.Data.ApplicationDbContext _context;

        public DetailsModel(EasyMind.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await _context.Bookings.SingleOrDefaultAsync(m => m.BookingId == id);

            if (Booking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
