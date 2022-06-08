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

        private ObservableCollection<VinWindow> _clients;

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
