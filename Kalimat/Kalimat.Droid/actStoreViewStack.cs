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
            Data_Local dLoc = new Data_Local();
            Data_Server dServ = new Data_Server();
            Stack thisStack = dServ.Stack_Get(incUID);

            if (thisStack == null)
            {
                ShowError();
                return;
            }

            this.Title = String.Format("Store: {0}", thisStack.Title);

            ListView lvWords = FindViewById<ListView>(Resource.Id.storeListWordPairs);
            lvWords.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListPairs());

            Button btnBuyStack = FindViewById<Button>(Resource.Id.storeBuyStack);

            if (dLoc.Stack_Exists(thisStack.UID))
                btnBuyStack.Text = "You already own this stack!";
            else
            {
                btnBuyStack.Click += (object sender, EventArgs e) =>
                {

                    AlertDialog.Builder alertResult = new AlertDialog.Builder(this);


                    if (thisStack.Price_Points > 0)
                    {
                        if (dServ.Player_Points_Get(Intent.GetStringExtra("Username")) > thisStack.Price_Points)
                        {
                            alertResult.SetTitle("Points or Dollars?");
                            alertResult.SetMessage("Would you like to make this purchase with in-game points or real money?");
                            alertResult.SetPositiveButton("Points", delegate { Purchase_Points(thisStack.UID); });
                            alertResult.SetNegativeButton("Money", delegate { Purchase_Money(thisStack.UID); });
                            alertResult.SetCancelable(true);
                            alertResult.Show();
                        }
                        else
                            Purchase_Money(thisStack.UID);
                    }
                    else
                        Purchase_Free(thisStack.UID);
                };
            }
        }

        void Purchase_Free(string incUID)
        {
            AlertDialog.Builder alertResult = new AlertDialog.Builder(this);
            Data_Local dLoc = new Data_Local();

            if (dLoc.Stack_Purchase(Intent.GetStringExtra("Username"), incUID))
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
        }

        void Purchase_Points(string incUID)
        {
            AlertDialog.Builder alertResult = new AlertDialog.Builder(this);
            Data_Local dLoc = new Data_Local();

            if (dLoc.Stack_Purchase_Points(Intent.GetStringExtra("Username"), incUID))
            {
                alertResult.SetTitle("Transaction Complete");
                alertResult.SetMessage("The transaction is complete and was made with points. Thank you for playing Kalimat!");
                alertResult.SetPositiveButton("OK", delegate { Finish(); });
                alertResult.SetCancelable(false);
                alertResult.Show();
            }
            else
            {
                alertResult.SetTitle("Transaction Failed");
                alertResult.SetMessage("There was an error with the transaction and it was cancelled. You were not charged any points or money.");
                alertResult.SetPositiveButton("OK", delegate { });
                alertResult.SetCancelable(false);
                alertResult.Show();
            }
        }

        void Purchase_Money(string incUID)
        {
            // IMPLEMENT STORE
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
