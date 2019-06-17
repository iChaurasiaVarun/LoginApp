using LoginApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Common
{
    /// <summary>
    /// Common Utility  
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Get domain from loginId
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public string GetDomain(string loginId)
        {
            if (String.IsNullOrWhiteSpace(loginId) || !this.IsValidEmail(loginId))
                return string.Empty;
            MailAddress address = new MailAddress(loginId);
            string splittedHost = address.Host.Remove(address.Host.LastIndexOf("."));
            return String.Format("https://{0}.mangopulse.com", splittedHost.Replace(".", "-"));
        }

        /// <summary>
        /// Check for valid email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsValidEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get password from password box security
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        public string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Get login request
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string GetLoginRequest(Login login)
        {
            Request rq = new Request();
            rq.ms_request.user.api_key = "MangoAppForWindows";
            rq.ms_request.user.client_id = "windows";
            rq.ms_request.user.current_version = "1.0.0.0";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(login.Password);
            rq.ms_request.user.password = System.Convert.ToBase64String(plainTextBytes);
            rq.ms_request.user.username = login.LoginId;
            return JsonConvert.SerializeObject(rq);
        }

        public string GetErrorMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("We are unable to reach the server or it did not properly respond.");
            sb.AppendLine();
            sb.AppendLine("Kindly verify the following and try again:");
            sb.AppendLine("1. Your Domain URL is correct (e.g., https://demo.mangoapps.com)");
            sb.AppendLine("2. You are connected to the internet");
            sb.AppendLine("3. The proxy settings are configured correctly in preferences (if you use a proxy to connect to the internet)");
            sb.AppendLine("4. Server may be temporarily down. Please re-try in a few minutes.");
            return sb.ToString();
        }
    }
}
