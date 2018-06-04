using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWVMLib;
using DatabaseLib;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Mail;
using System.Net;
using System.Windows;

namespace UserWindow
{
    class UserWindowViewModel : ViewModelBase
    {
        private CinemaEntities _database;

        public UserWindowViewModel()
        {
            _database = new CinemaEntities();
            Messenger.Default.Register<string>(this, FillPhoneNumber, false);

            #region OrderTicketWindow
            Cities = _database.Cities.ToList();
            Cinemas = new List<Cinemas>();
            Films = new List<Films>();
            Sessions = new List<MovieSession>();
            Categories = _database.Categories.ToList();

            ToOrderCommand = new RelayCommand(ToOrderCommandExecute, ToOrderCommandCanExecute);
            #endregion

            #region CheckOutFilmInfoWindow
            Films_FilmInfo = _database.Films.ToList();
            CurrentReviewUrl = "https://youtube.com";
            #endregion

            #region Weather

            WeatherImageSource = "https://i.ytimg.com/vi/aOPsmnexJi8/hqdefault.jpg";
            DefineWeather();

            #endregion

            #region Distance
            GetDistanceCommand = new RelayCommand(GetDistanceCommandExecute, GetDistanceCommandCanExecute);
            UserAddress = string.Empty;
            #endregion

        }

        #region WeatherWindow

        private string _weatherStatus;

        public string WeatherStatus
        {
            get { return _weatherStatus; }
            set { _weatherStatus = value;
                OnPropertyChanged(nameof(WeatherStatus));
            }
        }

        private string _weatherTemperature;

        public string WeatherTemperature
        {
            get { return _weatherTemperature; }
            set { _weatherTemperature = value;
                OnPropertyChanged(nameof(WeatherTemperature));
            }
        }

        private string _WeatherWind;

        public string WeatherWind 
        {
            get { return _WeatherWind; }
            set { _WeatherWind = value;
                OnPropertyChanged(nameof(WeatherWind));
            }
        }

        private string _weatherRain;

        public string WeatherRain
        {
            get { return _weatherRain; }
            set { _weatherRain = value;
                OnPropertyChanged(nameof(WeatherRain));
            }
        }

        private string _weatherImageSource;

        public string WeatherImageSource
        {
            get { return _weatherImageSource; }
            set { _weatherImageSource = value;
            OnPropertyChanged(nameof(WeatherImageSource)); }
        }

        private void DefineWeather()
        {
            WeatherService weatherService = new WeatherService();
            string weather_params = weatherService.GetActualData();
            var splitted_params = weather_params.Split(',');
            WeatherStatus = splitted_params[0];
            WeatherWind = splitted_params[1];
            WeatherTemperature = splitted_params[2];
            WeatherRain = splitted_params[3];

            switch (WeatherStatus)
            {
                case "Облачно":
                    WeatherImageSource = "http://wingbeat.co.uk/wp-content/uploads/2013/05/Cloudy_large.jpg";
                    break;
                case "Ясная погода":
                    WeatherImageSource = "https://i.pinimg.com/736x/29/43/3b/29433bcd3efadefba2d0b883ed1672c6--grumpy-cat-meme-grumpy-kitty.jpg";
                    break;
                //case "Переменная облачность":
                //    WeatherImageSource = "https://i.ytimg.com/vi/aOPsmnexJi8/hqdefault.jpg";
                //    break;
            }
        }
        #endregion

        #region OrderTicketWindow

        private bool GetDistanceCommandCanExecute(object obj) => UserAddress != string.Empty ? true : false;

        private void GetDistanceCommandExecute(object obj)
        {
            GetDistanceClass getDistance = new GetDistanceClass();
            DistanceToCinema = getDistance.getDistance(UserAddress, SelectedCinema.address);
        }

        private string _userAddress;

        public string UserAddress
        {
            get { return _userAddress; }
            set
            {
                _userAddress = value;
                OnPropertyChanged(nameof(UserAddress));
            }
        }

        private string _distanceToCinema;

        public string DistanceToCinema
        {
            get { return _distanceToCinema; }
            set
            {
                _distanceToCinema = value;
                OnPropertyChanged(nameof(DistanceToCinema));
            }
        }


        private string _StatusText;
        

        public string StatusText
        {
            get { return _StatusText; }
            set { _StatusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }


        private bool ToOrderCommandCanExecute(object obj)
        {
            if (SelectedPlace != null && SelectedSession != null)
                return true;
            return false;
        }

        private void ToOrderCommandExecute(object obj)
        {
            var ticket = new Tickets
            {
                id_session = SelectedSession.id,
                id_user = CurrentUser.id
            };

            _database.Tickets.Add(ticket);

            _database.FreePlacesForSession.Single(p => p.id_hall == SelectedSession.id_hall && p.place_number == SelectedPlace.place_number).is_empty = false;

            _database.SaveChanges();
            SendReceiptByEmail(ticket);
        }

        private void SendReceiptByEmail(Tickets ticket)
        {
            var fromAddress = new MailAddress("stepemailsender@gmail.com", "Cinema");
            var toAddress = new MailAddress(CurrentUser.email); 
            const string fromPassword = "sendemail";
            const string subject = "Your ticket is formed in.";

            Receipts receipt = new Receipts
            {
                content = $"<p><strong>Your ticket: {ticket.id}</strong></p> <p></p> <p><span style = 'text-decoration: underline;'> Your film: {_database.Films.Single(f=>f.id == ticket.MovieSession.id_film).title}</span> </p> <p><span style='text-decoration: underline;'> Your hall: {ticket.MovieSession.id_hall.ToString()}</span></p><p><span style = 'text-decoration: underline;'> Date: {ticket.MovieSession.date.ToString()}</span></p> <p><span style = 'text-decoration: underline;'> Cinema: {ticket.MovieSession.Cinemas.title}</span></p>"
            };

            _database.Receipts.Add(receipt);

            string body = receipt.content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
                StatusText = "Your ticket is ordered. Receipt has sent to your e-mail.";
            }
        }

        private void SessionChangedHandler() => FreePlacesForSession = _database.FreePlacesForSession.Where(fpfs => fpfs.id_hall == SelectedSession.id_hall && fpfs.is_empty == true).ToList();

        private void FilmChangedHandler() {
            if (SelectedFilm != null && SelectedCinema != null)
            Sessions = _database.MovieSession.Where(ms => ms.id_film == SelectedFilm.id && ms.id_cinema == SelectedCinema.id).ToList();
        }

        private void FillPhoneNumber(string s)
        {
            Phone_Number = s;
            CurrentUser = _database.Users.Single(u => u.phone_number == Phone_Number);
        }

        public string Phone_Number { get; set; }

        private void CategoryChangedHandler()
        {
            IQueryable<int> id_films = _database.FilmCategories.Where(fc => fc.id_category == SelectedCategory.id).Select(i => i.id);
            Films = _database.Films.Where(f => id_films.Contains(f.id)).ToList();
        }

        private void CityChangedHandler() => Cinemas = _database.Cinemas.Where(c => c.id_city == SelectedCity.id).ToList();

        private Users CurrentUser { get; set; }
        public List<Cities> Cities { get; set; }
        public List<Categories> Categories { get; set; }

        private List<Cinemas> _cinemas;

        public List<Cinemas> Cinemas
        {
            get { return _cinemas; }
            set { _cinemas = value;
                OnPropertyChanged(nameof(Cinemas));
            }
        }

        private List<Films> _films;

        public List<Films> Films
        {
            get { return _films; }
            set { _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }


        private List<MovieSession> _ms;

        public List<MovieSession> Sessions
        {
            get { return _ms; }
            set { _ms = value;
                OnPropertyChanged(nameof(Sessions));
            }
        }

        private List<FreePlacesForSession> _fpfs;

        public List<FreePlacesForSession> FreePlacesForSession
        {
            get { return _fpfs; }
            set
            {
                _fpfs = value;
                OnPropertyChanged(nameof(FreePlacesForSession));
            }
        }


        private Cities _scity;

        public Cities SelectedCity
        {
            get { return _scity; }
            set { _scity = value;
                CityChangedHandler();
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        private Categories _scategory;

        public Categories SelectedCategory
        {
            get { return _scategory; }
            set { _scategory = value;
                CategoryChangedHandler();
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private Cinemas _scinema;

        public Cinemas SelectedCinema
        {
            get { return _scinema; }
            set { _scinema = value;
                OnPropertyChanged(nameof(SelectedCinema));
            }
        }

        private Films _sfilm;

        public Films SelectedFilm
        {
            get { return _sfilm; }
            set { _sfilm = value;
                FilmChangedHandler();
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }


        private MovieSession _ss;

        public MovieSession SelectedSession
        {
            get { return _ss; }
            set { _ss = value;
                SessionChangedHandler();
                OnPropertyChanged(nameof(SelectedSession));
            }
        }


        private FreePlacesForSession _sfpfs;

        public FreePlacesForSession SelectedPlace
        {
            get { return _sfpfs; }
            set { _sfpfs = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }


        #endregion

        #region CheckOutFilmInfoWindow

        private List<Films> _films_fi;

        public List<Films> Films_FilmInfo
        {
            get { return _films_fi; }
            set
            {
                _films_fi = value;
                OnPropertyChanged(nameof(Films_FilmInfo));
            }
        }

        private Films _sfilm_FilmInfo;

        public Films SelectedFilm_FilmInfo
        {
            get { return _sfilm_FilmInfo; }
            set
            {
                if (_sfilm_FilmInfo != value)
                {
                    _sfilm_FilmInfo = value;

                    Film_Info_FilmChangedHandler();

                    OnPropertyChanged(nameof(SelectedFilm_FilmInfo));
                }
            }
        }

        private string _current_review_url;

        public string CurrentReviewUrl
        {
            get { return _current_review_url; }
            set { _current_review_url = value;
                OnPropertyChanged(nameof(CurrentReviewUrl));
            }
        }

        private string _current_film_descr;

        public string CurrentFilmDescription
        {
            get { return _current_film_descr; }
            set { _current_film_descr = value;
                OnPropertyChanged(nameof(CurrentFilmDescription));
            }
        }

        private void Film_Info_FilmChangedHandler()
        {
            CurrentReviewUrl = SelectedFilm_FilmInfo.trailer_url;
            CurrentFilmDescription = _database.FilmDescription.Single(fd => fd.id_film == SelectedFilm_FilmInfo.id).content;
        }

        #endregion

        public RelayCommand ToOrderCommand { get; }
        public RelayCommand GetDistanceCommand { get; }
    }
}
