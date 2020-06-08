using PourItOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PourItOut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetUpPage : ContentPage
    {
        List<string> players = new List<string>();

        public SetUpPage()
        {
            InitializeComponent();
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(playerName.Text) ||
                players.Any(s => s.Equals(playerName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                playerName.PlaceholderColor = Color.FromHex("#133C55");
                playerName.BackgroundColor = Color.FromHex("#f70d1a"); //red
                return;
            }
            playerName.PlaceholderColor = Color.FromHex("#59A5D8"); //light blue
            playerName.BackgroundColor = Color.FromHex("#133C55");

            players.Add(playerName.Text);
            playerName.Text = string.Empty;
            lbl1.Text = $"Enter {players.Count + 1}. player:";
            playerName.Focus();
        }

        private void Gameplay(object sender, EventArgs e)
        {
            if (players.Count < 2)
            {
                playerName.PlaceholderColor = Color.FromHex("#133C55");
                playerName.BackgroundColor = Color.FromHex("#f70d1a"); //red
                return;
            }
            playerName.PlaceholderColor = Color.FromHex("#59A5D8"); //light blue
            playerName.BackgroundColor = Color.FromHex("#133C55"); 

            Navigation.PushAsync(new GameplayPage(players));
        }

        private void ShowPlayers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayersList(players));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lbl1.Text = $"Enter {players.Count + 1}. player:";
        }
    }
}