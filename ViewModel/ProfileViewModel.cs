using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMind.ViewModels
{
    public class ProfileViewModel
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public decimal Price { get; set; }
        public string PhotPath { get; set; }
    }
}
