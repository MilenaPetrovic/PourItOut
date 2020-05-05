using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PourItOut.Models;
using System.Collections.ObjectModel;

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
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });

            // Creating layout items
            int rowNum = 0;
            foreach (string p in Players)
            {
                Label lbl = new Label
                {
                    Text = p,
                    TextColor = Color.Black,
                    BackgroundColor = Color.LightSteelBlue,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                };


                lbl.GestureRecognizers.Add(new TapGestureRecognizer((view) => RemovePlayer(lbl)));

                // Updating the grid
                grid.Children.Add(lbl, 0, rowNum++);
            }

            Content = grid;
        }

        public void AddPlayer(string p)
        {
            Players.Add(p);
        }

        public void RemovePlayer(Label label)
        {
            grid.Children.Remove(label);
            Players.Remove(label.Text); 
        }
    }
}