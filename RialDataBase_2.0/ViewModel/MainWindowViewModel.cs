﻿using RialDataBase_2._0.EntityClasses.SqlCommands;
using RialDataBase_2._0.Infrasrtucture;
using RialDataBase_2._0.Model;
using RialDataBase_2._0.Services;
using RialDataBase_2._0.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
 
        private string _phoneSearch;
        private int _addCashBack;
        private int _spendCashBack;        
        private bool _flagForEditClient;
        private string _editSearchPhone;

        private EntityClient _editClient;
        private EntityClient _implicitClone;
        private EntityClient _clientfromsecondwindow;
        private DataWorker _dataWorkersql;
        private DataTable _data;
        private InsertCommands _insert;
        private EntityClient _clientAfterSearch;

        #endregion

        #region Основные свойства


        public InsertCommands Insert
        {
            get { return _insert; }
            set 
            {
                _insert = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойства окна добавления клиентов

        private EntityClient _newClient;

        public EntityClient NewClient
        {
            get => _newClient;
            set 
            {
                _newClient = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойства окна работы с клиентом и кешбеком


        /// <summary>
        /// Клиент второго окна
        /// </summary>
        public EntityClient ClientFromSecondWindow
        {
            get => _clientfromsecondwindow;
            set
            {
                _clientfromsecondwindow = value;
                OnPropertyChanged();
            }
        }

        private bool _flag;
        public bool Flag { get => _flag; set { _flag = value; } }
        
        public int SpendCashBack
        {
            get => _spendCashBack;

            set
            {
                if (Equals(_spendCashBack, value)) return;

                _spendCashBack = value;

                OnPropertyChanged();
                
            }
        }

        public int AddCashBack
        {
            get
            {
                return _addCashBack;
            }
            set 
            {
                if (Equals(_addCashBack, value)) return;
                _addCashBack = value;
                OnPropertyChanged();
                
            }
        }

        public EntityClient ClientAfterSearh
        {
            get { return _clientAfterSearch; }
            set 
            {
                if (Equals(_clientAfterSearch, value)) return;
                _clientAfterSearch = value;
                OnPropertyChanged();          
            }
        }
        public string PhoneSearch
        {
            get
            {
                if (String.IsNullOrEmpty(_phoneSearch))
                {
                    return "";
                }
                else
                    return _phoneSearch;
            }
            set
            {
                if (Equals(_phoneSearch, value)) return;

                _phoneSearch = value;
                OnPropertyChanged();         
            }
        }

        #endregion

        #region Свойтсва окна редактирования клиента

        public EntityClient ImlicitClone
        {
            get => _implicitClone; 

            set => _implicitClone = value;
        }



        public string EditSearchPhone
        {
            get
            {
                if (String.IsNullOrEmpty(_editSearchPhone))
                {
                    return "";
                }

                return _editSearchPhone;
            }
            set
            {
                if (Equals(_editSearchPhone, value)) return;
                _editSearchPhone = value;
                OnPropertyChanged();
                
            }
        }

       

        public EntityClient EditClient
        {
            get { return _editClient; }
            set
            {
                if (Equals(_editClient, value))
                    return;
                _editClient = value;
                OnPropertyChanged();
            }
        }


        

        public bool FlagForEditClient
        {
            get { return _flagForEditClient; }
            set { _flagForEditClient = value; }
        }



        #endregion

        #region Основные свойства
        

        public DataWorker DataWorkerSql
        {
            get { return _dataWorkersql; }
            set { _dataWorkersql = value; }
        }

        public DataTable MainDataForViewModel
        {
            get { return _data; }
            set
            {
                if (Equals(_data, value)) return;
                _data = value;
                OnPropertyChanged();

            }
        }
        #endregion

        #endregion

        #region Команды

        #region Команда добавления клиента

        public ICommand AddClientCommand { get; }

        public bool CanAddClient(object p) => Inspector.SearchClient(NewClient.Phone);

        public void OnAddClient (object p)
        {

            Insert.InsertNewClient(NewClient);

            NewClient = default;
        }

        #endregion

        #region Команда поиска клиента во втором окне

        public ICommand SearchClientCommand { get; }
        public bool CanSearchClientExecutrd(object p)

        {

            ClientFromSecondWindow.Phone ??= String.Empty;

            if ((ClientFromSecondWindow.Phone ??= String.Empty).Length < 11) return false;

            return true;
        }
        public void OnSearchClientExecute(object p)        
            => ClientFromSecondWindow = ClassWorker.FillSecondWindowClient(ClientFromSecondWindow.Phone, ref _flag);
        
            
        #endregion

        #region Команда добавления кешбека

        public ICommand AddCashBackCommand { get; }

        public bool CanAddCasbackExecuted(object p)
        {
            if (ClientFromSecondWindow.Phone.Length >= 11 && Flag) return true;

            return false;
        }

        public void OnAddCashBackExecuted(object p)
        {

            UpdateCommands.AddCashBack(ClientFromSecondWindow,AddCashBack);

            ClientFromSecondWindow = new EntityClient();

            AddCashBack = 0;

            Flag = false;

        }



        #endregion
   
        #region Команда списывания кешбека

        public ICommand SpendСashback { get; }

        public bool CanSpendCashBackExecuted(object p)
        {
            if (
                ClientFromSecondWindow.Phone.Length >= 11 && 
                Flag && 
                SpendCashBack > 0 &&
                ClientFromSecondWindow.CashBack >= SpendCashBack) return true;

            return false;       
        }

        public void OnSpendCashBackExecute(object p) 

        {

            UpdateCommands.SpendCashBack(ClientFromSecondWindow, SpendCashBack);

            ClientFromSecondWindow = new EntityClient();

            SpendCashBack = 0;

            Flag = false;
        }

        #endregion

        #region Команда поиска для редактирования клиента
        public ICommand SearchEditClientDataCommand { get; }

        public bool CanSearchEditClientDataExecuted(object p)
        {
            if (EditSearchPhone.Length >= 11)
                return true;
         
            return false;
            

        }

        public void OnSearchEditClientDataExecuted(object p)
        {
            if (!DataWorkerSql.SearchClientForCashBackWindow(EditSearchPhone))
            {
                MessageBox.Show("Клиент не найден, попробуйте еще раз");
                return;
            }

            EditClient = DataWorkerSql.FillsClass(EditSearchPhone);
            ImlicitClone = (EntityClient)EditClient.Clone();

            FlagForEditClient = true;
        }

        #endregion

        #region Команда редактирования клиента

        public ICommand EditClientDataCommand { get; }

        public bool CanEditClientDataExecuted(object p)
        {
            if (FlagForEditClient) return true;

            return false;
            
        }

        public void OnEditClientDataExecute(object p)

        {

            DataWorkerSql.ChangesDataOfClient(EditClient, EditSearchPhone, ImlicitClone);

            MessageBox.Show("Данные успешно изменены");

            EditClient = default;
            EditSearchPhone = default;

        }

        #endregion

        #endregion

        #region Конструктор
        public MainWindowViewModel()
        {

            #region Команды
            AddClientCommand = new LambaCommand(OnAddClient, CanAddClient);
            SearchClientCommand = new LambaCommand(OnSearchClientExecute, CanSearchClientExecutrd);
            AddCashBackCommand = new LambaCommand(OnAddCashBackExecuted, CanAddCasbackExecuted);
            SpendСashback = new LambaCommand(OnSpendCashBackExecute, CanSpendCashBackExecuted);
            EditClientDataCommand = new LambaCommand(OnEditClientDataExecute, CanEditClientDataExecuted);
            SearchEditClientDataCommand = new LambaCommand(OnSearchEditClientDataExecuted, CanSearchEditClientDataExecuted);
            #endregion

            NewClient = new EntityClient();
            ClientFromSecondWindow = new EntityClient();
            Insert = new InsertCommands();

            DataWorkerSql = new DataWorker();

            MainDataForViewModel = DataWorker.DataSetTable.Tables[0];
            
        }
        #endregion

        

    }
}
