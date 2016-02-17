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
    [Activity(Label = "Quiz Finished!")]
    public class actQuizFinish : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.actQuizFinish);

            Data_Local dLoc = new Data_Local();
            Stack thisStack = dLoc.Stack_Get(Intent.GetStringExtra("StackUID"));

            TextView txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            TextView txtScores = FindViewById<TextView>(Resource.Id.txtScores);
            Button btnContinue = FindViewById<Button>(Resource.Id.btnContinue);

            txtTitle.Text = String.Format("You completed {0}!", thisStack.Title);
            txtScores.Text = String.Format("Correct {0} / {1}.\n\r\n\rYou earned {2} points!",
                Intent.GetIntExtra("TotalCorrect", 0), thisStack.WordPairs().Count, Intent.GetIntExtra("TotalScore", 0));

            btnContinue.Click += (object sender, EventArgs e) =>
            { Finish(); };
        }
    }
}