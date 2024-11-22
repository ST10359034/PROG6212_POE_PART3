namespace CMCS.Models
{
    public class InvoiceViewModel
    {
        public int ClaimID { get; set; }
        public string LecturerName { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
    }
}
