using Newtonsoft.Json;
using PourItOut.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PourItOut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameplayPage : ContentPage
    {
        static readonly HttpClient client = new HttpClient();

        string chosenP1 = null;
        string chosenP2 = null;

        List<string> players;
        //List<Question> questions;
        List<string> questions;

        Dictionary<string, List<string>> questionDict;

        //player shuffler
        int[] playerOrder;
        int playerPointer = 0;
        Random rnd = new Random();

        //two player counter
        int questionCounter = 1;
        int specialQuestionFrequency = 5;

        public GameplayPage(List<string> players)
        {
            this.players = players;

            //player shuffle
            playerOrder = new int[players.Count];
            for (int i = 0; i < players.Count; i++)
            {
                playerOrder[i] = i;
            }
            playerOrder = playerOrder.OrderBy(x => rnd.Next()).ToArray();

            // non-repetable question checker
            questionDict = new Dictionary<string, List<string>>();
            for (int i = 0; i < players.Count; i++)
            {
                questionDict.Add(players[i], new List<string>());
            }

            FillQuestions();
            InitializeComponent();
            AskMe(null, null);
        }        

        private void AskMe(object sender, EventArgs e)
        {  
            // End of the game, question pool used up
            if (questionCounter >= questions.Count * players.Count * 0.9)
            {
                lbl1.Text = $"You ran out of questions. Thank you for your playing!";
                btnReady.Text = "Main menu";
                // Returning to the main menu
            }

            // Shuffling players' turns
            if(playerPointer >= players.Count)
            {
                playerOrder = playerOrder.OrderBy(x => rnd.Next()).ToArray();
                playerPointer = 0;
            }

            // Returning to main menu
            if(btnReady.Text == "Main menu")
            {
                Navigation.PopAsync();
                Navigation.PopAsync();
            }

            // Choosing players
            if (btnReady.Text == "Next player!")
            {
                if (questionCounter % specialQuestionFrequency == 0)
                {
                    chosenP1 = players[playerOrder[playerPointer++]];

                    if (playerPointer >= players.Count)
                    {
                        playerOrder = playerOrder.OrderBy(x => rnd.Next()).ToArray();
                        playerPointer = 0;
                    }

                    chosenP2 = players[playerOrder[playerPointer++]];
                    if (chosenP2 == chosenP1)
                        chosenP2 = players[playerOrder[playerPointer++]];

                    var formattedString = new FormattedString();
                    formattedString.Spans.Add(new Span
                    {
                        Text = "Multiple players turn!\nIt is ",
                        ForegroundColor = Color.White
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = $"{chosenP1}",
                        ForegroundColor = Color.FromHex("#84D2F6")
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = "'s and ",
                        ForegroundColor = Color.White
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = $"{chosenP2}",
                        ForegroundColor = Color.FromHex("#84D2F6")
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = "'s turn!",
                        ForegroundColor = Color.White
                    });

                    //lbl1.Text = $"Multiple players turn!\nIt's {chosenP1}'s and {chosenP2}'s turn!";
                    lbl1.FormattedText = formattedString;
                    btnReady.Text = "I'm ready!";
                }
                else  // Choosing question
                {
                    chosenP1 = players[playerOrder[playerPointer++]];

                    var formattedString = new FormattedString();
                    formattedString.Spans.Add(new Span
                    {
                        Text = "It is ",
                        ForegroundColor = Color.White
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = $"{chosenP1}",
                        ForegroundColor = Color.FromHex("#84D2F6")
                    });
                    formattedString.Spans.Add(new Span
                    {
                        Text = "'s turn!",
                        ForegroundColor = Color.White
                    });

                    //lbl1.Text = $"It's {chosenP}'s turn!";
                    lbl1.FormattedText = formattedString;
                    btnReady.Text = "I'm ready!";
                }
            }
            else
            {
                string chosenQ = null;
                do
                {
                    int num = rnd.Next(0, questions.Count);
                    //string chosenQ = questions[num].Text;
                    chosenQ = questions[num];
                }
                while (
                    (chosenP1 != null && questionDict[chosenP1].Contains(chosenQ)) || 
                    (chosenP2 != null && questionDict[chosenP2].Contains(chosenQ))
                );
                questionCounter++;
                questionDict[chosenP1].Add(chosenQ);
                if(chosenP2 != null)
                {
                    questionDict[chosenP2].Add(chosenQ);
                }

                lbl1.Text = $"{chosenQ}";
                btnReady.Text = "Next player!";

                // returning players' variables to defaults
                chosenP1 = null;
                chosenP2 = null;
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

            /*
            try
            {
                WebRequest request = WebRequest.Create("https://localhost:44319/api/questions/");
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
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            */
        }
    }
}