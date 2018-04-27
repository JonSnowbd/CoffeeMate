using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoffeeMate
{
    /// <summary>
    /// Interaction logic for AlternateGUI.xaml
    /// </summary>
    public partial class AlternateGUI : Window
    {
        private List<string> greetings = new List<string>()
        {
            "Sit down and have a cuppa",
            "Enjoy your brew, just for you",
            "It's nice and hot",
            "Persistently consistent",
            "Niceeeee",
            "Grab your favorite mug",
            "That smells delicious",
            "Remember to pour high over the cup to oxidize coffee",
            "Dark roast or light roast?",
            "Strong bodied",
            "Start your day right"
        };

        public AlternateGUI()
        {
            Closing += Shutdown;
            this.DataContext = new CoffeeViewModel();
            InitializeComponent();

            Random r = new Random();
            this.Title = "CoffeeMate - " + greetings[r.Next(greetings.Count)];

            this.AddGroundButton.Click += TryCreateGround;
            this.DeleteGroundButton.Click += TryDeleteGround;
        }

        private void Shutdown(object sender, CancelEventArgs e)
        {
            CoffeeViewModel x = (CoffeeViewModel)this.DataContext;
            Persistence.Save(x.GroundList);
        }

        private void TryCreateGround(object s, RoutedEventArgs e)
        {
            CoffeeViewModel x = (CoffeeViewModel)this.DataContext;
            x.GroundList.Add(new GroundModel() { Parent = x });
            x.NotifyChange("GroundList");
        }
        private void TryDeleteGround(object s, RoutedEventArgs e)
        {
            CoffeeViewModel x = (CoffeeViewModel)this.DataContext;
            if (x.GroundList.Count <= 1)
                return;

            int current_index = x.GroundList.IndexOf(x.CurrentGround);
            x.GroundList.Remove(x.CurrentGround);
            
            x.CurrentGround = x.GroundList[current_index-1 < 0? 0 : current_index -1];
        }
    }

}
