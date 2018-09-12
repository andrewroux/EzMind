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
    public class IndexModel : PageModel
    {
        private readonly EasyMind.Data.ApplicationDbContext _context;

        public IndexModel(EasyMind.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Bookings.ToListAsync();
        }
    }
}
