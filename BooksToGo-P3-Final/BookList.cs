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

    
    [Activity(Label = "BookList")]
    class BookList : ListActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Includes text1, text2
            base.OnCreate(savedInstanceState);
            var JsonString = Intent.GetStringExtra("Books");
            //var books = JsonConvert.DeserializeObject < IEnumerable < SimpleBook >> (JsonString);
            var simpleBookList = JsonConvert.DeserializeObject < IEnumerable < SimpleBook >> (JsonString);

            //var kittens = Kittens.GetKittens();

            var adapter = new SimpleListItem2Adapter(this, simpleBookList);

            this.ListAdapter = adapter;
        }
    }
}