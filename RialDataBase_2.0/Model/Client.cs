using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.Model
{
    public class VinWindow
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
        private int _cashBack;
        private string _ngk;
        private string _padsfront;
        private string _padsrear;
        private string _fuelfilter;
        private string _comment;
        private string _date;

        #endregion

        public string Date
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


        public int CashBack
        {
            get { return _cashBack; }
            set { _cashBack = value; }
        }

        public string Vin
        {
            get { return _vin; }
            set { _vin = value; }
        }
        public string Phone
        {
            get { return _phone; }
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

        public VinWindow(string _vin, string _name, string _phone, string _car, string _oil,string _oilFilter, string _airFilter,
                        string _salonFilter, int _cashBack, string _ngk, string _padsfront, string _padsrear, string _fuelfilter,
                        string _comment, string _date)
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
      
        }
        
    }
}
