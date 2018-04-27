using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CoffeeMate
{
    public class CoffeeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GroundModel> GroundList { get; set; }

        private float _str = 0f;
        public float CurrentStrength {
            get { return _str; }
            set { _str = value; NotifyChange("CurrentStrength"); CurrentGround.NotifyChange("Ratio"); NotifyChange("TotalCoffeeNeeded"); }
        }

        private GroundModel _curground;
        public GroundModel CurrentGround {
            get { return _curground; }
            set {
                _curground = value;
                NotifyChange("CurrentGround"); NotifyChange("TotalCoffeeNeeded");
            }
        }

        public CoffeeViewModel()
        {
            GroundList = Persistence.GetOrCreateSettings();
            foreach (GroundModel Model in GroundList)
            {
                Model.Parent = this;
                Model.PropertyChanged += (s, e) => { NotifyChange("GroundList"); };
            }
            CurrentGround = GroundList[0];
        }

        private float _water = 250f;
        public float WaterPerCup
        {
            get { return _water; }
            set { _water = value; NotifyChange("WaterPerCup"); NotifyChange("TotalCoffeeNeeded"); }
        }

        public string TotalCoffeeNeeded
        {
            get
            {
                if (CurrentGround == null)
                    return "Awaiting selected ground.";
                return "You need " + WaterPerCup * CurrentGround.Ratio + "g of coffee";
            }
        }

        public void CreateNewGround()
        {
            GroundModel newg = new GroundModel() { Parent = this };
            newg.PropertyChanged += (s, e) => { NotifyChange("GroundList"); };
            GroundList.Add(newg);
            CurrentGround = newg;
            NotifyChange("GroundList");
            NotifyChange("CurrentGround");
        }

        public void NotifyChange(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
