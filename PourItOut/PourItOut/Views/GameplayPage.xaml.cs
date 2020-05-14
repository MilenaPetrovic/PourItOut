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

        private void FillQuestions()
        {
            questions = new List<string> {
                "What are you most scared of?",
                "Is there anything you regret in your life, and what?",
                "Do you believe in God?",
                "What would you do if you won million dollars?",
                "When was the last time you cried?",
                "If your house was caught on fire, what is the one thing you would save?",
                "Did you ever steal something and what?",
                "What is the worst gift you got?",
                "What is the most money you spent on a gift?",
                "Were you ever in love with someone much older than you and who?",
                "What is the worst thing you lied about to get out of plans?",
                "Did you ever look through someone's phone?",
                "Have you ever wished someone is dead and who?",
                "Out of all the players who do you dislike most?",
                "Out of all the players who do you like most?"
            };
        }
    }
}