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

namespace Kalimat.Droid.Resources.layout
{
    [Activity(Label = "Kalimat: Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class actLogin : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actLogin);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            TextView txtUsername = FindViewById<TextView>(Resource.Id.txtfldUsername);
            TextView txtPassword = FindViewById<TextView>(Resource.Id.txtfldPassword);

            Data_Local dLoc = new Data_Local();
            Player thisPlayer = dLoc.GetPlayer();
            AlertDialog.Builder alertExisting = new AlertDialog.Builder(this);
            if (thisPlayer == null)
            {
                alertExisting.SetTitle("No user found");
                alertExisting.SetMessage("No user has logged in on this device. Please create a user.");
                alertExisting.SetPositiveButton("OK", delegate { });
                alertExisting.SetCancelable(false);
                alertExisting.Show();
            }
            else
            {
                alertExisting.SetTitle("Local user found!");
                alertExisting.SetMessage(String.Format("Would you like to continue as {0}?", thisPlayer.Username));
                alertExisting.SetPositiveButton("Yes", delegate 
                {
                    Intent intAct = new Intent(this, typeof(actMainMenu));
                    intAct.PutExtra("Username", thisPlayer.Username);
                    StartActivity(intAct);
                });
                alertExisting.SetNegativeButton("No", delegate { });
                alertExisting.Show();
            }

            btnLogin.Click += (object sender, EventArgs e) => {
                Data_Server srvClass = new Data_Server();
                bool loginValid = srvClass.Login(txtUsername.Text, txtPassword.Text);
                AlertDialog.Builder alertLogin = new AlertDialog.Builder(this);

                if (loginValid)
                {
                    alertLogin.SetTitle("Success!");
                    alertLogin.SetMessage("You have been logged in.");
                    alertLogin.SetPositiveButton("OK", delegate 
                    {
                        dLoc.AddPlayer(new Player(txtUsername.Text));
                        Intent intAct = new Intent(this, typeof(actMainMenu));
                        intAct.PutExtra("Username", txtUsername.Text);
                        StartActivity(intAct);
                    });
                }
                else
                {
                    alertLogin.SetTitle("Error");
                    alertLogin.SetMessage("Error logging in. Please check your login information and ensure you are connected to the internet.");
                    alertLogin.SetPositiveButton("OK", delegate { });
                }
                alertLogin.SetCancelable(false);
                alertLogin.Show();
            };
        }
    }
}