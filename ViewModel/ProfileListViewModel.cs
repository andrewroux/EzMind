using EasyMind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMind.ViewModels
{
    public class ProfileListViewModel
    {
        public IEnumerable<Profile> Profile { get; set; }
        public string CurrentCategory { get; set; }
    }
}
