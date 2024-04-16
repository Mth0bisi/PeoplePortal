using System;
using System.Collections.Generic;

namespace SuperHeroAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Statuses = new HashSet<Status>();
            Transactions = new HashSet<Transaction>();
        }

        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal OutstandingBalance { get; set; }

        public virtual Person PersonCodeNavigation { get; set; } = null!;
        public virtual ICollection<Status> Statuses { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
