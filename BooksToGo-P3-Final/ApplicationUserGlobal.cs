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
    class ApplicationUserGlobal : Application
    {
     
        public string Id;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public ApplicationUserGlobal(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}