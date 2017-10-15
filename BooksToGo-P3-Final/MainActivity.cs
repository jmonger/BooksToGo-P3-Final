using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using System;
using System.IO;

namespace BooksToGo_P3_Final
{
    [Activity(Label = "BooksToGo_P3_Final", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText inputUserText;
        EditText inputPassText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button signUpBtn = FindViewById<Button>(Resource.Id.signUpButton);
            Button loginBtn = FindViewById<Button>(Resource.Id.loginButton);
            inputUserText = FindViewById<EditText>(Resource.Id.inputUserMain);
            inputPassText = FindViewById<EditText>(Resource.Id.inputPasswordMain);



            signUpBtn.Click += SignUpBtn_Click;

            loginBtn.Click += async (IntentSender, e) =>
            {
                string url = "http://73.87.111.140:8000/users";
                JsonValue json = await FetchUsersAsync(url);
                bool userFound = ParseAndInterpret(json);


                if (!userFound)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                     alert.SetTitle("LOGIN FAILURE");
                     alert.SetMessage("Incorrect Username and Password Combination. Please Try Again.");
                     alert.SetNegativeButton("Try Again", (senderAlert, args) =>
                     {

                     });

                     Dialog dialog = alert.Create();
                     dialog.Show();

   
                }
            };
        }

        private async Task<JsonValue> FetchUsersAsync(string url)
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
        private bool ParseAndInterpret(JsonValue json)
        {
            Intent nextActivity = new Intent(this, typeof(HomeScreenActivity));
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());

            string appUser = inputUserText.Text.ToString();
            string appPass = inputPassText.Text.ToString();

            //JsonValue userData = json["objects"];

            string checkedUser;
            string checkedPass;
            foreach (var obj in jsonObj.objects)
            {
                checkedUser = obj.Email;
                checkedPass = obj.Password;

                if (appUser.Equals(checkedUser) && appPass.Equals(checkedPass))
                {

                    StartActivity(nextActivity);
                    return true;
                    break;
                }
 
            }

            return false;

        }

        private void SignUpBtn_Click(object sender, System.EventArgs e)
        {

            Intent nextActivity = new Intent(this, typeof(SignUpActivity));
            StartActivity(nextActivity);

        }
    }
}

