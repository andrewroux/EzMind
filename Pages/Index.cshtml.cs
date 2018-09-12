﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyMind.Pages
{
    public class IndexModel : PageModel
    {
        private IHostingEnvironment _hostingEnvironment;
        public IndexModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void OnGet()
        {
        }

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

