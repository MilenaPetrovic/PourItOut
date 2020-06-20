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
        public ScrollView scroll;

        public PlayersList(List<string> list)
        {
            InitializeComponent();
            Players = list;

            // Creating layout                   
            grid = new Grid();
            var metrics = DeviceDisplay.MainDisplayInfo;
            var height = metrics.Height; //Screen height
            var rowHeight = height / (Players.Count * 20) > 70 ? height / (Players.Count * 20) : 70;

            // Creating layout items
            int headerHeight = 60;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(headerHeight) });
            int rowNum = 1;
               
            // Page label
            Label lbl = new Label
            {
                FontSize = 18,
                TextColor = Color.FromHex("#E9FFFF"),
                FontAttributes = FontAttributes.Bold,
                Text = "Click on a player to remove them!",
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 15, 0, 0)
            };
            grid.Children.Add(lbl);

            foreach (string p in Players)
            {
                Button btn = new Button
                {
                    Text = p,
                    TextColor = Color.FromHex("#E9FFFF"),
                    FontSize = 15,
                    BackgroundColor = Color.FromHex("#141414"),
                    CornerRadius = 5,
                    BorderWidth = 1,
                    BorderColor = Color.FromHex("#B203AE"),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,                             
                };                
                //btn.GestureRecognizers.Add(new TapGestureRecognizer((view) => RemovePlayer(btn)));
                btn.Clicked += RemovePlayer2;

                // Updating the grid
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
                grid.Children.Add(btn, 0, rowNum++);
            }

            //Content = grid;
            scroll = new ScrollView { Content = grid};
            Content = scroll;
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