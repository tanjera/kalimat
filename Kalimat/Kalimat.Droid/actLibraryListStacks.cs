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
    [Activity(Label = "Pick a Vocabulary Stack")]
    public class actLibraryListStacks : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Data_Local dLoc = new Data_Local();
            List<Stack> listStacks = dLoc.List_Stacks_ByLanguage(Intent.GetStringExtra("Language"));

            List<string> listNames = new List<string>();
            listStacks.ForEach(obj => listNames.Add(obj.Title));

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listNames.ToArray());

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actLibraryViewStack));
                intAct.PutExtra("StackUID", listStacks[e.Position].UID);
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}