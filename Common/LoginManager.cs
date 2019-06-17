using LoginApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace LoginApp.Common
{
    /// <summary>
    /// To handle login activity
    /// </summary>
    public class LoginManager
    {
        /// <summary>
        /// To validate user information from API
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool Validate(Login login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://alivance.mangoapps.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new Utility().GetLoginRequest(login);
                HttpResponseMessage response = client.PostAsJsonAsync("https://alivance.mangoapps.com/apidoc", request).Result;
                return response.IsSuccessStatusCode;
            }
        }
    }
}
