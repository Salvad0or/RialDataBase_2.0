using Microsoft.Data.SqlClient;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RialDataBase_2._0.Services
{
    public partial class DataWorker
    {
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


            IEnumerable<DataRow> query2 = query.Where(p => p.Field<string>("Phone") == phone);

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
        public EntityClient FillsClass(string phone)
        {
            //EntityClient ClientAferSearch = new EntityClient();
            //try
            //{
            //    using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
            //    {

            //        sqlConnection.Open();
            //        string command = "SELECT * FROM RialDataBase WHERE Phone = @phone";

            //        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            //        sqlCommand.Parameters.AddWithValue("@phone", phone);

            //        SqlDataReader reader = sqlCommand.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            ClientAferSearch.Vin = reader["vin"] as string;
            //            ClientAferSearch.Name = reader["Names"] as string;
            //            ClientAferSearch.Phone = reader["Phone"] as string;
            //            ClientAferSearch.Car = reader["Car"] as string;
            //            ClientAferSearch.Oil = reader["Oil"] as string;
            //            ClientAferSearch.OilFilter = reader["OilFilter"] as string;
            //            ClientAferSearch.AirFilter = reader["AirFilter"] as string;
            //            ClientAferSearch.SalonFilter = reader["SalonFilter"] as string;
            //            ClientAferSearch.CashBack = (reader["CashBack"] as int?).GetValueOrDefault();
            //            ClientAferSearch.Ngk = reader["Ngk"] as string;
            //            ClientAferSearch.Padsfront = reader["Padsfront"] as string;
            //            ClientAferSearch.Padsrear = reader["Padsrear"] as string;
            //            ClientAferSearch.Fuelfilter = reader["Fuelfilter"] as string;
            //            ClientAferSearch.Comment = reader["Comment"] as string;
            //            ClientAferSearch.TotalPurchaseAmount = (reader["TotalPurchaseAmount"] as int?).GetValueOrDefault();
            //            ClientAferSearch.Date = reader["Dates"] as string;


            //            switch (reader["Statuss"] as string)
            //            {
            //                case "Standart":
            //                    ClientAferSearch.Status = StatusEnum.Standart;
            //                    break;
            //                case "Silver":
            //                    ClientAferSearch.Status = StatusEnum.Silver;
            //                    break;
            //                case "Gold":
            //                    ClientAferSearch.Status = StatusEnum.Gold;
            //                    break;
            //                case "Vip":
            //                    ClientAferSearch.Status = StatusEnum.Vip;
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //    }

            //    return ClientAferSearch;

            //}
            //catch (Exception e)
            //{

            //    MessageBox.Show(e.Message);
            //    return null;
            //}

            return new EntityClient();
        }

        /// <summary>
        /// Изменение кешбека
        /// </summary>
        /// <param name="a"></param>
        /// <param name="ClientAfterSearch"></param>
        public void UpdateCashBackCommand(int a, EntityClient ClientAfterSearch)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(StringBuilder.ToString()))
                {

                    sqlConnection.Open();

                    string command = "UPDATE [RialDataBase] SET CashBack = @cashBack," +
                        "TotalPurchaseAmount = @total WHERE Phone = @phone";
                    string commandForSqlAdapter = "Select * FROM RialDataBase";

                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandForSqlAdapter, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@cashBack", ClientAfterSearch.CashBack);
                    sqlCommand.Parameters.AddWithValue("@phone", ClientAfterSearch.Phone);
                    sqlCommand.Parameters.AddWithValue("@total", ClientAfterSearch.TotalPurchaseAmount);

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
        public void ChangeStatusUpdateCommand(EntityClient ClientAfterSearch)
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
        public void ChangesDataOfClient(EntityClient EditClient, string EditSearchPhone, EntityClient ImlicitClone)
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
