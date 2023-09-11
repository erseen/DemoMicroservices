using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURİTY_KEY = "yadsjasd5546468813asdASDASSFAGGFXXV";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            _userAccountList = new List<UserAccount>()
            {
                new UserAccount() {UserName="admin",Password="admin123",Role="Administrator"},
                new UserAccount() {UserName="user01",Password="user01",Role="User"}
            };
        }
        public AuthenticationResponse? GenerateJWtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName)|| string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;
            //Validation 
                var userAccount=_userAccountList.Where(x=>x.UserName==authenticationRequest.UserName && x.Password== authenticationRequest.Password).FirstOrDefault();
                if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURİTY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.UserName),
                new Claim("Role",userAccount.Role)
            });

            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signinCredentials
            };
            var jwtSecurityTokenHandler= new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName=userAccount.UserName,
                ExpireIn=(int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken=token
            };
            
        
        }
    }
}
