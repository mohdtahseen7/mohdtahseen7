using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TokenManager
{
    public class TokenManagement
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";


        public static string GenerateAPIToken(string username, String uniqueId)
        {
            DateTime SessionTime = DateTime.Now;
            DateTime ExpiryTime = SessionTime.AddYears(10);
            double TokenLifetimeinSeconds = (ExpiryTime - SessionTime).TotalSeconds;

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, uniqueId), new Claim(ClaimTypes.Name, username) }),
                // new Claim(ClaimTypes.Name, username,ClaimTypes.uniqueData,uniqueId)}),
                Expires = DateTime.UtcNow.AddSeconds(TokenLifetimeinSeconds),

                //Expires =  DateTime.UtcNow.AddSeconds(120),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            Console.WriteLine(descriptor.Expires);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }


        public static string GenerateToken(string username, String uniqueId, int TokenLifetimeinSeconds = 400)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, uniqueId), new Claim(ClaimTypes.Name, username) }),
                // new Claim(ClaimTypes.Name, username,ClaimTypes.uniqueData,uniqueId)}),
                Expires = DateTime.UtcNow.AddSeconds(TokenLifetimeinSeconds),

                //Expires =  DateTime.UtcNow.AddSeconds(120),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            Console.WriteLine(descriptor.Expires);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            var tokenvalue = token.RawData;


            var specialCharcter = (tokenvalue.Contains("--") ? true : false);
            if (specialCharcter == true)
            {

                GenerateToken(username, uniqueId, TokenLifetimeinSeconds = 600);


            }

            return handler.WriteToken(token);

        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {

                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            var expClaim = principal.Claims.First(x => x.Type == "exp").Value;
            var tokenExpiryTime = UnixTimeToDateTime(Convert.ToInt64(expClaim));
            if (tokenExpiryTime < DateTime.UtcNow)
            {
                return null;
            }

            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);

            username = usernameClaim.Value;
            return username;
        }
        public static bool ValidateJWToken(string token, string UniqueID)
        {
            bool response = false;
            string username = string.Empty;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return response;

            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;

            }
            catch (NullReferenceException)
            {
                return response;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;

            response = (UniqueID == username) ? true : false;

            return response;
        }
        public static string Extract(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            username = usernameClaim.Value;
            return username;
        }

    }
}
