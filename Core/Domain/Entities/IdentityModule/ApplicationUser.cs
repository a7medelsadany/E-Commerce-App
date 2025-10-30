﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule
{
    public class ApplicationUser:IdentityUser
    {
        public String DisplayName { get; set; } = null!;
        public Address? Address { get; set; }
    }
}
