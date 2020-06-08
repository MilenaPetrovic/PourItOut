using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PourItOut.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace PourItOut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayersList : ContentPage
    {
        public List<string> Players { get; set; }
        public Grid grid;

        public PlayersList(List<string> list)
        {
            InitializeComponent();
            Players = list;

            // Creating layout
            grid = new Grid();
            grid.BackgroundColor = Color.FromHex("#133C55");
            var metrics = DeviceDisplay.MainDisplayInfo;
            var height = metrics.Height; //Screen height
            var rowHeight = height / (Players.Count * 20) > 70 ? height / (Players.Count * 20) : 70;

            // Creating layout items
            int headerHeight = 60;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(headerHeight) });
            int rowNum = 1;
            foreach (string p in Players)
            {
                Button btn = new Button
                {
                    Text = p,
                    TextColor = Color.FromHex("#E9FFFF"),
                    BackgroundColor = Color.FromHex("#59A5D8"),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    
             
                };

                //btn.GestureRecognizers.Add(new TapGestureRecognizer((view) => RemovePlayer(btn)));
                btn.Clicked += RemovePlayer2;

                // Updating the grid
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
                grid.Children.Add(btn, 0, rowNum++);
            }

            Content = grid;
        }

        public void AddPlayer(string p)
        {
            Players.Add(p);
        }

        public void RemovePlayer(Button btn)
        {
            grid.Children.Remove(btn);
            Players.Remove(btn.Text);
        }

        public void RemovePlayer2(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            grid.Children.Remove(btn);
            Players.Remove(btn.Text);
        }
    }
}