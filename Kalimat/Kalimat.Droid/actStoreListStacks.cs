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
    [Activity(Label = "Store: Pick a Vocabulary Stack")]
    public class actStoreListStacks : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Data_Server dServ = new Data_Server();
            List<Stack> incLang = dServ.Get_StackList(Intent.GetStringExtra("Language"));

            List<string> listUIDs = new List<string>();
            incLang.ForEach(obj => listUIDs.Add(obj.UID));

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listUIDs.ToArray());

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                    Intent intAct = new Intent(this, typeof(actStoreViewStack));
                    intAct.PutExtra("StackUID", listUIDs[e.Position]);
                    intAct.PutExtras(Intent);   // Include existing info- username, etc.
                    StartActivity(intAct);
                };
        }
    }
}