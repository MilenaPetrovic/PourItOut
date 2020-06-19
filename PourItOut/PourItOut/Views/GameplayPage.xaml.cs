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
    public enum Signal
    {
        ChoosePlayers,
        ChooseQuestion,
        ReturnToMainMenu
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameplayPage : ContentPage
    {
        Signal signal = Signal.ChoosePlayers;

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

        public GameplayPage(List<string> players, List<string> questions)
        {
            this.players = players;
            this.questions = questions;

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

            InitializeComponent();
            AskMe(null, null);
        }

        private void AskMe(object sender, EventArgs e)
        {
            // Returning to main menu
            if (signal == Signal.ReturnToMainMenu)
            {
                Navigation.PopAsync();
                Navigation.PopAsync();

                signal = Signal.ChoosePlayers;
                return;
            }

            // End of the game, question pool used up
            if (questionCounter >= questions.Count * players.Count * 0.9)
            {
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span
                {
                    Text = "You ran out of questions.\n\nThank you for playing!",
                    ForegroundColor = Color.White
                });
                lbl1.FormattedText = formattedString;
                btnReady.Text = "Main menu";
                signal = Signal.ReturnToMainMenu;
                return;
                // Returning to the main menu after this
            }

            // Shuffling players' turns
            if (playerPointer >= players.Count)
            {
                playerOrder = playerOrder.OrderBy(x => rnd.Next()).ToArray();
                playerPointer = 0;
            }

            // Choosing players
            if (signal == Signal.ChoosePlayers)
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
                        Text = "Multiple players turn!\n\nIt is ",
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
                }

                btnReady.Text = "Ready!";
                signal = Signal.ChooseQuestion;
            }
            else
            {
                string chosenQ = null;
                int i = 0;
                do
                {
                    i++;
                    int num = rnd.Next(0, questions.Count);
                    //chosenQ = questions[num].Text;
                    chosenQ = questions[num];

                    if (i == questions.Count * questions.Count)
                    {
                        lbl1.Text = $"You ran out of questions. Thank you for your playing!";
                        btnReady.Text = "Main menu";
                        btnReady.BackgroundColor = Color.Red;
                        signal = Signal.ReturnToMainMenu;
                        return;
                    }
                }
                while (
                    (chosenP1 != null && questionDict[chosenP1].Contains(chosenQ)) ||
                    (chosenP2 != null && questionDict[chosenP2].Contains(chosenQ))
                );
                questionCounter++;
                questionDict[chosenP1].Add(chosenQ);
                if (chosenP2 != null)
                {
                    questionDict[chosenP2].Add(chosenQ);
                }

                lbl1.Text = $"{chosenQ}";
                btnReady.Text = "Next!";

                // returning players' variables to defaults
                chosenP1 = null;
                chosenP2 = null;
                signal = Signal.ChoosePlayers;
            }
        }

    }
}