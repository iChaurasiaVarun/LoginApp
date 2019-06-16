using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Model
{
    /// <summary>
    /// user detail models
    /// </summary>
    public class UserDetails
    {
        public UserDetails()
        {
            user = new ApiInfo();
        }
        public ApiInfo user { get; set; }
    }
}
