using Microsoft.AspNetCore.Mvc; 
using CMCS.Data; 
using CMCS.Models; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore; 
using System.Security.Claims; 
using FluentValidation; // For applying FluentValidation rules

namespace CMCS.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the application database context
        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to display all approved claims
        public async Task<IActionResult> ApprovedClaimsSummary()
        {
            // Retrieve all claims with a status of "Approved"
            var approvedClaims = await _context.Claims
                .Where(c => c.Status == "Approved")
                .ToListAsync();

            // Pass the list of approved claims to the view
            return View(approvedClaims);
        }

        // Action to display the invoice for a specific claim
        public async Task<IActionResult> ViewInvoice(int claimId)
        {
            // Fetch the claim with the given ID from the database
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimID == claimId);

            // Check if the claim exists and is approved
            if (claim == null || claim.Status != "Approved")
            {
                return NotFound("Invoice not found or claim is not approved.");
            }

            // Calculate the total payment for the claim
            decimal totalPayment = claim.HoursWorked * claim.HourlyRate;

            // Create a view model for the invoice to pass necessary details to the view
            var invoiceViewModel = new InvoiceViewModel
            {
                ClaimID = claim.ClaimID,
                LecturerName = $"{claim.LecturerName} {claim.LecturerSurname}",
                ClaimDate = claim.ClaimDate,
                HoursWorked = claim.HoursWorked,
                HourlyRate = claim.HourlyRate,
                TotalPayment = totalPayment,
                Status = claim.Status
            };

            return View(invoiceViewModel); // Render the invoice view with the view model
        }

        // Action to edit lecturer information for a specific claim
        public IActionResult EditLecturerInfo(int claimId)
        {
            // Retrieve the lecturer's claim details based on the claim ID
            var lecturer = _context.Claims.FirstOrDefault(l => l.ClaimID == claimId);

            // If the claim is not found, return a 404 error
            if (lecturer == null)
            {
                return NotFound();
            }

            // Pass the claim details to the view for editing
            return View(lecturer);
        }

        // Action to handle the form submission for editing lecturer information
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents cross-site request forgery attacks
        public async Task<IActionResult> EditLecturerInfo(int claimId, LectureClaim lecturer)
        {
            // Ensure the claim ID in the URL matches the lecturer's claim ID
            if (claimId != lecturer.ClaimID)
            {
                return NotFound();
            }

            // Check if the submitted model is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the lecturer's information in the database
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync(); // Save changes asynchronously
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues if the record no longer exists
                    if (!_context.Claims.Any(e => e.ClaimID == claimId))
                    {
                        return NotFound();
                    }
                    throw; // Re-throw the exception if it's not related to concurrency
                }

                // Redirect to the ApprovedClaimsList page after successful update
                return RedirectToAction("ApprovedClaimsList");
            }

            // If the model is invalid, re-display the form with validation errors
            return View(lecturer);
        }
    }
}
