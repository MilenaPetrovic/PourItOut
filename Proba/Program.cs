using Newtonsoft.Json;
using PourItOut.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    class Program
    {
        static void Main(string[] args)
        {
            Porba();
        }

        private static void Porba()
        {
            try
            {
                //WebRequest request = WebRequest.Create("https://localhost:44319/api/questions/");
                WebRequest request = WebRequest.Create("https://192.168.0.18:45457/api/questions");
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

                List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(json);
                Console.WriteLine(questions[0].Text);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
    }
}
