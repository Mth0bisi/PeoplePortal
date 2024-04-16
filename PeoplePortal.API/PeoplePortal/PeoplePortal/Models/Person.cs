using System;
using System.Collections.Generic;

namespace PeoplePrtal.Models
{
    public partial class Person
    {
        public Person()
        {
            Accounts = new HashSet<Account>();
        }

        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string IdNumber { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
