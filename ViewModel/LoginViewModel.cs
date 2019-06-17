using LoginApp.Common;
using LoginApp.Model;
using System;
using System.Collections;
using System.ComponentModel;
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
        private readonly Utility utility;
        private readonly Validator validate;

        public LoginViewModel()
        {
            loginObject = new Login();
            loginManager = new LoginManager();
            LoginCmd = new RelayCommand(Login, CanLogin);
            utility = new Utility();
            validate = new Validator(this.utility);
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
            get { return utility.GetDomain(loginObject.LoginId);  }
            set {
                loginObject.DomainUrl = value;
                NotifyPropertyChanged("DomainUrl");
            }
        }

        #endregion

        #region Commands


        public ICommand LoginCmd { get; }


        #endregion

        /// <summary>
        /// To handle login
        /// </summary>
        /// <param name="obj"></param>
        private void Login(object obj)
        {
            var password = (obj as PasswordBox).SecurePassword;
            Password = password.Length > 0 ? this.utility.ConvertToUnsecureString(password) : String.Empty;
            
            var login = new Login { LoginId = LoginId, Password = Password, DomainUrl = DomainUrl };
            var validateResponse = this.validate.IsValid(login);
            if (String.IsNullOrWhiteSpace(validateResponse))
                _ = this.loginManager.Validate(login) ? MessageBox.Show("Successfull Login !!!", "Login Successful") : MessageBox.Show(utility.GetErrorMessage(), "Unable To Connect", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(validateResponse, "Login", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "LoginId":
                        if (!string.IsNullOrWhiteSpace(LoginId) && !this.validate.IsValidLoginId(LoginId))
                            return "Please enter a valid login Id.";
                        break;
                    case "Password":
                        if (!string.IsNullOrWhiteSpace(Password) && !this.validate.IsValidPassword(Password))
                            return "Please enter a valid Password.";
                        break;
                }
                return string.Empty;
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
        public string Error { get; }
    }
}
