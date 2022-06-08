using RialDataBase_2._0.Infrasrtucture;
using RialDataBase_2._0.Model;
using RialDataBase_2._0.Services;
using RialDataBase_2._0.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RialDataBase_2._0.ViewModel
{
    internal class MainWindowViewModel : ViewModels
    {

        #region Поля

        #region private Поля

        private string _vin;
        private string _name;
        private string _phone;
        private string _car;
        private string _oil;
        private string _oilFilter;
        private string _airFilter;
        private string _salonFilter;
        private int _cashBack;
        private string _ngk;
        private string _padsfront;
        private string _padsrear;
        private string _fuelfilter;
        private string _comment;
        private string _date;
        private ObservableCollection<VinWindow> _clients;

        #endregion

        #region Поля окна добавления клиентов

        public string Date
        {
            get { return _date; }
            set 
            {
                if (Equals(_date, value)) return;
                _date = value;
                OnPropertyChanged();
               
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (Equals(_comment, value)) return;
                _comment = value;
                OnPropertyChanged();

            }
        }


        public string Fuelfilter
        {
            get { return _fuelfilter; }
            set
            {
                if (Equals(_fuelfilter, value)) return;
                _fuelfilter = value;
                OnPropertyChanged();

            }
        }


        public string Padsrear
        {
            get { return _padsrear; }
            set
            {
                if (Equals(_padsrear, value)) return;
                _padsrear = value;
                OnPropertyChanged();

            }
        }


        public string Padsfront
        {
            get { return _padsfront; }
            set
            {
                if (Equals(_padsfront, value)) return;
                _padsfront = value;
                OnPropertyChanged();

            }
        }


        public string Ngk
        {
            get { return _ngk; }
            set
            {
                if (Equals(_ngk, value)) return;
                _ngk = value;
                OnPropertyChanged();

            }
        }


        public int CashBack
        {
            get { return _cashBack; }
            set
            {
                if (Equals(_cashBack, value)) return;
                _cashBack = value;
                OnPropertyChanged();

            }
        }

        public string Vin
        {
            get { return _vin; }
            set
            {
                if (Equals(_vin, value)) return;
                _vin = value;
                OnPropertyChanged();

            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (Equals(_phone, value)) return;
                _phone = value;
                OnPropertyChanged();

            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) return;
                _name = value;
                OnPropertyChanged();

            }
        }
        public string Car
        {
            get { return _car; }
            set
            {
                if (Equals(_car, value)) return;
                _car = value;
                OnPropertyChanged();

            }
        }
        public string Oil
        {
            get { return _oil; }
            set
            {
                if (Equals(_oil, value)) return;
                _oil = value;
                OnPropertyChanged();

            }
        }
        public string OilFilter
        {
            get { return _oilFilter; }
            set
            {
                if (Equals(_oilFilter, value)) return;
                _oilFilter = value;
                OnPropertyChanged();

            }
        }
        public string AirFilter
        {
            get { return _airFilter; }
            set
            {
                if (Equals(_airFilter, value)) return;
                _airFilter = value;
                OnPropertyChanged();

            }
        }

        public string SalonFilter
        {
            get { return _salonFilter; }
            set
            {
                if (Equals(_salonFilter, value)) return;
                _salonFilter = value;
                OnPropertyChanged();

            }
        }

        #endregion

        #region Основные свойства
        public ObservableCollection<VinWindow> Clients
        {
            get => _clients;
            set
            {
                if (Equals(_clients, value)) return;
                _clients = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Команды

        #region

        public ICommand testCommand { get; }

        public bool CanTestCommandExecuted(object p)
        {
            return true;
        }


        public void OnTestCommandExecuted(object p)
        {
            MessageBox.Show("123");
        }

        #endregion тестовая команда

        #endregion

        #region Конструктор
        public MainWindowViewModel()
        {
            testCommand = new LambaCommand(OnTestCommandExecuted, CanTestCommandExecuted);

            Clients = DataWorker.LoadData();

        }

        #endregion
    }
}
