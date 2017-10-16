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
    class SimpleListItem2Adapter :BaseAdapter<SimpleBook>
    {
        private readonly List<SimpleBook> _simpleBooks;
        private readonly Activity _activity;

        public SimpleListItem2Adapter(Activity activity, IEnumerable<SimpleBook> simpleBooks)
        {
            _simpleBooks = simpleBooks.OrderBy(s => s.Title).ToList();
            _activity = activity;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override SimpleBook this[int index]
        {
            get { return _simpleBooks[index]; }
        }

        public override int Count
        {
            get { return _simpleBooks.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            var simpleBook = _simpleBooks[position];

            TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = simpleBook.Title;

            TextView text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            text2.Text = simpleBook.Author;

            return view;
        }
    }
}