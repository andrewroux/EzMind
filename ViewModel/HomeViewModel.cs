using EasyMind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMind.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Profile> PreferredServices { get; set; }
    }
}
