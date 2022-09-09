using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.Model
{
    public class EntityClient : ICloneable
    {
        #region private поля
        private string _vin;
        private string _name;
        private string _phone;
        private string _car;
        private string _oil;
        private string _oilFilter;
        private string _airFilter;
        private string _salonFilter;
        private decimal _cashBack;
        private string _ngk;
        private string _padsfront;
        private string _padsrear;
        private string _fuelfilter;
        private string _comment;
        private DateTime _date;
        private StatusEnum _status;
        private decimal _totalPurchaseAmount;

        #endregion

        #region Свойства

        public StatusEnum Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Общая сумма потраченная клиентом.
        /// </summary>
        public decimal TotalPurchaseAmount
        {
            get { return _totalPurchaseAmount; }
            set { _totalPurchaseAmount = value; }
        }


        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }


        public string Fuelfilter
        {
            get { return _fuelfilter; }
            set { _fuelfilter = value; }
        }


        public string Padsrear
        {
            get { return _padsrear; }
            set { _padsrear = value; }
        }


        public string Padsfront
        {
            get { return _padsfront; }
            set { _padsfront = value; }
        }


        public string Ngk
        {
            get { return _ngk; }
            set { _ngk = value; }
        }


        public decimal CashBack
        {
            get { return _cashBack; }

            set
            {
                _cashBack = value;
            }
        }

        public string Vin
        {
            get { return _vin; }
            set { _vin = value; }
        }
        public string Phone
        {
            get 

            {
                if (_phone is null) return String.Empty;
                return _phone;
            }

            set { _phone = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Car
        {
            get { return _car; }
            set { _car = value; }
        }
        public string Oil
        {
            get { return _oil; }
            set { _oil = value; }
        }
        public string OilFilter
        {
            get { return _oilFilter; }
            set { _oilFilter = value; }
        }
        public string AirFilter
        {
            get { return _airFilter; }
            set { _airFilter = value; }
        }

        public string SalonFilter
        {
            get { return _salonFilter; }
            set { _salonFilter = value; }
        }

        #endregion

        #region конструкторы
        public EntityClient(string _vin, string _name, string _phone, string _car, string _oil,string _oilFilter, string _airFilter,
                        string _salonFilter, int _cashBack, string _ngk, string _padsfront, string _padsrear, string _fuelfilter,
                        string _comment, DateTime _date, int total = 0)
        {
            Vin = _vin;
            Name = _name;
            Phone = _phone;
            Car = _car;
            Oil = _oil;
            OilFilter = _oilFilter;
            AirFilter = _airFilter;
            SalonFilter = _salonFilter;
            CashBack = _cashBack;
            Ngk = _ngk;
            Padsfront = _padsfront;
            Padsrear = _padsrear;
            Fuelfilter = _fuelfilter;
            Comment = _comment;
            Date = _date;
            Status = StatusEnum.Standart;
            TotalPurchaseAmount = total;
      
        }
        
        public EntityClient()
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод клонирования
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        
        #endregion
    }
}
