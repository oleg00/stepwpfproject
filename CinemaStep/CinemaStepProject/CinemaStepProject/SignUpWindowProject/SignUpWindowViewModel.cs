using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWVMLib;
using System.Text.RegularExpressions;
using DatabaseLib;
using Lib_SMS;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.Windows;

namespace SignUpWindowProject
{
    class SignUpWindowViewModel : ViewModelBase
    {
        private CinemaEntities _database;
        private int _generatedSmsCode;

        public SignUpWindowViewModel()
        {
            SignUpCommand = new RelayCommand(SignUpCommandExecute, SignUpCommandCanExecute);
            GetSmsCodeCommand = new RelayCommand(GetSmsCodeCommandExecute, null);
            //Messenger.Default.Register<string>(this, GetNumber, true);

            _database = new CinemaEntities();
        }

        private void GetSmsCodeCommandExecute(object obj)
        {
            SMSSender sender = new SMSSender("vladmir.klepach@gmail.com", "1qaZ2wsX", "name");

            _generatedSmsCode = new Random().Next(9999);

            sender.Send(PhoneNumber, _generatedSmsCode.ToString());
        }

        private bool SignUpCommandCanExecute(object obj)
        {
            if (PhoneNumber != null && Email != null)
            {
                if (PhoneNumber != string.Empty && PhoneNumber.Length <= 15 && Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                    return true;
            }
            return false;
        }

        private void SignUpCommandExecute(object obj)
        {
            PasswordBox pb = obj as PasswordBox;
            if (_generatedSmsCode.ToString() == SmsCode)
            {
                _database.Users.Add(new Users { phone_number = PhoneNumber, email = Email, password = pb.Password });

                UserWindow.UserWindowClass wnd = new UserWindow.UserWindowClass();
                wnd.Show();
                _database.SaveChanges();

                Messenger.Default.Send(PhoneNumber);

                Application.Current.Windows[0].Close();
            }

            else
            {
                SmsCode = "You've typed a wrong code. Try again.";
            }
        }

        #region properties
        private string _phonenumb;

        public string PhoneNumber
        {
            get { return _phonenumb; }
            set { _phonenumb = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _codeview;

        public string SmsCode
        {
            get { return _codeview; }
            set { _codeview = value;
                OnPropertyChanged(nameof(SmsCode));
            }
        }
        #endregion

        public RelayCommand SignUpCommand { get; private set; }
        public RelayCommand GetSmsCodeCommand { get; private set; }
    }
}
