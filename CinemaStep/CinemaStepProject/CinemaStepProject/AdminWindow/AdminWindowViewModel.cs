using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib;
using MWVMLib;
using System.Data.Entity;

namespace AdminWindow
{
    class AdminWindowViewModel : ViewModelBase
    {
        //private fields
        //public fields and properties
        //constructor
        //private methods
        //public methods

        private CinemaEntities _database;

        public AdminWindowViewModel()
        {
            _database = new CinemaEntities();

            #region EditCityWindowInitializing
            _database.Cities.Load();
            Cities = _database.Cities.Local;
            AddCityCommand = new RelayCommand(AddCityCommandExecute, AddCityCommandCanExecute);
            CancelAddingCityCommand = new RelayCommand(CancelAddingCityCommandExecute, null);
            RemoveCityCommand = new RelayCommand(RemoveCityCommandExecute, RemoveCityCommandCanExecute);
            CancelRemovingCityCommand = new RelayCommand(CancelRemovingCityCommandExecute, null);
            CitiesToDelete = new List<DatabaseLib.Cities>();
            CityToAdd = string.Empty;
            #endregion

            #region EditCinemaWindowInitializing
            _database.Cinemas.Load();
            Cinemas = _database.Cinemas.Local;
            CinemasToDelete = new ObservableCollection<DatabaseLib.Cinemas>();

            AddCinemaCommand = new RelayCommand(AddCinemaCommandExecute, AddCinemaCommandCanExecute);
            CancelAddingCinemaCommand = new RelayCommand(CancelAddingCinemaCommandExecute, null);
            RemoveCinemaCommand = new RelayCommand(RemoveCinemaCommandExecute, RemoveCinemaCommandCanExecute);
            CancelRemovingCinemaCommand = new RelayCommand(CancelRemovingCinemaCommandExecute, null);

            TitleOfCinemaToAdd = string.Empty;
            AddressOfCinemaToAdd = string.Empty;
            #endregion

            #region EditFilmWindowInitializing
                        _database.Films.Load();
                        Films = _database.Films.Local;
                        FilmsToDelete = new ObservableCollection<Films>();
                        CategoriesOfFilmToAdd = new List<CategoriesOfFilmToAdd_cb>();
                        FillCategoriesEditFilmWindow();            
                       

                        TitleOfFilmToAdd = string.Empty;
                        ImageUrlOfFilmToAdd = string.Empty;
                        TrailerUrlOfFilmToAdd = string.Empty;
                        FilmDescriptionToAdd = string.Empty;

                        AddFilmCommand = new RelayCommand(AddFilmCommandExecute, AddFilmCommandCanExecute);
                        CancelAddingFilmCommand = new RelayCommand(CancelAddingFilmCommandExecute, null);
                        RemoveFilmCommand = new RelayCommand(RemoveFilmCommandExecute, RemoveFilmCommandCanExecute);
                        CancelRemovingFilmCommand = new RelayCommand(CancelRemovingFilmCommandExecute, null);
            #endregion

            #region EditCategoryWindowInitializing
            _database.Categories.Load();
            Categories = _database.Categories.Local;
            CategoriesToDelete = new ObservableCollection<DatabaseLib.Categories>();
            CategoryToAdd = string.Empty;

            AddCategoryCommand = new RelayCommand(AddCategoryCommandExecute, AddCategoryCommandCanExecute);
            CancelAddingCategoryCommand = new RelayCommand(CancelAddingCategoryCommandExecute, null);
            RemoveCategoryCommand = new RelayCommand(RemoveCategoryCommandExecute, RemoveCategoryCommandCanExecute);
            CancelRemovingCategoryCommand = new RelayCommand (CancelRemovingCategoryCommandExecute, null);
            #endregion

            #region EditSessionWindowInitializing
            _database.Hall.Load();
            _database.MovieSession.Load();
            Halls = _database.Hall.Local;
            Sessions = _database.MovieSession.Local;
            SessionsToDelete = new ObservableCollection<MovieSession>();
            SelectedDateSession = DateTime.Now;

            AddSessionCommand = new RelayCommand(AddSessionCommandExecute, AddSessionCommandCanExecute);
            CancelAddingSessionCommand = new RelayCommand(CancelAddingSessionCommandExecute, null);
            RemoveSessionCommand = new RelayCommand(RemoveSessionCommandExecute, RemoveSessionCommandCanExecute);
            CancelRemovingSessionCommand = new RelayCommand(CancelRemovingSessionCommandExecute, null);
            #endregion

            DatabaseSaveChangesCommand = new RelayCommand(DatabaseSaveChangesCommandExecute, null);
        }

        //method for CommandInitialization


        #region EditCityWindow
        private bool RemoveCityCommandCanExecute(object obj) => SelectedCityToDelete != null ? true : false;

        private void RemoveCityCommandExecute(object obj)
        {
            _database.Cities.Remove(SelectedCityToDelete);
            DatabaseSaveChangesCommandExecute(null);
        }

        private void CancelRemovingCityCommandExecute(object obj) => SelectedCityToDelete = null;

        private void CancelAddingCityCommandExecute(object obj) => CityToAdd = string.Empty;

        private bool AddCityCommandCanExecute(object obj) => (CityToAdd != string.Empty && CityToAdd.Length <= 15) ? true : false;

        private void AddCityCommandExecute(object obj)
        {
            var city = new Cities { title = CityToAdd };
            _database.Cities.Add(city);
            CitiesToDelete.Add(city);
            DatabaseSaveChangesCommandExecute(null);
        }

        private string _cityToAdd;

        public string CityToAdd
        {
            get { return _cityToAdd; }
            set { _cityToAdd = value;
                OnPropertyChanged(nameof(CityToAdd));
            }
        }


        private Cities _selectedcitytodelete;

        public Cities SelectedCityToDelete
        {
            get { return _selectedcitytodelete; }
            set { _selectedcitytodelete = value;
                OnPropertyChanged(nameof(SelectedCityToDelete));
            }
        }


        private ObservableCollection<Cities> _cities;

        public ObservableCollection<Cities> Cities
        {
            get { return _cities; }
            set { _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private List<Cities> _citiestodel;

        public List<Cities> CitiesToDelete
        {
            get { return _citiestodel; }
            set { _citiestodel = value; }
        }

        private Cities _selectedCity;

        public Cities SelectedCity
        {
            get { return _selectedCity; }
            set { _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }



        public RelayCommand AddCityCommand { get; }
        public RelayCommand CancelAddingCityCommand { get; }
        public RelayCommand RemoveCityCommand { get; }
        public RelayCommand CancelRemovingCityCommand { get; }
        #endregion
        public RelayCommand AddCinemaCommand { get; }
        public RelayCommand CancelAddingCinemaCommand { get; }
        public RelayCommand RemoveCinemaCommand { get; }
        public RelayCommand CancelRemovingCinemaCommand { get; }

        #region EditCinemaWindow


        private void RemoveCinemaCommandExecute(object obj)
        {
            _database.Cinemas.Remove(SelectedCinemaToDelete);
            DatabaseSaveChangesCommandExecute(null);
        }

        private bool RemoveCinemaCommandCanExecute(object obj) => (SelectedCinemaToDelete != null) ? true : false;

        private void CancelRemovingCinemaCommandExecute(object obj) => SelectedCinemaToDelete = null;

        private void CancelAddingCinemaCommandExecute(object obj)
        {
            TitleOfCinemaToAdd = string.Empty;
            AddressOfCinemaToAdd = string.Empty;
        }

        private bool AddCinemaCommandCanExecute(object obj) => (TitleOfCinemaToAdd != null && TitleOfCinemaToAdd.Length <= 15 && AddressOfCinemaToAdd != string.Empty && SelectedCity != null) ? true : false;

        private void AddCinemaCommandExecute(object obj)
        {
            Cinemas cinema = new DatabaseLib.Cinemas { title = TitleOfCinemaToAdd, address = AddressOfCinemaToAdd, id_city = SelectedCity.id };
            _database.Cinemas.Add(cinema);
            CinemasToDelete.Add(cinema);

            DatabaseSaveChangesCommandExecute(null);
        }

        private string _titleofcinematoadd;

        public string TitleOfCinemaToAdd
        {
            get { return _titleofcinematoadd; }
            set { _titleofcinematoadd = value;
                OnPropertyChanged(nameof(TitleOfCinemaToAdd));
            }
        }

        private string _addressofCinematoAdd;

        public string AddressOfCinemaToAdd
        {
            get { return _addressofCinematoAdd; }
            set { _addressofCinematoAdd = value;
                OnPropertyChanged(nameof(AddressOfCinemaToAdd));
            }
        }

        private Cinemas _selectedcinematodelete;

        public Cinemas SelectedCinemaToDelete
        {
            get { return _selectedcinematodelete; }
            set { _selectedcinematodelete = value;
                OnPropertyChanged(nameof(SelectedCinemaToDelete));
            }
        }


        private ObservableCollection<Cinemas> _cinemasToDelete;

        public ObservableCollection<Cinemas> CinemasToDelete
        {
            get { return _cinemasToDelete; }
            set { _cinemasToDelete = value;
                OnPropertyChanged(nameof(CinemasToDelete));
            }
        }

        private ObservableCollection<Cinemas> _cinemas;

        public ObservableCollection<Cinemas> Cinemas
        {
            get { return _cinemas; }
            set { _cinemas = value; }
        }

        #endregion

        #region EditFilmWindow
        private string _titleofFilmToAdd;

        public string TitleOfFilmToAdd
        {
            get { return _titleofFilmToAdd; }
            set {
                _titleofFilmToAdd = value;
                OnPropertyChanged(nameof(TitleOfFilmToAdd));
            }
        }

        private string _filmDescriptionToAdd;

        public string FilmDescriptionToAdd
        {
            get { return _filmDescriptionToAdd; }
            set { _filmDescriptionToAdd = value;
                OnPropertyChanged(nameof(FilmDescriptionToAdd));
            }
        }


        private string _trailerOfFilmToAdd;

        public string TrailerUrlOfFilmToAdd
        {
            get { return _trailerOfFilmToAdd; }
            set { _trailerOfFilmToAdd = value;
                OnPropertyChanged(nameof(TrailerUrlOfFilmToAdd));
            }
        }

        public RelayCommand AddFilmCommand { get; }
        public RelayCommand CancelAddingFilmCommand { get; }
        public RelayCommand RemoveFilmCommand { get; }
        public RelayCommand CancelRemovingFilmCommand { get; }
        public RelayCommand SaveEditingFilmCommand { get; }

        private string _imageUrlOfFilmToAdd;

        public string ImageUrlOfFilmToAdd
        {
            get { return _imageUrlOfFilmToAdd; }
            set { _imageUrlOfFilmToAdd = value;
                OnPropertyChanged(nameof(ImageUrlOfFilmToAdd));
            }
        }

        private ObservableCollection<Films> _filmsToDelete;

        public ObservableCollection<Films> FilmsToDelete
        {
            get { return _filmsToDelete; }
            set { _filmsToDelete = value;
                OnPropertyChanged(nameof(FilmsToDelete));
            }
        }

        private List<CategoriesOfFilmToAdd_cb> _categoriesOfFilmToAdd;

        public List<CategoriesOfFilmToAdd_cb> CategoriesOfFilmToAdd
        {
            get { return _categoriesOfFilmToAdd; }
            set { _categoriesOfFilmToAdd = value;
                OnPropertyChanged(nameof(CategoriesOfFilmToAdd));
            }
        }


        private Films _selectedFilmToDelete;

        public Films SelectedFilmToDelete
        {
            get { return _selectedFilmToDelete; }
            set { _selectedFilmToDelete = value;
                OnPropertyChanged(nameof(SelectedFilmToDelete));
            }
        }

        private ObservableCollection<Films> _films;

        public ObservableCollection<Films> Films
        {
            get { return _films; }
            set { _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }

        private void CancelRemovingFilmCommandExecute(object obj) => SelectedFilmToDelete = null;

        private bool RemoveFilmCommandCanExecute(object obj) => SelectedFilmToDelete != null ? true : false;

        private void RemoveFilmCommandExecute(object obj)
        {
            _database.FilmDescription.Remove(_database.FilmDescription.Single(fd => fd.id_film == SelectedFilmToDelete.id));
            _database.FilmCategories.RemoveRange(_database.FilmCategories.Where(fc => fc.id_film == SelectedFilmToDelete.id));

            _database.Films.Remove(SelectedFilmToDelete);
            DatabaseSaveChangesCommandExecute(null);
        }

        private void CancelAddingFilmCommandExecute(object obj)
        {
            TitleOfCinemaToAdd = string.Empty;
            TrailerUrlOfFilmToAdd = string.Empty;
            ImageUrlOfFilmToAdd = string.Empty;
        }

        private bool AddFilmCommandCanExecute(object obj) => TrailerUrlOfFilmToAdd != string.Empty &&
            (TitleOfFilmToAdd != string.Empty && TitleOfFilmToAdd.Length <= 30) &&
            (FilmDescriptionToAdd != string.Empty) &&
            (ImageUrlOfFilmToAdd != string.Empty) && CategoriesOfFilmToAdd.Any(c => c.Is_Chosen == true)
            ? true : false;

        private void AddFilmCommandExecute(object obj)
        {
            Films film = new Films { title = TitleOfFilmToAdd, image_url = ImageUrlOfFilmToAdd, trailer_url = TrailerUrlOfFilmToAdd };
            _database.Films.Add(film);
            FilmsToDelete.Add(film);

            CategoriesOfFilmToAdd.ForEach((c) => {
                if (c.Is_Chosen)
                    _database.FilmCategories.Add(new FilmCategories
                    {
                        id_film = film.id,
                        id_category = c.Category.id
                    });
            }
            );

            _database.FilmDescription.Add(new FilmDescription { id_film = film.id, content = FilmDescriptionToAdd });

            DatabaseSaveChangesCommandExecute(null);
        }

        #endregion
      
        #region EditCategoryWindow
        private string _categoryToAdd;

        public string CategoryToAdd
        {
            get { return _categoryToAdd; }
            set { _categoryToAdd = value;
                OnPropertyChanged(nameof(CategoryToAdd));
            }
        }

        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand CancelAddingCategoryCommand { get; }
        public RelayCommand RemoveCategoryCommand { get; }
        public RelayCommand CancelRemovingCategoryCommand { get; }
        public RelayCommand SaveEditingCategoryCommand { get; }

        private ObservableCollection<Categories> _categoriestoDelte;

        public ObservableCollection<Categories> CategoriesToDelete
        {
            get { return _categoriestoDelte; }
            set { _categoriestoDelte = value;
            OnPropertyChanged(nameof(CategoriesToDelete)); }
        }

        private Categories _selectedCategorytoDelete;

        public Categories SelectedCategoryToDelete
        {
            get { return _selectedCategorytoDelete; }
            set { _selectedCategorytoDelete = value;
                OnPropertyChanged(nameof(SelectedCategoryToDelete));
            }
        }

        private ObservableCollection<Categories> _categories;

        public ObservableCollection<Categories> Categories
        {
            get { return _categories; }
            set { _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }


        private void FillCategoriesEditFilmWindow()
        {
            foreach (var c in _database.Categories)
            {
                CategoriesOfFilmToAdd.Add(new CategoriesOfFilmToAdd_cb(c));
            }
        }

       

        private void CancelRemovingCategoryCommandExecute(object obj) => SelectedCategoryToDelete = null;

        private bool RemoveCategoryCommandCanExecute(object obj) => SelectedCategoryToDelete != null ? true : false;

        private void RemoveCategoryCommandExecute(object obj)
        {
            _database.Categories.Remove(SelectedCategoryToDelete);
            DatabaseSaveChangesCommandExecute(null);
        }

        private void CancelAddingCategoryCommandExecute(object obj) => CategoryToAdd = string.Empty;

        private bool AddCategoryCommandCanExecute(object obj) => CategoryToAdd != string.Empty && CategoryToAdd.Length <= 15 ? true : false;

        private void AddCategoryCommandExecute(object obj)
        {
            Categories c = new DatabaseLib.Categories { title = CategoryToAdd };
            _database.Categories.Add(c);
            CategoriesToDelete.Add(c);
            DatabaseSaveChangesCommandExecute(null);
        }
        #endregion

        #region EditSessionWindow

        private void CancelRemovingSessionCommandExecute(object obj) => SelectedSessionToDelete = null;

        private bool RemoveSessionCommandCanExecute(object obj) => SelectedSessionToDelete != null ? true : false;

        private void RemoveSessionCommandExecute(object obj)
        {
            _database.MovieSession.Remove(SelectedSessionToDelete);
            DatabaseSaveChangesCommandExecute(null);
        }

        private void CancelAddingSessionCommandExecute(object obj)
        {
            SelectedFilmSession = null;
            SelectedDateSession = DateTime.Now;
            SelectedCinemaSession = null;
            SelectedHallSession = null;
        }

        private bool AddSessionCommandCanExecute(object obj) =>
            SelectedCinemaSession != null &&
            SelectedFilmSession != null &&
            SelectedDateSession != null &&
            SelectedHallSession != null ?
             true : false;

        private void AddSessionCommandExecute(object obj)
        {
            MovieSession ms = new MovieSession
            {
                id_cinema = SelectedCinemaSession.id,
                id_film = SelectedFilmSession.id,
                id_hall = SelectedHallSession.id,
                date = SelectedDateSession
            };
            _database.MovieSession.Add(ms);
            SessionsToDelete.Add(ms);
            DatabaseSaveChangesCommandExecute(null);
        }


        private Cinemas _selectedCinemaSession;

        public Cinemas SelectedCinemaSession
        {
            get { return _selectedCinemaSession; }
            set { _selectedCinemaSession = value;
                OnPropertyChanged(nameof(SelectedCinemaSession));
            }
        }

        private Hall _SelectedHallSession;

        public Hall SelectedHallSession
        {
            get { return _SelectedHallSession; }
            set { _SelectedHallSession = value;
                OnPropertyChanged(nameof(SelectedHallSession));
            }
        }

        private Films _SelectedFilmSession;

        public Films SelectedFilmSession
        {
            get { return _SelectedFilmSession; }
            set { _SelectedFilmSession = value;
                OnPropertyChanged(nameof(SelectedFilmSession));
            }
        }

        private DateTime _SelectedDateSession;

        public DateTime SelectedDateSession
        {
            get { return _SelectedDateSession; }
            set { _SelectedDateSession = value;
                OnPropertyChanged(nameof(SelectedDateSession));
            }
        }

        private MovieSession _SelectedSessionToDelete;

        public MovieSession SelectedSessionToDelete
        {
            get { return _SelectedSessionToDelete; }
            set { _SelectedSessionToDelete = value;
                OnPropertyChanged(nameof(SelectedSessionToDelete));
            }
        }

        private ObservableCollection<MovieSession> _sessions;

        public ObservableCollection<MovieSession> Sessions
        {
            get { return _sessions; }
            set { _sessions = value;
                OnPropertyChanged(nameof(Sessions));
            }
        }

        private ObservableCollection<MovieSession> _sessionsToDelete;

        public ObservableCollection<MovieSession> SessionsToDelete
        {
            get { return _sessionsToDelete; }
            set { _sessionsToDelete = value;
                OnPropertyChanged(nameof(SessionsToDelete));
            }
        }

        private ObservableCollection<Hall> _halls;

        public ObservableCollection<Hall> Halls
        {
            get { return _halls; }
            set { _halls = value;
                OnPropertyChanged(nameof(Halls));
            }
        }

        #endregion

        private void DatabaseSaveChangesCommandExecute(object obj) => _database.SaveChanges();

        public RelayCommand AddSessionCommand { get; }
        public RelayCommand CancelAddingSessionCommand { get; }
        public RelayCommand RemoveSessionCommand { get; }
        public RelayCommand CancelRemovingSessionCommand { get; }
        public RelayCommand DatabaseSaveChangesCommand { get; private set; }
    }
}
