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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Bson;


namespace BooksToGo_P3_Final
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        EditText firstNameIn;
        EditText lastNameText;
        EditText emailText;
        EditText passwordText;
        EditText confirmationPassword;


        TextView creationResponse;
        NewUser newUser = new NewUser();

        static HttpClient client = new HttpClient();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.signUpPage);

            Button submitBtn = FindViewById<Button>(Resource.Id.submitButton);
             firstNameIn = FindViewById<EditText>(Resource.Id.firstNameText);
             lastNameText = FindViewById<EditText>(Resource.Id.lastNameText);
             emailText = FindViewById<EditText>(Resource.Id.emailAddressText);
             passwordText = FindViewById<EditText>(Resource.Id.passwordText);
             confirmationPassword = FindViewById<EditText>(Resource.Id.confirmPasswordText);

            creationResponse = (TextView)FindViewById<TextView>(Resource.Id.creationResponse);




            submitBtn.Click += async (IntentSender, e) =>
            {
                string url = "http://73.87.111.140:8000/users";
                await CreateAccount(url);

                    


            };



            // Create your application here


        }

        private async Task CreateAccount(String url)
        {
            client.MaxResponseContentBufferSize = 256000;
            var uri = new Uri(url);
            string password = passwordText.Text.ToString();
            string confirmPassword = confirmationPassword.Text.ToString();

            if (verifyPassword(password, confirmPassword))
            {
                createUser();
                var json = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Account Created!");
                    alert.SetMessage("Welcome to BooksToGo! Click Login to return to the main page.");
                    alert.SetNeutralButton("LOGIN",(senderAlert,args)=>
                    {
                        Intent nextActivity = new Intent(this, typeof(MainActivity));
                        StartActivity(nextActivity);
                    });
                    

                

                    Dialog dialog = alert.Create();
                    dialog.Show();

                    

                }
            }
            else
            {
               // creationResponse.SetText(Int32.Parse("Password and Confirmation Don't Match!"));
            }
 
        }

        private bool verifyPassword(string password, string confirmationPassword)
        {
            if (password.Equals(confirmationPassword))
            {
                return true;
            }

            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("PASSWORD ERROR");
                alert.SetMessage("Password and Confirmation Password do not match. Please make correction.");
                alert.SetNeutralButton("Try Again", (senderAlert, args) =>
                {
             
                });

               



                Dialog dialog = alert.Create();
                dialog.Show();
                return false;
            }
        }

        public void createUser()
        {         
            newUser.FirstName = firstNameIn.Text.ToString();
            newUser.LastName = lastNameText.Text.ToString();
            newUser.Password = passwordText.Text.ToString();
            newUser.Email = emailText.Text.ToString();
            newUser._id =  ObjectId.GenerateNewId().ToString();
        }
        
     
        }
    

    public class NewUser
    {
        public string _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
