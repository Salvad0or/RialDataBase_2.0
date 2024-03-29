﻿using RialDataBase_2._0.EntityClasses.SqlCommands;
using RialDataBase_2._0.HelperClasses;
using RialDataBase_2._0.Infrasrtucture;
using RialDataBase_2._0.Model;
using RialDataBase_2._0.Services;
using RialDataBase_2._0.Services.TgBot;
using RialDataBase_2._0.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace RialDataBase_2._0.ViewModel
{
    internal class MainWindowViewModel : ViewModels
    {
        #region Поля

        #region private Поля
   
        private int _addCashBack;
        private int _spendCashBack;        
        private bool _flagForEditClient;
        private string _editSearchPhone;
        private bool _flag;
        private int _promocodSum;
        private string _promocodName;
        private string _currentPromocode;
        private string _messageText;

        private EntityClient _newClient;
        private EntityClient _clientfromsecondwindow;
        private EntityClient _thirtywindowlient;

        private InsertCommands _insert;

        private ObservableCollection<EntityClient> _alljoinedclients;

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

        TheWorkerBot BotClient { get; set; }


        #endregion

        #region Свойства окна добавления клиентов   

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

        #endregion

        #region Свойтсва окна редактирования клиента

        public EntityClient ThirtyWindowClient
        {
            get { return _thirtywindowlient; }

            set
            { _thirtywindowlient = value;
                OnPropertyChanged();
            }
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

        public bool FlagForEditClient
        {
            get { return _flagForEditClient; }
            set { _flagForEditClient = value; }
        }

        #endregion

        #region Свойства окна всех клиентов

        public ObservableCollection<EntityClient> AllJoinedClients
        {
            get => _alljoinedclients;
            set
            {
                _alljoinedclients = value;
                OnPropertyChanged(); 
            }
            
        }

        
        public string CurrentPromocode
        {
            get => _currentPromocode;
            set
            {
                _currentPromocode = value;

                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства окна добавления промокода


        public string PromocodName
        {
            get { return _promocodName; }
            set 
            {
                _promocodName = value;
                _promocodName = _promocodName.Replace(" ","");

                OnPropertyChanged();
            }
        }

        public int PromocodSum
        {
            get { return _promocodSum; }
            set 
            { 
                _promocodSum = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Свойства окна рассылки массовых сообщений



        public string MessageText 
        {
            get { return _messageText; }
            set
            { 
                _messageText = value;
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

            EntityClient c = (EntityClient)NewClient.Clone();
            
            AllJoinedClients.Add(c);

            Insert.InsertNewClient(NewClient);

            NewClient = Helper.Cleaner(NewClient);


        }

        #endregion

        #region Команда поиска клиента во втором окне

        public ICommand SearchClientCommand { get; }
        public bool CanSearchClientExecutrd(object p)
            => (ClientFromSecondWindow.Phone ??= String.Empty).Length == 11;

        public void OnSearchClientExecute(object p)        
            => ClientFromSecondWindow = ClassWorker.FillClient(ClientFromSecondWindow.Phone, ref _flag);
          
        #endregion

        #region Команда добавления кешбека

        public ICommand AddCashBackCommand { get; }

        public bool CanAddCasbackExecuted(object p)
            => (ClientFromSecondWindow.Phone.Length >= 11 && Flag);

        public void OnAddCashBackExecuted(object p)
        {

            UpdateCommands.AddCashBackAsync(ClientFromSecondWindow, BotClient, AddCashBack);

            ClientFromSecondWindow = new EntityClient();

            AddCashBack = 0;

            Flag = false;

        }

        #endregion
   
        #region Команда списывания кешбека

        public ICommand SpendСashback { get; }

        public bool CanSpendCashBackExecuted(object p)   
             => (ClientFromSecondWindow.Phone.Length >= 11 && 
                Flag && 
                SpendCashBack > 0 &&
                ClientFromSecondWindow.CashBack >= SpendCashBack);

        public async void OnSpendCashBackExecute(object p) 

        {

            await Task.Run(() => UpdateCommands.SpendCashBackAsync(ClientFromSecondWindow, BotClient, SpendCashBack));

            ClientFromSecondWindow = new EntityClient();

            SpendCashBack = 0;

            Flag = false;
        }

        #endregion

        #region Команда поиска клиента для редактирования 
        public ICommand SearchEditClientDataCommand { get; }

        public bool CanSearchEditClientDataExecuted(object p)
        => (EditSearchPhone.Length == 11);

        public void OnSearchEditClientDataExecuted(object p) =>
            ThirtyWindowClient = ClassWorker.FillClient(EditSearchPhone, ref _flagForEditClient);

        #endregion

        #region Команда редактирования клиента

        public ICommand EditClientDataCommand { get; }

        public bool CanEditClientDataExecuted(object p)    
            => (EditSearchPhone.Length == 11 && FlagForEditClient);
         
        public void OnEditClientDataExecute(object p)

        {
            UpdateCommands.ChangeClientData(ThirtyWindowClient, ref _flagForEditClient, EditSearchPhone);

            ThirtyWindowClient = new EntityClient();

            EditSearchPhone = String.Empty;
        }

        #endregion

        #region Команда добавления промокода

        public ICommand ActivatePromocodeCommand { get; }

        public bool CanActivatePromocodeExecuted(object p)
        {
            if (PromocodName != null && PromocodSum > 0) return true;

            return false;
        }

        public void OnActivatePromocodeExecute(object p)
        {     
            bool flag = Insert.AddNewPromoCodeAsync(_promocodName, _promocodSum).Result;

            if(flag)
            {
                CurrentPromocode = SelectCommands.FindCurrentPromocodeAsync().Result;

                PromocodName = String.Empty;
                PromocodSum = default;
            }
            
        }

        #endregion

        #region Команда рассылки массовых сообщений

        public ICommand SendMessageToClientsCommand { get; }

        public bool CanSendMessageToClientsExecuted(object p)
        {
            return MessageText.Length > 0;
        }

        public void OnSendMessageToClientsExecuted(object p)
        {
            MessageHandler.SendTextMessageToClient(MessageText);
            MessageText = String.Empty;
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
            ActivatePromocodeCommand = new LambaCommand(OnActivatePromocodeExecute, CanActivatePromocodeExecuted);
            SendMessageToClientsCommand = new LambaCommand(OnSendMessageToClientsExecuted, CanSendMessageToClientsExecuted);

            #endregion

            #region Свойства

            MessageText = String.Empty;

            BotClient = new TheWorkerBot();
            NewClient = new EntityClient();

            ClientFromSecondWindow = new EntityClient();
            ThirtyWindowClient = new EntityClient();
            Insert = new InsertCommands();

            Task.Factory.StartNew(PrepareAllClientTablesAsync);

            CurrentPromocode = SelectCommands.FindCurrentPromocodeAsync().Result;


            #endregion

        }

        void PrepareAllClientTablesAsync()
        {
            AllJoinedClients = JoinCommands.JoinAllDataAsync();
        }

   
        #endregion
       
    }
}
