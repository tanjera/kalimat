using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Kalimat.Droid
{
    [Activity(Label = "Main Menu")]
    public class actMainMenu : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actMainMenu);

            Data_Local dLoc = new Data_Local();
            Player thisPlayer = dLoc.Player_Get();

            TextView txtPoints = FindViewById<TextView>(Resource.Id.mmPoints);
            TextView txtPlayer = FindViewById<TextView>(Resource.Id.mmPlayer);
            Button btnViewLibrary = FindViewById<Button>(Resource.Id.mmViewLibrary);
            Button btnViewStore = FindViewById<Button>(Resource.Id.mmViewStore);

            txtPoints.Text = String.Format("{0} pts", thisPlayer.Points);
            txtPlayer.Text = thisPlayer.Username;

            btnViewLibrary.Click += (object sender, EventArgs e) =>
            {
                Intent intAct = new Intent(this, typeof(actLibraryListLanguages));
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };

            btnViewStore.Click += (object sender, EventArgs e) =>
            {
                Intent intAct = new Intent(this, typeof(actStoreListLanguages));
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}


