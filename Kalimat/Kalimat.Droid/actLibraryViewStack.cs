using System;
using System.Collections;
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
    [Activity(Label = "Viewing Stack")]
    public class actLibraryViewStack : Activity
    {
        bool showAnswers = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actLibraryViewStack);

            Data_Local dLoc = new Data_Local();
            Stack thisStack = dLoc.Stack_Get(Intent.GetStringExtra("StackUID"));

            this.Title = thisStack.Title;

            ListView lvWords = FindViewById<ListView>(Resource.Id.listWordPairs);
            lvWords.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListPairs());

            Button btnToggleAnswers = FindViewById<Button>(Resource.Id.btnToggleAnswers);
            btnToggleAnswers.Click += (object sender, EventArgs e) =>
            {
                showAnswers = !showAnswers;
                lvWords.Adapter = showAnswers
                    ? new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListPairs())
                    : new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListTargets());
            };

            Button btnStartQuiz= FindViewById<Button>(Resource.Id.btnStartQuiz);
            btnStartQuiz.Click += (object sender, EventArgs e) =>
            {
                Intent intAct = new Intent(this, typeof(actQuizStack));
                intAct.PutExtras(Intent);   // Include existing info- username, etc.
                StartActivity(intAct);
            };
        }
    }
}
