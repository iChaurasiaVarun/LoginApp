using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Model
{
    /// <summary>
    /// Request model
    /// </summary>
    public class Request
    {
        public Request()
        {
            ms_request = new UserDetails();
        }
        public UserDetails ms_request { get; set; }
    }
}
