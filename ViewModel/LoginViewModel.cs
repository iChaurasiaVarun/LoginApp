using LoginApp.Common;
using LoginApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginApp.ViewModel
{
    /// <summary>
    /// Login viewmodel
    /// </summary>
    public class LoginViewModel: ViewModelBase, IDataErrorInfo
    {
        private readonly Login loginObject;
        private readonly LoginManager loginManager;
        private readonly ICommand _loginCmd;
        private readonly Utility _utility;

        public LoginViewModel()
        {
            loginObject = new Login();
            loginManager = new LoginManager();
            _loginCmd = new RelayCommand(Login, CanLogin);
            _utility = new Utility();
        }


        #region Properties
        public string LoginId
        {
            get { return loginObject.LoginId; }
            set
            {
                loginObject.LoginId = value;
                NotifyPropertyChanged("LoginId");
                NotifyPropertyChanged("DomainUrl");
            }
        }

        public string Password
        {
            get { return loginObject.Password; }
            set
            {
                loginObject.Password = value;
                NotifyPropertyChanged("Password");
            }
        }

        
        public string DomainUrl
        {
            get { if (String.IsNullOrWhiteSpace(loginObject.LoginId)) return loginObject.LoginId; return _utility.GetDomain(loginObject.LoginId);  }
            set {
                loginObject.DomainUrl = value;
                NotifyPropertyChanged("DomainUrl");
            }
        }

        #endregion

        #region Commands


        public ICommand LoginCmd { get { return _loginCmd; } }


        #endregion

        /// <summary>
        /// To handle login
        /// </summary>
        /// <param name="obj"></param>
        private void Login(object obj)
        {
            var password = (obj as PasswordBox).SecurePassword;
            if(password.Length > 0)
            {
                Password = _utility.ConvertToUnsecureString(password);
            }
            if (!string.IsNullOrWhiteSpace(LoginId) && !string.IsNullOrWhiteSpace(Password) && this["LoginId"] == "" && this["Password"] == "")
            {
                //var secureString = passwordContainer.Password;
                var login = new Login { LoginId = LoginId, Password = Password, DomainUrl = DomainUrl };
                if (loginManager.Validate(login))
                {
                    MessageBox.Show("Successfull Login !!!", "Login Successful");
                }
                else
                {
                    MessageBox.Show(_utility.GetErrorMessage(), "Unable To Connect", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if(string.IsNullOrWhiteSpace(LoginId) && string.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.Show("Please provide details", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(string.IsNullOrWhiteSpace(LoginId) || this["LoginId"] != "")
                {
                    MessageBox.Show("Please provide valid loginId", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrWhiteSpace(Password) || this["Password"] != "")
                {
                    MessageBox.Show("Please provide valid password", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        

        private bool CanLogin(object obj)
        {
            return true;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public string this[string columnName]
        {
            get
            {
                
                switch (columnName)
                {
                    case "LoginId":
                        if (!string.IsNullOrWhiteSpace(LoginId) && (!_utility.IsValidEmail(LoginId) || !( LoginId.Length >= 4 && LoginId.Length <= 100 )))
                            return "Please enter a valid login Id.";
                        break;
                    case "Password":
                        if (!string.IsNullOrWhiteSpace(Password) && (!(Password.Length >= 5 && Password.Length <= 25)))
                            return "Please enter a valid Password.";
                        break;
                }
                return string.Empty;
            }
        }
        public string Error { get; }
    }
}
