namespace PeoplePrtal.Models.ViewModel
{
    public class AccountVM
    {
        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string AccountStatus { get; set; } = null!;
        public decimal OutstandingBalance { get; set; }
    }
}
