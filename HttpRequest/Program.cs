using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HttpRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            UcitajPitanja();
        }

        private static void UcitajPitanja()
        {
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
                
                List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
