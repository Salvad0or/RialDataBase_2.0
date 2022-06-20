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
        private string _exception;
        private ObservableCollection<VinWindow> _clients;
        private VinWindow _clientAfterSearch;
        private string _phoneSearch;
        private int _addCashBack;
        private int _spendCashBack;

        #endregion

        #region Свойства окна добавления клиентов

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
            get

            {
                if (String.IsNullOrEmpty(_phone))
                {
                    return "";
                }
                else
                    return _phone;
            
            }
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

        public string Exception 
        {  
            get => _exception;

            set
            {
                if (Equals(_exception, value)) return;

                _exception = value;

                OnPropertyChanged();                          
            }           
        }

        #endregion

        #region Свойства окна работы с клиентом и кешбеком

       
        public bool Flag { get; set; }
        
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

        public VinWindow ClientAfterSearh
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

        private string _editSearchPhone;

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

        private VinWindow _editClient;

        public VinWindow EditClient
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



        #endregion

        #region Основные свойства
        public ObservableCollection<VinWindow> Clients
        {
            get 
                
            {
                return _clients;
            }
             
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

        #region Команда добавления клиента

        public ICommand AddClientCommand { get; }

        public bool CanTestCommandExecuted(object p)
        {

            for (int i = 0; i < Clients.Count; i++)
            {
                if (Equals(Phone, Clients[i].Phone))
                {
                    Exception = "Такой клиент\nприсутствует в базе";        
                    return false;
                }
            }

            if (Phone.Length == 11 && (long.TryParse(Phone, out long t)))
            {
               
                Exception = String.Empty; 
                return true;
                
            }

            return false;
            
        }


        public void OnTestCommandExecuted(object p)
        {

            Clients.Add(new VinWindow
            {
                Vin = Vin,
                Name = Name,
                Phone = Phone,
                Car = Car,
                Oil = Oil,
                OilFilter = OilFilter,
                AirFilter = AirFilter,
                SalonFilter = SalonFilter,
                CashBack = CashBack / 100,
                Ngk = Ngk,
                Padsfront = Padsfront,
                Padsrear = Padsrear,
                Fuelfilter = Fuelfilter,
                Comment = Comment,
                Date = DateTime.Now.ToString("dd.MM.yy"),
                TotalPurchaseAmount = CashBack

            }) ;

            ClearWindow();

            MessageBox.Show($"Клиент {Name} успешно добавлен");

            

        }

        #endregion

        #region Команда поиска клиента

        public ICommand SearchClientCommand { get; }
        public bool CanSearchClientExecutrd(object p)

        {
            if (PhoneSearch.Length < 11)
                return false;
  
            return true;

        }
        public void OnSearchClientExecute(object p) 
        {
            ClientAfterSearh = Clients.FirstOrDefault(x => x.Phone == PhoneSearch);

            if (ClientAfterSearh == null)
            {
                MessageBox.Show("Клиент не найден, попробуйте еще раз");
                Flag = false;
                return;
            }
            Flag = true;
           
        }



        #endregion
        
        #region Команда добавления кешбека

        public ICommand AddCashBackCommand { get; }

        public bool CanAddCasbackExecuted(object p)
        {
            if (PhoneSearch.Length >= 11 && Flag)
                return true;

            return false;
        }

        public void OnAddCashBackExecuted(object p)
        {
            int index = Clients.IndexOf(ClientAfterSearh);


            Clients[index].TotalPurchaseAmount += AddCashBack;


            switch (Clients[index].TotalPurchaseAmount)
            {
                case > 200_000:

                    if (Clients[index].Status == StatusEnum.Vip)
                    {
                        break;
                    }

                    Clients[index].Status = StatusEnum.Vip;

                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{Clients[index].Name} получил VIP статус!" +
                        $"\n Кешбек равен 4%!");

                    break;

                case > 100_000:

                    if (Clients[index].Status == StatusEnum.Gold)
                    {
                        break;
                    }

                    Clients[index].Status = StatusEnum.Gold;

                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{Clients[index].Name} получил GOLD статус!" +
                        $"\n Кешбек равен 3%!");           
                    break;

                case > 30_000:

                    if (Clients[index].Status == StatusEnum.Silver)
                    {
                        break;
                    }

                    Clients[index].Status = StatusEnum.Silver;

                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{Clients[index].Name} получил Silver статус!" +
                        $"\n Кешбек равен 2%!");               
                    break;
            }


            int _cash = 0;

            switch (Clients[index].Status)
            {
                case StatusEnum.Standart:
                    _cash = AddCashBack / 100;
                    Clients[index].CashBack += _cash;
                    break;
                case StatusEnum.Silver:
                    _cash = AddCashBack / 100 * 2;
                    Clients[index].CashBack += _cash;
                    break;
                case StatusEnum.Gold:
                    _cash = AddCashBack / 100 * 3;
                    Clients[index].CashBack += _cash;
                    break;
                case StatusEnum.Vip:
                    _cash = AddCashBack / 100 * 4;
                    Clients[index].CashBack += _cash;
                    break;  
            }   

            DataWorker.SavesData(Clients);

            MessageBox.Show($"Клиенту {ClientAfterSearh.Name} был добавлен кешбек\nВ размере {_cash} рублей\n" +
                            $"Накопленная сумма составляет {Clients[index].CashBack}");

            ClientAfterSearh = default;
            AddCashBack = default;
            PhoneSearch = default;
            Flag = false;

        }



        #endregion

        #region Команда списывания кешбека

        public ICommand SpendСashback { get; }

        public bool CanSpendCashBackExecuted(object p)
        {
            if (PhoneSearch.Length >= 11 && Flag)
                return true; 
            
            return false;
        }

        public void OnSpendCashBackExecute(object p) 

        {
            int index = Clients.IndexOf(ClientAfterSearh);

            bool canSpand = Clients[index].CashBack - SpendCashBack >= 0;

            if (!canSpand)
            {
                MessageBox.Show($"У клиента {ClientAfterSearh.Name} недостаточно средств\n" +
                    $"Баланс - {ClientAfterSearh.CashBack}\n" +
                    $"Вы хотите списать сумму - {SpendCashBack}");
                return;
            }

            Clients[index].CashBack -= Math.Abs(SpendCashBack);

            DataWorker.SavesData(Clients);

            MessageBox.Show($"Кешбек клиента {ClientAfterSearh.Name} успешно списан\nНа балансе осталось {Clients[index].CashBack}");

            ClientAfterSearh = default;
            SpendCashBack = default;
            PhoneSearch = default;
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
            EditClient = Clients.FirstOrDefault(x => x.Phone == EditSearchPhone);

            if (EditClient == null)
            {
                MessageBox.Show("Клиент не найден");
                return;
            }


        }

        #endregion

        #region Команда редактирования клиента

        public ICommand EditClientDataCommand { get; }

        public bool CanEditClientDataExecuted(object p) => true;

        public void OnEditClientDataExecute(object p)

        {
            Clients.Remove(EditClient);

            Clients.Add(EditClient);
           
            DataWorker.SavesData(Clients);

            MessageBox.Show("Данные успешно изменены");

            EditClient = default;
            EditSearchPhone = default;



        }

        #endregion

        #endregion

        #region Конструктор
        public MainWindowViewModel()
        {
           
            AddClientCommand = new LambaCommand(OnTestCommandExecuted, CanTestCommandExecuted);
            SearchClientCommand = new LambaCommand(OnSearchClientExecute, CanSearchClientExecutrd);
            AddCashBackCommand = new LambaCommand(OnAddCashBackExecuted, CanAddCasbackExecuted);
            SpendСashback = new LambaCommand(OnSpendCashBackExecute, CanSpendCashBackExecuted);
            EditClientDataCommand = new LambaCommand(OnEditClientDataExecute, CanEditClientDataExecuted);
            SearchEditClientDataCommand = new LambaCommand(OnSearchEditClientDataExecuted, CanSearchEditClientDataExecuted);

            Clients = DataWorker.LoadData();

            Clients.CollectionChanged += Clients_CollectionChanged;
            
        }
        #endregion

        #region Вспомогательные методы

        private void Clients_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    DataWorker.SavesData(Clients);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    DataWorker.SavesData(Clients);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        public void ClearWindow()
        {
            Vin = default;
            Name = default;
            Phone = default;
            Car = default;
            Oil = default;
            OilFilter = default;
            AirFilter = default;
            SalonFilter = default;
            CashBack = default;
            Ngk = default;
            Padsfront = default;
            Padsrear = default;
            Fuelfilter = default;
            Comment = default;
            Date = default;
        }

        #endregion

    }
}
