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
    public class actSelectStack : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<Kalimat.Vocabulary.Stack> langStack = new List<Kalimat.Vocabulary.Stack>(new Kalimat.Vocabulary.Stacks().Listing);
            for (int i = langStack.Count - 1; i >= 0; i--)
                if (langStack[i].Language.ToString() != Intent.GetStringExtra("Language"))
                    langStack.RemoveAt(i);

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, langStack.Select(i => i.Title).ToArray());

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actViewStack));
                intAct.PutExtra("Stack", langStack[e.Position].ToString());
                StartActivity(intAct);
            };
        }
    }
}