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

        static XmlSerializer xmlSerializer;

       public static ObservableCollection<VinWindow> LoadData()
        {
            
            ObservableCollection<VinWindow> client = new ObservableCollection<VinWindow>();

            using (StreamReader sr = new StreamReader(path))
            {
                client = (xmlSerializer.Deserialize(sr) as ObservableCollection<VinWindow>);   
            }

            return client;
        }


        public static void SavesData(ObservableCollection<VinWindow> Clients)

        {
            using (StreamWriter sr = new StreamWriter(path))
            {
                xmlSerializer.Serialize(sr, Clients);
            }

        }

        static DataWorker()
        {
           xmlSerializer = new XmlSerializer(typeof(ObservableCollection<VinWindow>));
        }
    }
}
