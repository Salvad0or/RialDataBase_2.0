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

        SqlDataAdapter dataAdapter;    

        public DataSet DataSet
        {
            get { return _dataset; }
            set { _dataset = value; }
        }


        public DataWorker()
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(stringBuilder.ToString()))
                {

                    string command = "SELECT * FROM RialDataBase";

                    dataAdapter = new SqlDataAdapter(command, sql);
                    DataSet = new DataSet();

                    dataAdapter.Fill(DataSet);
                }
            }

            catch (Exception e)
            {

                MessageBox.Show($"Ошибка : {e.Message}\nОбратитесь к создателю");
            }
        }

    }
}
