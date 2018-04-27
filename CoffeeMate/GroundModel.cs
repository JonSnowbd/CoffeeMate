using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMate
{
    public class GroundModel : INotifyPropertyChanged
    {
        private string _name = "Generic Ground";
        public string Name {
            get { return _name; }
            set { _name = value; NotifyChange("Name"); Parent?.NotifyChange("CurrentGround"); }
        }

        private string _note = "Generic Ground";
        public string Notes
        {
            get { return _note; }
            set { _note = value; NotifyChange("Notes"); }
        }


        private float _gcoffee = 1f;
        public float GramsOfCoffee
        {
            get { return _gcoffee; }
            set { _gcoffee = value; NotifyChange("GramsOfCoffee"); Parent?.NotifyChange("TotalCoffeeNeeded"); }
        }
        private float _gwater = 10f;
        public float GramsOfWater
        {
            get { return _gwater; }
            set { _gwater = value; NotifyChange("GramsOfWater"); Parent?.NotifyChange("TotalCoffeeNeeded"); }
        }

        [JsonIgnore]
        public CoffeeViewModel Parent;

        [JsonIgnore]
        public float Ratio {
            get{
                return (GramsOfCoffee / GramsOfWater) * (1+Parent.CurrentStrength);
            }
        }
        
        public void NotifyChange(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
