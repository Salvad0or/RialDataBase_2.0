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
using System.Reflection;

namespace RialDataBase_2._0.Services
{
    public class DataWorker
    {

        #region private поля

        private SqlConnectionStringBuilder _stringBuilder;
        private SqlConnection sql;
        private SqlCommand _searchCommand;
        private SqlDataAdapter dataAdapter;
        private DataTable _dataTable;

        #endregion

        #region public свойства

        public SqlConnectionStringBuilder StringBuilder
        {
            get => _stringBuilder;
            private set => _stringBuilder = value;
        }
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

            StringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"192.168.0.104, 1994",
                InitialCatalog = "DBTest",
                UserID = "sa",
                Password = "12345",
                Encrypt = false,
                Pooling = true
            };       

            try
            {
                

                using (Sql = new SqlConnection(StringBuilder.ToString()))
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

                using (Sql = new SqlConnection(StringBuilder.ToString()))
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

        /// <summary>
        /// Поиск клиента для окна кешбека
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool SearchClientForCashBackWindow(string phone)
        {
            IEnumerable<DataRow> query =
                from order in TableForSearch.AsEnumerable()
                select order;

            IEnumerable<DataRow> query2 = query.Where(p => p.Field<string>("Phone") == phone);

            if (query2.ToList().Count == 0) return false;
          
            return true;
        }

        /// <summary>
        /// Заполнения класса по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public VinWindow FillsClass(string phone)
        {
            VinWindow ClientAferSearch = new VinWindow();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
                {
                    
                    sqlConnection.Open();
                    string command = "SELECT * FROM RialDataBase WHERE Phone = @phone";

                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@phone", phone);

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ClientAferSearch.Vin = reader["vin"] as string;
                        ClientAferSearch.Name = reader["Names"] as string;
                        ClientAferSearch.Phone = reader["Phone"] as string;
                        ClientAferSearch.Car = reader["Car"] as string;
                        ClientAferSearch.Oil = reader["Oil"] as string; 
                        ClientAferSearch.OilFilter = reader["OilFilter"] as string;
                        ClientAferSearch.AirFilter = reader["AirFilter"] as string;
                        ClientAferSearch.SalonFilter = reader["SalonFilter"] as string;
                        ClientAferSearch.CashBack = (reader["CashBack"] as int?).GetValueOrDefault();
                        ClientAferSearch.Ngk = reader["Ngk"] as string;
                        ClientAferSearch.Padsfront = reader["Padsfront"] as string;
                        ClientAferSearch.Padsrear = reader["Padsrear"] as string;
                        ClientAferSearch.Fuelfilter = reader["Fuelfilter"] as string;
                        ClientAferSearch.Comment = reader["Comment"] as string;
                        ClientAferSearch.TotalPurchaseAmount = (reader["TotalPurchaseAmount"] as int?).GetValueOrDefault();
                        ClientAferSearch.Date = reader["Dates"] as string;


                        switch (reader["Statuss"] as string)
                        {
                            case "Standart":
                                ClientAferSearch.Status = StatusEnum.Standart;
                                break;
                            case "Silver":
                                ClientAferSearch.Status = StatusEnum.Silver;
                                break;
                            case "Gold":
                                ClientAferSearch.Status = StatusEnum.Gold;
                                break;
                            case "Vip":
                                ClientAferSearch.Status = StatusEnum.Vip;
                                break;
                            default:
                                break;
                        }
                    }         
                }

                return ClientAferSearch;

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Изменение кешбека
        /// </summary>
        /// <param name="a"></param>
        /// <param name="ClientAfterSearch"></param>
        public void UpdateCashBackCommand(int a,VinWindow ClientAfterSearch)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
                {

                    sqlConnection.Open();

                    string command = "UPDATE [RialDataBase] SET CashBack = @cashBack WHERE Phone = @phone";
                    string commandForSqlAdapter = "Select * FROM RialDataBase";

                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandForSqlAdapter, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@cashBack", ClientAfterSearch.CashBack);
                    sqlCommand.Parameters.AddWithValue("@phone", ClientAfterSearch.Phone);
                    
                    sqlCommand.ExecuteNonQuery();

                    DataSetTable.Clear();
                    sqlDataAdapter.Fill(DataSetTable);

                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Изменение статуса клиента
        /// </summary>
        /// <param name="ClientAfterSearch"></param>
        public void ChangeStatusUpdateCommand(VinWindow ClientAfterSearch)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
                {

                    sqlConnection.Open();

                    string command = "UPDATE [RialDataBase] SET Statuss = @status WHERE Phone = @phone";
                    string commandForSqlAdapter = "Select * FROM RialDataBase";

                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandForSqlAdapter, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@status", ClientAfterSearch.Status.ToString());
                    sqlCommand.Parameters.AddWithValue("@phone", ClientAfterSearch.Phone);

                    sqlCommand.ExecuteNonQuery();

                    DataSetTable.Clear();
                    sqlDataAdapter.Fill(DataSetTable);

                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Редактирование клиента
        /// </summary>
        /// <param name="EditClient"></param>
        /// <param name="EditSearchPhone"></param>
        public void ChangesDataOfClient(VinWindow EditClient,string EditSearchPhone, VinWindow ImlicitClone)
        {
          
            #region Команда Update and Select
            string UpdateCommand = "UPDATE RialDataBase SET vin = @vin," +
                "Names = @names," +
                "Car = @car," +
                "Oil = @oil," +
                "OilFilter = @oilfilter," +
                "AirFilter = @airfilter," +
                "SalonFilter = @salonfilter," +
                "Ngk = @ngk," +
                "Padsfront = @padsfront," +
                "Padsrear = @padsrear," +
                "Phone = @editphone," +
                "Fuelfilter = @fuelfilter," +
                "Comment = @comment WHERE Phone = @phone";

            

            string SelectCommand = "SELECT * FROM RialDataBase";
            #endregion

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
                {
                    sqlConnection.Open();


                    

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectCommand, sqlConnection);
                    SqlCommand sqlCommand = new SqlCommand(UpdateCommand, sqlConnection);
                    
                    sqlCommand.Parameters.AddWithValue("@vin", EditClient.Vin ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@names", EditClient.Name ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@car", EditClient.Car ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@oil", EditClient.Oil ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@oilfilter", EditClient.OilFilter ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@airfilter", EditClient.AirFilter ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@salonfilter", EditClient.SalonFilter ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@ngk", EditClient.Ngk ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@padsfront", EditClient.Padsfront ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@padsrear", EditClient.Padsrear ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@fuelfilter", EditClient.Fuelfilter ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@comment", EditClient.Comment ??= String.Empty);
                    sqlCommand.Parameters.AddWithValue("@phone", EditSearchPhone);
                    sqlCommand.Parameters.AddWithValue("@editphone", EditClient.Phone);

                    sqlCommand.ExecuteNonQuery();
        
                    DataSetTable.Clear();
                    sqlDataAdapter.Fill(DataSetTable);
         
                }

            }
            catch (Exception e)
            {

                MessageBox.Show($"Ошибка {e.Message}");
            }
        }

        
    }
}
