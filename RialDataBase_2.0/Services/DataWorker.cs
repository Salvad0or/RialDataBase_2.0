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
    public partial class DataWorker
    {

        #region private поля

        
        private SqlConnection sql;
        private SqlCommand _searchCommand;
        private SqlDataAdapter dataAdapter;
        private DataTable _dataTable;
        private readonly SqlConnectionStringBuilder StringBuilder;

        #endregion

        #region public свойства


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
                DataSource = @"192.168.1.48, 1994",
                InitialCatalog = "RialShop",
                UserID = "sa",
                Password = "rnjcerfkjvftN1",
                Encrypt = false,
                Pooling = true
            };

            //Data Source=;AttachDbFilename=C:\Users\Саша\ITVDN2db.mdf;Integrated Security=True;Connect Timeout=30

            /*DataSource = @"192.168.1.48, 1994",
            InitialCatalog = "RialShop",
                UserID = "sa",
                Password = "rnjcerfkjvftN1",
                Encrypt = false,              
                Pooling = true
            */

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

        
        
    }
}
