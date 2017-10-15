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

namespace BooksToGo_P3_Final
{
    [Activity(Label = "HomeScreenActivity")]
    public class HomeScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomeScreenPage);
            // Create your application here

            Button logoutButton = FindViewById<Button>(Resource.Id.homeLogoutButton);
            Button searchBooksButton = FindViewById<Button>(Resource.Id.homeSearchBooksButton);

            searchBooksButton.Click += SearchBooksButton_Click;

            logoutButton.Click += LogoutButton_Click;
        }

        private void SearchBooksButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(SearchBooksActivity));
            StartActivity(nextActivity);
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("CONFIRM LOGOUT");
            alert.SetMessage("Ready To Leave?");
            alert.SetNegativeButton("CANCEL", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("LOGOUT", (senderAlert, args) =>
            {
                this.FinishAffinity();
            });


            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}