using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Dapper.Infrastructure;
using Dapper.Infrastructure.Extensions;

namespace Dapper.Data.Models
{
    public class CustomPrincipal : ClaimsPrincipal
    {
        public CustomPrincipal(IIdentity identity) : base(identity)
        {
        }
        public string UserName => GetClaimValueAsString(ClaimTypes.Name);

        public string DisplayName
        {
            get
            {
                var displayName = GetClaimValueAsString(ClaimTypes.GivenName);
                if (string.IsNullOrEmpty(displayName))
                    return UserName;
                return displayName;
            }
        }

        public long Id => GetClaimValueAsString(ClaimTypes.NameIdentifier).ParseToLong();
        public string[] Roles => GetClaimValueAsStringArray(ClaimTypes.Role);
        public string Email => GetClaimValueAsString(ClaimTypes.Email);
        
        public int? PatientId => GetClaimValueAsString(ClaimTypes.DenyOnlySid).ParseToInt();
        public int? DoctorId => GetClaimValueAsString(ClaimTypes.DenyOnlyPrimarySid).ParseToInt();

        public int? ManageHospitalId => GetClaimValueAsString(ClaimTypes.GroupSid).ParseToInt();

        public string Avatar
        {
            get
            {
                var imageAvatar = GetClaimValueAsString(ClaimTypes.Thumbprint);
                if (string.IsNullOrEmpty(imageAvatar))
                    imageAvatar = ApplicationConstant.DefaultAvatar;
                return imageAvatar;
            }
        }

        private string GetClaimValueAsString(string type)
        {
            var claim = this.FindFirst(type);
            return claim?.Value;
        }
        private string[] GetClaimValueAsStringArray(string type)
        {
            var claims = this.Claims.Where(x => x.Type == type)
                .Select(x => x.Value)
                .ToList();
            if (claims.Count > 0)
            {
                return claims.ToArray();
            }
            return null;
        }

    }
}
