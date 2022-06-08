using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RialDataBase_2._0.Services
{
    public static class DataWorker
    {
        private static readonly string path = $"{Environment.CurrentDirectory}\\Data.xml";

       public static ObservableCollection<VinWindow> LoadData()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<VinWindow>));
            ObservableCollection<VinWindow> client = new ObservableCollection<VinWindow>();

            using (StreamReader sr = new StreamReader(path))
            {
                client = xmlSerializer.Deserialize(sr) as ObservableCollection<VinWindow>;   
            }

            return client;
        }
    }
}
