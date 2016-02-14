using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Kalimat.Droid
{
	[Activity (Label = "Kalimat", MainLauncher = true, Icon = "@drawable/icon")]
	public class actSelectLanguage : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Enum.GetNames(typeof(Kalimat.Vocabulary.Languages)));

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Intent intAct = new Intent(this, typeof(actSelectStack));
                intAct.PutExtra("Language", ListView.GetItemAtPosition(e.Position).ToString());
                StartActivity(intAct);
            };
		}
	}
}


