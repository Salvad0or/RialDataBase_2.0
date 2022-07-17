using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Data;

namespace RialDataBase_2._0.Services
{
    public class DataWorker
    {
        private SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
        { 
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "ITVDN2db",
            IntegratedSecurity = true,
            Pooling = true
        };
        private DataSet _dataset;
        private SqlCommand _searchCommand;
        private SqlDataAdapter dataAdapter;

        public SqlDataAdapter DataAdapter
        {
            get => dataAdapter;
            set => dataAdapter = value;
        }

        public DataSet DataSet
        {
            get { return _dataset; }
            set { _dataset = value; }
        }
       
        SqlCommand searchCommand
        {
            get =>  _searchCommand;
            set => _searchCommand = value;
        }

        public DataWorker()
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(stringBuilder.ToString()))
                {

                    string command = "SELECT * FROM RialDataBase";

                    DataAdapter = new SqlDataAdapter(command, sql);
                    DataSet = new DataSet();

                    DataAdapter.Fill(DataSet);


                    #region insertCommand
                    DataAdapter.InsertCommand = new SqlCommand(@"INSERT INTO [RialDataBase] " +
                                                                                            "(vin,Names,Phone,Car,Oil,OilFilter,AirFilter,SalonFilter,CashBack,Ngk,Padfront,Padsrear,Fuelfilter,Comment,TotalPurchaseAmount,Dates,Statuss" +
                                    "VALUES" +
                                    "@vin,@names,@phone,@car,@oil,@oilfilter,@airfilter,@salonfilter,@cashback,@ngk,@padsfront,@padsrear,@fuelfilter," +
                                    "@comment,@totalpurchaseamount,@dates,@statuss",sql);

                    #endregion


                    #region searchCommand

                    string _searchCommandText = $"SELECT * FROM RialDataBase WHERE Phone = '@phone'";

                    searchCommand = new SqlCommand(_searchCommandText,sql);


                    #endregion
                }
            }

            catch (Exception e)
            {

                MessageBox.Show($"Ошибка : {e.Message}\nОбратитесь к создателю");
            }

            
                

        }


        #region Запрос на добавление клиента

        public void InsertCommand(string vin, string name,string phone, string car,string oil,string oilfilter,string airfilter,
            string salonfilter,int CashBack, string Ngk, string PadsFront,string PadsRear,string fuelfilter, string comment,
            int Total, string status = "")


        {

            try
            {
                DataAdapter.InsertCommand.Parameters.AddWithValue("@vin", vin);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@names", name);
                DataAdapter.InsertCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 11, phone);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@car", car);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@oil", oil);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@oilfilter", oilfilter);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@airfilter", airfilter);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@salonfilter", salonfilter);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@cashback", CashBack);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@ngk", Ngk);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@padsfront", PadsFront);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@padsrear", PadsRear);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@fuelfilter", fuelfilter);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@comment", comment);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@totalpurchaseamount", Total);
                DataAdapter.InsertCommand.Parameters.AddWithValue("@dates", DateTime.Now.ToShortDateString());
                DataAdapter.InsertCommand.Parameters.AddWithValue("@statuss", status);

                dataAdapter.InsertCommand.BeginExecuteNonQuery();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
           
        }

        public bool SearchClient(string phone)
        {

            bool a = searchCommand.

            return true;
        }

        #endregion

    }
}
