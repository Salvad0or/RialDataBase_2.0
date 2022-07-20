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

        #region private поля

        private SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
        { 
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "ITVDN2db",
            IntegratedSecurity = true,
            Pooling = true
        };

        private SqlConnection sql;
        private SqlCommand _searchCommand;
        private SqlDataAdapter dataAdapter;
        private DataTable _dataTable;

        #endregion

        #region public поля
        public SqlConnection Sql 
        { 
            get => sql; 
            set => sql = value; 
        }

        public SqlDataAdapter DataAdapter
        {
            get => dataAdapter;
            set => dataAdapter = value;
        }

        public static DataSet DataSetTable;


        public SqlCommand SearchCommand
        {
            get =>  _searchCommand;
            set => _searchCommand = value;
        }

        public DataTable TableForSearch
        {
            get => _dataTable;
            set => _dataTable = value;
        }

        #endregion


        /// <summary>
        /// Дефолтный конструктор
        /// </summary>
        public DataWorker()
        {

            try
            {
                using (Sql = new SqlConnection(stringBuilder.ToString()))
                {
                    string command = "SELECT * FROM RialDataBase";
                    Sql.Open();

                    DataAdapter = new SqlDataAdapter(command, Sql);
                    DataSetTable = new DataSet();
                    TableForSearch = new DataTable();
                    
                    DataAdapter.Fill(DataSetTable);

                    TableForSearch = DataSetTable.Tables[0];

                }
            }

            catch (Exception e)
            {

                MessageBox.Show($"Ошибка : {e.Message}\n");
            }                  
        }

        /// <summary>
        /// Команда Insert на добавление клиента
        /// </summary>
        /// <param name="vin"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="car"></param>
        /// <param name="oil"></param>
        /// <param name="oilfilter"></param>
        /// <param name="airfilter"></param>
        /// <param name="salonfilter"></param>
        /// <param name="CashBack"></param>
        /// <param name="Ngk"></param>
        /// <param name="PadsFront"></param>
        /// <param name="PadsRear"></param>
        /// <param name="fuelfilter"></param>
        /// <param name="comment"></param>
        /// <param name="Total"></param>
        /// <param name="status"></param>
        public void InsertCommand(string vin, string name,string phone, string car,string oil,string oilfilter,string airfilter,
            string salonfilter,int CashBack, string Ngk, string PadsFront,string PadsRear,string fuelfilter, string comment,
            int Total, string status = "")

        {          
            try
            {

                using (Sql = new SqlConnection(stringBuilder.ToString()))
                {
                    Sql.Open();
                    
                    string command = "SELECT * FROM RialDataBase";

                    SqlDataAdapter adapter = new SqlDataAdapter(command, Sql);

                    adapter.InsertCommand = new SqlCommand(@"INSERT INTO [RialDataBase] " +
                                                                                                "(vin,Names,Phone,Car,Oil,OilFilter,AirFilter,SalonFilter,CashBack,Ngk,Padsfront,Padsrear,Fuelfilter,Comment,TotalPurchaseAmount,Dates,Statuss)" +
                                    "VALUES" +
                                    "(@vin,@names,@phone,@car,@oil,@oilfilter,@airfilter,@salonfilter,@cashback,@ngk,@padsfront,@padsrear,@fuelfilter," +
                                    "@comment,@totalpurchaseamount,@dates,@statuss)", Sql);

                    adapter.InsertCommand.Parameters.AddWithValue("@vin", vin ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@names", name ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@phone", phone);
                    adapter.InsertCommand.Parameters.AddWithValue("@car", car ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@oil", oil ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@oilfilter", oilfilter ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@airfilter", airfilter ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@salonfilter", salonfilter ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@cashback", CashBack);
                    adapter.InsertCommand.Parameters.AddWithValue("@ngk", Ngk ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@padsfront", PadsFront ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@padsrear", PadsRear ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@fuelfilter", fuelfilter ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@comment", comment ??= String.Empty);
                    adapter.InsertCommand.Parameters.AddWithValue("@totalpurchaseamount", Total);
                    adapter.InsertCommand.Parameters.AddWithValue("@dates", DateTime.Now.ToString("dd.MM.yy"));
                    adapter.InsertCommand.Parameters.AddWithValue("@statuss", StatusEnum.Standart.ToString());

                    adapter.InsertCommand.ExecuteNonQuery();

                    DataSetTable.Clear();

                    adapter.Fill(DataSetTable);

                }
            }
            catch (Exception e)
            {

                MessageBox.Show($"Ошибка: {e.Message}");     
            }
           
        }


        /// <summary>
        /// Поиск клиента по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>

        public bool SearchClientForAddClient(string phone)
        {

            IEnumerable<DataRow> query =
                from order in TableForSearch.AsEnumerable()
                select order;


            IEnumerable<DataRow> query2 =  query.Where(p => p.Field<string>("Phone") == phone);

            if (query2.ToList().Count == 0)
            {
                return true;
            }

            return false;


        }

        public bool SearchClientForCashBackWindow(string phone)
        {
            IEnumerable<DataRow> query =
                from order in TableForSearch.AsEnumerable()
                select order;

            IEnumerable<DataRow> query2 = query.Where(p => p.Field<string>("Phone") == phone);

            if (query2.ToList().Count == 0) return false;
          
            return true;

        }

        
    }
}
