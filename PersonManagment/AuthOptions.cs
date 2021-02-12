using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PersonManagment
{
    public class AuthOptions
    {
        public const string ISSUER = "PersonManagmentAuthServer"; // издатель токена
        public const string AUDIENCE = "PersonManagmentAuthClient"; // потребитель токена
        public const string KEY = "1399f448-e513-4c28-8c7a-afae948a3f92";   // ключ для шифрации
        public const int LIFETIME = 1440; // время жизни токена - 1440 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
