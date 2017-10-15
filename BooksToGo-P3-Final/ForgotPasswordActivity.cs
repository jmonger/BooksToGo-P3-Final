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
using System.Net.Mail;

namespace BooksToGo_P3_Final
{
    [Activity(Label = "ForgotPasswordActivity")]
    public class ForgotPasswordActivity : Activity
    {
        EditText forgottenPasswordUserEmail;
        Button getPasswordButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ForgotEmailPage);

            forgottenPasswordUserEmail = FindViewById<EditText>(Resource.Id.forgotEmail);
            getPasswordButton = FindViewById<Button>(Resource.Id.getPasswordButton);




        


            // Create your application here
        }
    }
}