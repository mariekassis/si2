using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace si2.bll.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim ("Create Role", "false"),
            new Claim ("Edit Role", "false"),
            new Claim ("Delete Role", "false"),
            new Claim ("FullControl Role", "false")
        };
    }
}