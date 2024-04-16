namespace PeoplePrtal.Models.ViewModel
{
    public class TransactionVM
    {
        public int Code { get; set; }
        public int AccountCode { get; set; }
        public string TransactionDate { get; set; }
        public string CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
