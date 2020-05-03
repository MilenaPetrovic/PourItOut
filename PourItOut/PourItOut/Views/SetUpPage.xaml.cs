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
            players.Add(playerName.Text);
            playerName.Text = string.Empty;
            lbl1.Text = $"Enter {players.Count + 1}. player:";
        }

        private void Gameplay(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameplayPage(players));
        }
    }
}