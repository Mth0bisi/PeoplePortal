using System;
using System.Collections.Generic;

namespace PeoplePrtal.Models
{
    public partial class Status
    {
        public int Code { get; set; }
        public string StatusType { get; set; } = null!;
        public int AccountCode { get; set; }

        public virtual Account AccountCodeNavigation { get; set; } = null!;
    }
}
