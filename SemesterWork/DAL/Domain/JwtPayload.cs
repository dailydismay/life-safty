using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SemesterWork.DAL.Domain
{
    public class JwtAuthPayload
    {
        public int ExpireMinutes { get; set; } = 10080;
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[] Claims { get; set; }

        public JwtAuthPayload(long userId)
        {
            Claims = new Claim[] { new Claim("userId", userId.ToString()) };
        }
    }
}
