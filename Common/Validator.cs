using LoginApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Common
{
    public class Validator
    {
        private Utility utility = null;

        public Validator(Utility utility)
        {
            this.utility = utility;
        }
        public string IsValid(Login login)
        {
            if (this.IsLoginDataEmpty(login))
                return "Please provide details";

            if (!IsValidLoginId(login.LoginId))
                return "Please provide valid loginId";

            if (!IsValidPassword(login.Password))
                return "Please provide valid password";

            return string.Empty;
        }

        public bool IsLoginDataEmpty(Login login)
        {
            return string.IsNullOrEmpty(login.LoginId) && string.IsNullOrEmpty(login.Password);
        }

        public bool IsValidPassword(string password)
        {
            return !(String.IsNullOrWhiteSpace(password) || password.Length < 5 || password.Length > 25);
        }

        public bool IsValidLoginId(string loginId)
        {
            return !(String.IsNullOrWhiteSpace(loginId) || !utility.IsValidEmail(loginId) || loginId.Length < 4 || loginId.Length > 100);
        }
    }
}
