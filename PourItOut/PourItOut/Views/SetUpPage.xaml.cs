using Newtonsoft.Json;
using PourItOut.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PourItOut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetUpPage : ContentPage
    {
        List<string> players;
        //List<string> questions;
        List<Question> questions;

        public SetUpPage()
        {
            players = new List<string>();
            //questions = new List<string>();
            questions = new List<Question>();
            InitializeComponent();
        }

        private async void AddPlayer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(playerName.Text) ||
                players.Any(s => s.Equals(playerName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                playerName.TextColor = Color.FromHex("#f70d1a");
                await playerName.TranslateTo(-20, 0, 50);
                await playerName.TranslateTo(+20, 0, 50);
                await playerName.TranslateTo(0, 0, 50);
                playerName.TextColor = Color.FromHex("#E9FFFF");

                return;
            }
            playerName.PlaceholderColor = Color.FromHex("#E9FFFF");
            playerName.TextColor = Color.FromHex("#E9FFFF");

            players.Add(playerName.Text);
            playerName.Text = string.Empty;
            lbl1.Text = $"Enter {players.Count + 1}. player:";
            playerName.Focus();
        }

        private async void Gameplay(object sender, EventArgs e)
        {
            if (players.Count < 2)
            {
                playerName.TextColor = Color.FromHex("#f70d1a");
                await playerName.TranslateTo(-20, 0, 50);
                await playerName.TranslateTo(+20, 0, 50);
                await playerName.TranslateTo(0, 0, 50);
                playerName.TextColor = Color.FromHex("#E9FFFF");

                return;
            }
            playerName.PlaceholderColor = Color.FromHex("#E9FFFF");
            playerName.TextColor = Color.FromHex("#E9FFFF");

            bool loaded = FillQuestions();
            if (!loaded)
            {
                DisplayAlert("Unable to load questions!", "We are sorry for the inconvenience, try again later.", "OK");
                Navigation.PopAsync();  // Without this call if you want to stay on the same page
            }
            else
            {
                Navigation.PushAsync(new GameplayPage(players, questions));
            }
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

        private bool FillQuestions()
        {
            //questions = new List<string> {
            //    "What are you most scared of?",
            //    "Is there anything you regret in your life, and what?",
            //    "Do you believe in God?",
            //    "What would you do if you won million dollars?",
            //    "When was the last time you cried?",
            //    "If your house was caught on fire, what is the one thing you would save?",
            //    "Did you ever steal something and what?",
            //    "What is the worst gift you got?",
            //    "What is the most money you spent on a gift?",
            //    "Were you ever in love with someone much older than you and who?",
            //    "What is the worst thing you lied about to get out of plans?",
            //    "Did you ever look through someone's phone?",
            //    "Have you ever wished someone is dead and who?",
            //    "Out of all the players who do you dislike most?",
            //    "Out of all the players who do you like most?"
            //};

            //return true;


            try
            {
                //WebRequest request = WebRequest.Create("https://localhost:44319/api/questions/");
                WebRequest request = WebRequest.Create("http://rdusan97-001-site1.gtempurl.com/api/questions");
                WebResponse response = request.GetResponse();

                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.

                string json;
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    json = reader.ReadToEnd();
                    // Display the content.
                }
                // Close the response.
                response.Close();

                questions = JsonConvert.DeserializeObject<List<Question>>(json);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }

        }
    }
}