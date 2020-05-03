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
    public partial class GameplayPage : ContentPage
    {

        List<string> players;
        List<string> questions;

        public GameplayPage(List<string> players)
        {
            this.players = players;
            FillQuestions();

            InitializeComponent();

            AskMe(null, null);
        }

        private void FillQuestions()
        {
            questions = new List<string> { "Kako si brt?", "Sta ima novo?", "Jesu dobro kuci?" };
        }

        private void AskMe(object sender, EventArgs e)
        {
            Random r = new Random();

            if (btnReady.Text == "Next player!")
            {
                int num = r.Next(0, players.Count);
                string chosenP = players[num];

                lbl1.Text = $"It's {chosenP}'s turn!";
                btnReady.Text = "I'm ready!";
            }
            else
            {
                int num = r.Next(0, questions.Count);
                string chosenQ = questions[num];

                lbl1.Text = $"{chosenQ}";
                btnReady.Text = "Next player!";
            }
        }
    }
}