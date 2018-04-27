using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMate
{
    public static class Persistence
    {
        public static readonly string CompanyName = "PySnow";
        public static readonly string AppName = "CoffeeMate";

        public static string Base
        {
            get
            {
                string x = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), CompanyName, AppName);
                Directory.CreateDirectory(x);
                return x;
            }
        }

        public static ObservableCollection<GroundModel> GetOrCreateSettings()
        {
            ObservableCollection<GroundModel> def = new ObservableCollection<GroundModel>()
            {
                new GroundModel(){GramsOfCoffee = 1, GramsOfWater = 10}
            };

            string GroundPath = Path.Combine(Base, "grounds.json");

            if (!File.Exists(GroundPath))
            {
                File.WriteAllText(GroundPath, JsonConvert.SerializeObject(def));
                return def;
            }
            else
            {
                string import = File.ReadAllText(GroundPath);
                ObservableCollection<GroundModel> imp = JsonConvert.DeserializeObject<ObservableCollection<GroundModel>>(import);

                return imp;
            }
        }
        public static void Save(ObservableCollection<GroundModel> grounds)
        {
            string GroundPath = Path.Combine(Base, "grounds.json");
            File.WriteAllText(GroundPath, JsonConvert.SerializeObject(grounds));
        }
    }
}
