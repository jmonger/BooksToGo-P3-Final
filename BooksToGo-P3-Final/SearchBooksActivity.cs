using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Json;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace BooksToGo_P3_Final
{
    [Activity(Label = "SearchBooksActivity")]
    public class SearchBooksActivity : Activity
    {

        LoggedInUser U;
        List<SimpleBook> simpleBookList;
        EditText searchObject;
        private List<string> bookDisplayList;
        private ListView booksListView;
       // ArrayAdapter<string> adapter;

        //private List<Tuple<string, string>> titleAuthor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchBooksPage);


            var JsonString = Intent.GetStringExtra("User");
            var UserObject = JsonConvert.DeserializeObject<LoggedInUser>(JsonString);

            U = UserObject;

            Button searchButton = FindViewById<Button>(Resource.Id.searchPageSearchButton);
            Button homeButton = FindViewById<Button>(Resource.Id.searchPageHomeButton);
            searchObject = FindViewById<EditText>(Resource.Id.searchText);
            booksListView = FindViewById<ListView>(Resource.Id.booksList);

 

            searchButton.Click += async (IntentSender, e) =>
            {
                string url = "http://73.87.111.140:8000/books";
                JsonValue jsonBooks = await FetchBooksAsync(url);
                simpleBookList = SearchUser(jsonBooks);


                var adapter = new SimpleListItem2Adapter(this, simpleBookList);
                //adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, titleAuthor);
                booksListView.Adapter = adapter;

                Intent nextActivity = new Intent(this, typeof(BookList));

                //var serializedBooks = JsonConvert.SerializeObject(simpleBookList);
                //nextActivity.PutExtra("Books", serializedBooks);
                //StartActivity(nextActivity);
            };
            homeButton.Click += HomeButton_Click;



            // Create your application here
        }
        private List<SimpleBook> SearchUser(JsonValue json)
        {
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());

            string temp = searchObject.Text.ToString();
            string query = temp.ToLower();


            //JsonValue userData = json["objects"];

            string title;
            string author;
            string genre;
            bookDisplayList = new List<string>();
            List<SimpleBook> simpleBookList = new List<SimpleBook>();

           // authors = new List<string>();
            //titles = new List<string>();

            foreach (var obj in jsonObj.objects)
            {
               title = obj.Title;
               string sanitizedTitle = title.ToLower();

                author = obj.Author;
                string sanitizedAuthor = author.ToLower();

                genre = obj.Genre;
              string sanitizedGenre = genre.ToLower();

                
                

               

                if (sanitizedTitle.Contains(query) || sanitizedAuthor.Contains(query) || sanitizedGenre.Contains(query))
                {
                    SimpleBook newSimpleBook = new SimpleBook();
                    newSimpleBook.Title = title;
                    newSimpleBook.Author = author;
                    simpleBookList.Add(newSimpleBook);
                    //titleAuthor.Add(new Tuple<string, string>(title, author));
                }
            }


            return simpleBookList;

        }
        private async Task<JsonValue> FetchBooksAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {


                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            var serializedUser = JsonConvert.SerializeObject(U);
            Intent nextActivity = new Intent(this, typeof(HomeScreenActivity));
            nextActivity.PutExtra("User", serializedUser);
            StartActivity(nextActivity);
        }


    }

    public class SimpleBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}