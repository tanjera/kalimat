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
    public class actQuizStack : Activity
    {
        bool showAnswers = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.actQuizStack);
            string incStackName = Intent.GetStringExtra("Stack");

            Vocabulary.Stacks mainStacks = new Vocabulary.Stacks();
            Vocabulary.Stack thisStack = mainStacks.GetStack(incStackName);
            this.Title = String.Format("Quiz: {0}", thisStack.Title);

            /* Create buttons and delegates for each button
            ...
            then route in previous Unity code!!!!

            Button btnToggleAnswers = FindViewById<Button>(Resource.Id.btnToggleAnswers);
            btnToggleAnswers.Click += (object sender, EventArgs e) =>
            {
                showAnswers = !showAnswers;
                lvWords.Adapter = showAnswers
                    ? new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListPairs())
                    : new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, thisStack.ListTargets());
            };
            */
        }
    }
}
