using MWVMLib;
using DatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using UserWindow;
using System.Windows.Controls;

namespace LoginWindow
{
    class LoginWindowViewModel : ViewModelBase
    {
        private CinemaEntities _database;


        public LoginWindowViewModel()
        {
            SignUpCommand = new RelayCommand(SignUpCommandExecute, null);
            SignInCommand = new RelayCommand(SignInCommandExecute, SignInCommandCanExecute);

            _database = new CinemaEntities();
        }

        private bool SignInCommandCanExecute(object obj)
        {
            if (PhoneNumber != string.Empty )
                return true;
            return false;
        }

        private void SignInCommandExecute(object obj)
        {
            var pb = obj as PasswordBox;
            if(PhoneNumber == "admin" && pb.Password == "admin")
            {
                AdminWindow.AdminWindowXaml aw = new AdminWindow.AdminWindowXaml();
                aw.Show();
                Application.Current.MainWindow.Close();
            }

            if ( _database.Users.Any(u => u.phone_number == PhoneNumber && u.password == pb.Password) )
            {
                UserWindow.UserWindowClass uw = new UserWindow.UserWindowClass();
                Messenger.Default.Send(PhoneNumber);
                uw.Show();

                Application.Current.MainWindow.Close();
            }

            else
            {

            }
        }

        private void SignUpCommandExecute(object obj)
        {
            Messenger.Default.Send(PhoneNumber);

            SignUpWindowProject.SignUpWindow suw = new SignUpWindowProject.SignUpWindow();
            suw.Show();

            Application.Current.Windows[0].Close();
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public RelayCommand SignUpCommand { get; }
        public RelayCommand SignInCommand { get; private set; }
    }
}
