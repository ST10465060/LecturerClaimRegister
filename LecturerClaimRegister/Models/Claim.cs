using System.ComponentModel.DataAnnotations;

namespace LecturerClaimRegister.Models
{
    /// <summary>
    /// Represents a single monthly claim submitted by a lecturer.
    /// Acts as the "M" in MVC - it holds the data and the rules that data must obey.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// Unique identifier for the claim. Assigned by the controller when a claim is added.
        /// </summary>
        public int ClaimId { get; set; }

        [Required(ErrorMessage = "Lecturer name is required.")]
        [StringLength(100, ErrorMessage = "Lecturer name cannot exceed 100 characters.")]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Module code is required.")]
        [StringLength(10, MinimumLength = 4,
            ErrorMessage = "Module code must be between 4 and 10 characters.")]
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; } = string.Empty;

        [Range(1, 160, ErrorMessage = "Hours worked must be between 1 and 160.")]
        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }

        // 0.01 is used as the lower bound because Range is inclusive,
        // which enforces "greater than zero" for a currency value.
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than zero.")]
        [Display(Name = "Hourly Rate")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        [Required(ErrorMessage = "Claim month is required.")]
        [Display(Name = "Claim Month")]
        public string ClaimMonth { get; set; } = string.Empty;

        /// <summary>
        /// Workflow state of the claim. New claims start as Draft.
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; } = "Draft";

        /// <summary>
        /// Calculated value of the claim. Read-only and derived on access,
        /// so it can never fall out of sync with hours or rate.
        /// </summary>
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount => (decimal)HoursWorked * HourlyRate;
    }
}