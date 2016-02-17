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

namespace Kalimat.Droid
{
    [Activity(Label = "View Your Library: Languages")]
    public class actLibraryListLanguages : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Data_Local dLoc = new Data_Local();

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, dLoc.List_Languages());

            if (dLoc.List_Languages().Count == 0)
            {
                AlertDialog.Builder alertEmpty = new AlertDialog.Builder(this);
                alertEmpty.SetTitle("No Stacks Found");
                alertEmpty.SetMessage("You don't have any stacks! You can get some from the online store.");
                alertEmpty.SetPositiveButton("OK", delegate { Finish(); });
                alertEmpty.SetCancelable(false);
                alertEmpty.Show();
            }

                ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actLibraryListStacks));
                intAct.PutExtra("Language", ListView.GetItemAtPosition(e.Position).ToString());
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}