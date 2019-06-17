using LoginApp.Common;
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
            bool validFlag = new Utility().IsValidEmail(value.ToString());
            return new ValidationResult(validFlag, validFlag ? null : "Please enter a valid email.");
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
