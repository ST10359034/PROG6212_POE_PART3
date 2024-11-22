using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manager/Login
        public IActionResult ManagerLogin()
        {
            return View();
        }

        // POST: Manager/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManagerLogin(Manager manager)
        {
            // Placeholder authentication logic (replace with database authentication)
            if (manager.Username == "Moderator" && manager.Password == "12345") // Replace with real authentication logic
            {
                // Redirect to the ClaimsList 
                return RedirectToAction("Index", "LectureClaims");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(manager); // Return view with error message
        }

        // Create Manager Action
        public async Task<IActionResult> CreateManager(Manager manager)
        {
            // Add manager to the database
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Manager/Logout
        public IActionResult Logout()
        {
            // Clear session or authentication cookie (implementation not shown)
            return RedirectToAction("ManagerLogin");
        }
    }
}

