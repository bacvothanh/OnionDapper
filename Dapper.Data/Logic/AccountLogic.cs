using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Dapper.Data.Enums;
using Dapper.Data.Models;
using Dapper.Infrastructure;
using Dapper.Infrastructure.Extensions;

namespace Dapper.Data.Logic
{
    public static class AccountLogic
    {
        public static void SetPassword(this Account account, string password)
        {
            account.HashPassword = password.Encrypt(EncryptType.Md5);
        }

        public static bool IsValidPassword(this Account account, string password)
        {
            return account.HashPassword == password.Encrypt(EncryptType.Md5);
        }

        public static void GenerateTokenResetPassword(this Account account)
        {
            account.TokenResetPassword = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).UrlEncode();
        }

        public static void GenerateActiveToken(this Account account)
        {
            account.TokenConfrimEmail = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).UrlEncode();
        }


        public static string GetActiveToken(this Account account)
        {
            return account.TokenConfrimEmail;
        }

        public static string GetDisplayName(this Account account)
        {
            return !string.IsNullOrEmpty(account.DisplayName) ? account.DisplayName : account.UserName;
        }
        
        

        public static void AddRole(this Account account, Role role)
        {
            if (account.Roles == null)
                account.Roles = new List<Role>();
            if (account.Roles.FirstOrDefault(x => x.Name == role.Name) != null)
                return;
            account.Roles.Add(role);
        }

        

        public static void ConfirmEmail(this Account account)
        {
            account.Status=AccountStatus.Active;
            account.TokenConfrimEmail = null;
        }

        public static ClaimsIdentity CreateClaimSignIn(this Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.GivenName, account.GetDisplayName())
            };
            if (account.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, account.Email));
            }
            if (account.Roles != null)
            {
                var roles = account.Roles.Select(x => x.Name).ToArray();
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            
            return new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
