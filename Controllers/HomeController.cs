using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


/*using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting; // For IWebHostEnvironment
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env; // Inject the IWebHostEnvironment

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        // Action method to display the home page
        public IActionResult Index()
        {
            return View();
        }

        // Action method for the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Action to handle file uploads
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Get the path to wwwroot
                string webRootPath = _env.WebRootPath;
                string uploadsFolder = Path.Combine(webRootPath, "uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create the full path to the file
                string filePath = Path.Combine(uploadsFolder, file.FileName);

                // Save the file to wwwroot/uploads folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Optionally, you can return the file path or confirmation message to the user
                ViewBag.Message = "File uploaded successfully!";
                ViewBag.FilePath = filePath;
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return View("Index"); // Return to the Index view
        }

        // Error handling action method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
*/