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
    class TI_EditCinema_ViewModel : ViewModelBase
    {
        //private fields
        //public fields and properties
        //constructor
        //private methods
        //public methods

        private CinemaEntities _database;
        private string _titleofcinematoadd;
        private string _addressofCinematoAdd;
        private Cinemas _selectedcinematodelete;
        private ObservableCollection<Cinemas> _cinemasToDelete;
        private ObservableCollection<Cinemas> _cinemas;
        private Cities _selectedCity;


        public RelayCommand AddCinemaCommand { get; set; }
        public RelayCommand CancelAddingCinemaCommand { get; set; }
        public RelayCommand RemoveCinemaCommand { get; set; }
        public RelayCommand CancelRemovingCinemaCommand { get; set; }
        public RelayCommand DatabaseSaveChangesCommand { get; private set; }


        public string TitleOfCinemaToAdd
        {
            get { return _titleofcinematoadd; }
            set
            {
                _titleofcinematoadd = value;
                OnPropertyChanged(nameof(TitleOfCinemaToAdd));
            }
        }

        public string AddressOfCinemaToAdd
        {
            get { return _addressofCinematoAdd; }
            set
            {
                _addressofCinematoAdd = value;
                OnPropertyChanged(nameof(AddressOfCinemaToAdd));
            }
        }


        public Cinemas SelectedCinemaToDelete
        {
            get { return _selectedcinematodelete; }
            set
            {
                _selectedcinematodelete = value;
                OnPropertyChanged(nameof(SelectedCinemaToDelete));
            }
        }

        public ObservableCollection<Cinemas> CinemasToDelete
        {
            get { return _cinemasToDelete; }
            set
            {
                _cinemasToDelete = value;
                OnPropertyChanged(nameof(CinemasToDelete));
            }
        }

        public ObservableCollection<Cinemas> Cinemas
        {
            get { return _cinemas; }
            set { _cinemas = value; }
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


        public TI_EditCinema_ViewModel()
        {
            CommandInitializing();
            PropertyInitializing();
        }

        private void CommandInitializing()
        {
            AddCinemaCommand = new RelayCommand(AddCinemaCommandExecute, AddCinemaCommandCanExecute);
            CancelAddingCinemaCommand = new RelayCommand(CancelAddingCinemaCommandExecute, null);
            RemoveCinemaCommand = new RelayCommand(RemoveCinemaCommandExecute, RemoveCinemaCommandCanExecute);
            CancelRemovingCinemaCommand = new RelayCommand(CancelRemovingCinemaCommandExecute, null);
            DatabaseSaveChangesCommand = new RelayCommand(DatabaseSaveChangesCommandExecute, null);
        }

        private void DatabaseSaveChangesCommandExecute(object obj) => DatabaseSaveChanges();

        private void PropertyInitializing()
        {
            _database = new CinemaEntities();
            _database.Cinemas.Load();

            Cinemas = _database.Cinemas.Local;

            CinemasToDelete = new ObservableCollection<DatabaseLib.Cinemas>();
            TitleOfCinemaToAdd = string.Empty;
            AddressOfCinemaToAdd = string.Empty;
        }

        private void RemoveCinemaCommandExecute(object obj)
        {
            _database.Cinemas.Remove(SelectedCinemaToDelete);
            DatabaseSaveChanges();
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

            DatabaseSaveChanges();
        }

        private void DatabaseSaveChanges() => _database.SaveChanges();
    }
}
