using System;
using System.Collections.Generic;

namespace SuperHeroAPI.Models
{
    public partial class User
    {
        public int Code { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
