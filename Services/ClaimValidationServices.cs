// Import the FluentValidation library, which provides tools for creating and applying validation rules
using FluentValidation;
using CMCS.Models;

namespace CMCS.Services;

// Validator for LectureClaim that ensures the claim adheres to business rules
public class ClaimValidator : AbstractValidator<LectureClaim>
{
    public ClaimValidator()
    {
        // Rule: HoursWorked must be greater than 0 and less than 100
        RuleFor(x => x.HoursWorked)
            .GreaterThan(0) // Ensures HoursWorked is positive
            .WithMessage("Hours worked must be greater than 0.") // Custom error message for invalid input
            .LessThan(100) // Ensures HoursWorked does not exceed 100
            .WithMessage("Hours worked cannot exceed 100."); // Custom error message for exceeding limit

        // Rule: HourlyRate must be between 20 and 100
        RuleFor(x => x.HourlyRate)
            .InclusiveBetween(20, 100) // Ensures HourlyRate is within the acceptable range
            .WithMessage("Hourly rate must be between 20 and 100."); // Custom error message for out-of-range input
    }
}
