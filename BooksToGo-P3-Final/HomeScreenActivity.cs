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

namespace BooksToGo_P3_Final
{
    [Activity(Label = "HomeScreenActivity")]
    public class HomeScreenActivity : Activity
    {

        LoggedInUser U;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomeScreenPage);
            // Create your application here

            var JsonString = Intent.GetStringExtra("User");
            var UserObject = JsonConvert.DeserializeObject<LoggedInUser>(JsonString);

            U = UserObject;

            Button logoutButton = FindViewById<Button>(Resource.Id.homeLogoutButton);
            Button searchBooksButton = FindViewById<Button>(Resource.Id.homeSearchBooksButton);
            Button myAccountButton = FindViewById<Button>(Resource.Id.homeMyAccountButton);
            Button catButton = FindViewById<Button>(Resource.Id.homeCategoriesButton);
            Button popSearchButton = FindViewById<Button>(Resource.Id.homePopularBooksButton);
            Button favoriteButton = FindViewById<Button>(Resource.Id.homeMyFavoritesButton);

            favoriteButton.Click += FavoriteButton_Click;

            popSearchButton.Click += PopSearchButton_Click;

            catButton.Click += CatButton_Click;

            myAccountButton.Click += MyAccountButton_Click;

            searchBooksButton.Click += SearchBooksButton_Click;

            logoutButton.Click += LogoutButton_Click;
        }

        private void FavoriteButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(FavoritesPageActivity));
            StartActivity(nextActivity);
        }

        private void PopSearchButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(PopularSearchActivity));
            StartActivity(nextActivity);
        }

        private void CatButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(CategoriesMainActivity));
            StartActivity(nextActivity);
        }

        private void MyAccountButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MyAccountActivity));
            StartActivity(nextActivity);
        }

        private void SearchBooksButton_Click(object sender, EventArgs e)
        {
            var serializedUser = JsonConvert.SerializeObject(U);
            
            Intent nextActivity = new Intent(this, typeof(SearchBooksActivity));
            nextActivity.PutExtra("User", serializedUser);
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