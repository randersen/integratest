using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratest.Security
{
    public class JwtHelpers
    {
        private static readonly string _jwtSecret = Environment.GetEnvironmentVariable("INTEGRATEST_JWT_SECRET");

        public static string CreateJwt(JwtRequest request)
        {
            var payload = new Dictionary<string, object>()
            {
                { "email", request.UserEmail },
                { "id", request.UserId }
            };
            var secretKey = _jwtSecret;
            string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
            return token;
        }

        public static IDictionary<string, object> DecodeJwt(string token)
        {
            try
            {
                var payload = JWT.JsonWebToken.DecodeToObject(token, _jwtSecret) as IDictionary<string, object>;
                return payload;
            }
            catch (JWT.SignatureVerificationException)
            {
                Console.WriteLine("Invalid token!");
            }

            return null;
        }

    }
}
