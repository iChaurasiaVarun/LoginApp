using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Model
{
    /// <summary>
    /// To store KPI details
    /// </summary>
    public class ApiInfo
    {
        public string api_key { get; set; }
        public string password { get; set; }
        public string current_version { get; set; }
        public string client_id { get; set; }
        public string username { get; set; }
    }
}
