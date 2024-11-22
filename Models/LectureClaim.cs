using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCS.Models
{
    // LectureClaim model represents the details of a lecturer's claim for hours worked
    public class LectureClaim
    {
        [Key] // Specifies ClaimID as the primary key
        public int ClaimID { get; set; }

        [Required] // Lecturer's first name is a required field
        public string LecturerName { get; set; }

        [Required] // Lecturer's surname is a required field
        public string LecturerSurname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")] // Validates proper email format
        public string LecturerEmail { get; set; }

        [DataType(DataType.Date)] // Specifies that ClaimDate will be stored as a date only
        public DateTime ClaimDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid hours worked")] // Ensures a positive number of hours
        public decimal HoursWorked { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter valid hourly rate")] // Ensures a positive hourly rate
        public decimal HourlyRate { get; set; }

        public decimal FinalPayment => HoursWorked * HourlyRate; // Auto-calculated property


        // New field to store the status of the claim, with a default value of 'Pending'
        [Required]
        public string Status { get; set; } = "Pending";

        // NotMapped means this property is not stored in the database, it's only used for file upload purposes
        [NotMapped]
        [Display(Name = "Upload Supporting Document")] // Display name for the file upload field in the UI
        public IFormFile ClaimDocument { get; set; }
    }
}
