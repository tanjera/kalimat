using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Kalimat.Droid
{
    [Activity(Label = "Viewing Stack")]
    public class actStoreViewStack : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actStoreViewStack);

            string incUID = Intent.GetStringExtra("Stack");     // WARNING: Switched from passing "Title" to "UID"...
            Data_Server dServ = new Data_Server();
            Stack thisStack = dServ.Get_Stack(incUID);

            if (thisStack == null)
            {
                ShowError();
                return;
            }

                this.Title = String.Format("Store: {0}", thisStack.Title);

                ListView lvWords = FindViewById<ListView>(Resource.Id.storeListWordPairs);
                lvWords.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListPairs());
        }

        void ShowError()
        {
            AlertDialog.Builder alertError = new AlertDialog.Builder(this);
            alertError.SetTitle("Stack Unavailable");
            alertError.SetMessage("Sorry, this stack is not available for viewing at this time.");
            alertError.SetPositiveButton("OK", delegate { Finish(); });
            alertError.SetCancelable(false);
            alertError.Show();
        }
    }
}
