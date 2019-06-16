using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LoginApp.ViewModel
{
    /// <summary>
    /// base class for ViewModels
    /// </summary>
    public class ViewModelBase : ValidationRule, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                new MailAddress(value.ToString());
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Please enter a valid email.");
            }
            return new ValidationResult(true, null);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
