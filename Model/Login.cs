using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Model
{
    /// <summary>
    /// Login models
    /// </summary>
    public class Login
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string DomainUrl { get; set; }
    }
}
