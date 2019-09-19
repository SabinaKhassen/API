using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using ConsoleApplication1.Entities;

namespace ConsoleApplication1
{
    class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Get();

            Console.ReadKey();
        }

        static void Delete(int id)
        {
            string url = "http://localhost:63551//api/AuthorsApi/" + $"{id}";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "DELETE";

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string answer = sr.ReadToEnd();
                Console.WriteLine(answer);
            }
        }

        static void Post()
        {
            var newAuthor = new Author { FirstName = "Author from app", LastName = "Last name from app" };

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:63551//api/AuthorsApi/");
            request.ContentType = "application/json";
            request.Method = "POST";
            var authorJson = JsonConvert.SerializeObject(newAuthor);

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(authorJson);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string answer = sr.ReadToEnd();
                Console.WriteLine(answer);
            }
        }

        static void Get()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:63551//api/AuthorsApi/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string answer = stream.ReadToEnd();
                object authors = JsonConvert.DeserializeObject<List<Author>>(answer);

            }
        }
    }
}
