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

            string incUID = Intent.GetStringExtra("StackUID");
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

            Button btnBuyStack = FindViewById<Button>(Resource.Id.storeBuyStack);
            btnBuyStack.Click += (object sender, EventArgs e) => {
                AlertDialog.Builder alertResult = new AlertDialog.Builder(this);
                Data_Local dLoc = new Data_Local();
                if (dLoc.Stack_Purchase(thisStack.UID))
                {
                    alertResult.SetTitle("Transaction Complete");
                    alertResult.SetMessage("The transaction is complete! Thank you for playing Kalimat!");
                    alertResult.SetPositiveButton("OK", delegate { Finish(); });
                    alertResult.SetCancelable(false);
                    alertResult.Show();
                }
                else
                {
                    alertResult.SetTitle("Transaction Failed");
                    alertResult.SetMessage("There was an error with the transaction and it was cancelled.");
                    alertResult.SetPositiveButton("OK", delegate { });
                    alertResult.SetCancelable(false);
                    alertResult.Show();
                }
            };
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
