using MWVMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace AdminWindow.Views
{
    class TI_EditCity_ViewModel : ViewModelBase
    {
        //private fields
        //public fields and properties
        //constructor
        //private methods
        //public methods
        //method for CommandInitialization


        private CinemaEntities _database;
        private Cities _selectedcitytodelete;
        private ObservableCollection<Cities> _cities;
        private List<Cities> _citiestodel;
        private Cities _selectedCity;
        private string _cityToAdd;

        public RelayCommand AddCityCommand { get; set; }
        public RelayCommand CancelAddingCityCommand { get; set; }
        public RelayCommand RemoveCityCommand { get; set; }
        public RelayCommand CancelRemovingCityCommand { get; set; }
        public RelayCommand DatabaseSaveChangesCommand { get; private set; }


        public string CityToAdd
        {
            get { return _cityToAdd; }
            set
            {
                _cityToAdd = value;
                OnPropertyChanged(nameof(CityToAdd));
            }
        }

        public Cities SelectedCityToDelete
        {
            get { return _selectedcitytodelete; }
            set
            {
                _selectedcitytodelete = value;
                OnPropertyChanged(nameof(SelectedCityToDelete));
            }
        }

        public ObservableCollection<Cities> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        public List<Cities> CitiesToDelete
        {
            get { return _citiestodel; }
            set { _citiestodel = value; }
        }

        public Cities SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public TI_EditCity_ViewModel()
        {
            CommandInitializing();
            PropertiesInitializing();
        }

        private bool RemoveCityCommandCanExecute(object obj) => 
            SelectedCityToDelete != null 
            ? true : false;

        private void RemoveCityCommandExecute(object obj)
        {
            _database.Cities.Remove(SelectedCityToDelete);
            DatabaseSaveChanges();
        }

        private void CancelRemovingCityCommandExecute(object obj) => SelectedCityToDelete = null;

        private void CancelAddingCityCommandExecute(object obj) => CityToAdd = string.Empty;

        private bool AddCityCommandCanExecute(object obj) =>
            (CityToAdd != string.Empty && CityToAdd.Length <= 15) 
            ? true : false;

        private void AddCityCommandExecute(object obj)
        {
            var city = new Cities { title = CityToAdd };
            _database.Cities.Add(city);
            CitiesToDelete.Add(city);
            DatabaseSaveChanges();
        }

        private void DatabaseSaveChangesCommandExecute(object obj) => DatabaseSaveChanges();

        private void DatabaseSaveChanges() => _database.SaveChanges();

        private void CommandInitializing()
        {
            AddCityCommand = new RelayCommand(AddCityCommandExecute, AddCityCommandCanExecute);
            CancelAddingCityCommand = new RelayCommand(CancelAddingCityCommandExecute, null);
            RemoveCityCommand = new RelayCommand(RemoveCityCommandExecute, RemoveCityCommandCanExecute);
            CancelRemovingCityCommand = new RelayCommand(CancelRemovingCityCommandExecute, null);
        }

        private void PropertiesInitializing()
        {
            _database = new CinemaEntities();
            _database.Cities.Load();

            Cities = _database.Cities.Local;
            CitiesToDelete = new List<DatabaseLib.Cities>();
            CityToAdd = string.Empty;
        }

    }
}
