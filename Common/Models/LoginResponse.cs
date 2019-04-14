using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class LoginResponse
    {
        public AuthEnum AuthResponse { get; set; }
        public string Token { get; set; }
    }
}
