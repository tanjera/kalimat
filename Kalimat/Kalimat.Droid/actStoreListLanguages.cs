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
    [Activity(Label = "View The Store: Languages")]
    public class actStoreListLanguages : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Data_Server dServ = new Data_Server();
            List<string> incLang = dServ.Get_Languages();

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, incLang.ToArray());

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actStoreListStacks));
                intAct.PutExtra("Language", ListView.GetItemAtPosition(e.Position).ToString());
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}