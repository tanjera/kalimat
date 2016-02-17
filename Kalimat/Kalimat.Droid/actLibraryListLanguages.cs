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

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Enum.GetNames(typeof(Languages)));

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actLibraryListStacks));
                intAct.PutExtra("Language", ListView.GetItemAtPosition(e.Position).ToString());
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}