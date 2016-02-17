using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public enum Directions
        { Up, Left, Right, Down }

        Stack thisStack;
        Random thisRandom = new Random();
        int pairCurrent;            // Index of the current word pair
        List<int> stackPending;     // List of all word pair indices that need answering
        Directions pairAnswer;      // Direction to swipe to match the pair correctly
        DateTime pairTime;          // Used for measuring how many seconds each answer takes
        int totalCorrect = 0,       // Total amount of pairs answered correctly
            totalScore = 0;         // Score using scoring algorithm


        Button btnWordUp,
            btnWordLeft,
            btnWordRight,
            btnWordDown,
            btnWordCenter;

        ProgressBar pbrProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.actQuizStack);
            string incStackName = Intent.GetStringExtra("Stack");

            Stacks mainStacks = new Stacks();
            thisStack = mainStacks.GetStack(incStackName);

            this.Title = String.Format("Quiz: {0}", thisStack.Title);

            pbrProgress = FindViewById<ProgressBar>(Resource.Id.pbrProgress);

            btnWordUp = FindViewById<Button>(Resource.Id.btnWordUp);
            btnWordLeft = FindViewById<Button>(Resource.Id.btnWordLeft);
            btnWordRight = FindViewById<Button>(Resource.Id.btnWordRight);
            btnWordDown = FindViewById<Button>(Resource.Id.btnWordDown);
            btnWordCenter = FindViewById<Button>(Resource.Id.btnWordCenter);

            btnWordUp.Click += (object sender, EventArgs e) => { ProcessResponse(Directions.Up, (Button)sender); };
            btnWordLeft.Click += (object sender, EventArgs e) => { ProcessResponse(Directions.Left, (Button)sender); };
            btnWordRight.Click += (object sender, EventArgs e) => { ProcessResponse(Directions.Right, (Button)sender); };
            btnWordDown.Click += (object sender, EventArgs e) => { ProcessResponse(Directions.Down, (Button)sender); };

            // Create the stack of pairs (notecards) to quiz on
            stackPending = new List<int>();
            for (int i = 0; i < thisStack.WordPairs.Count; i++)
                stackPending.Add(i);

            // And display the first pair!
            DisplayPair();
        }

        void DisplayPair()
        {
            if (stackPending.Count == 0)
            {
                StackComplete();
                return;
            }

            pairCurrent = stackPending[thisRandom.Next(0, stackPending.Count)];
            stackPending.Remove(pairCurrent);
            pairAnswer = (Directions)thisRandom.Next(0, Enum.GetValues(typeof(Directions)).Length);
            pairTime = DateTime.Now;

            btnWordCenter.Text = thisStack.WordPairs[pairCurrent][WordPair.Target.GetHashCode()];

            List<string[]> unusedPairs = new List<string[]>(thisStack.WordPairs);
            unusedPairs.RemoveAt(pairCurrent);
            int unusedIndex;

            if (pairAnswer == Directions.Up)    // Is this WordDirection the intended answer?
                btnWordUp.Text = thisStack.WordPairs[pairCurrent][WordPair.Source.GetHashCode()];    // If yes, display the PairCurrent word
            else
            {   // Or else display a random pair word from a list of unused words... then remove the used word from the unused pile.
                unusedIndex = thisRandom.Next(0, unusedPairs.Count);
                btnWordUp.Text = unusedPairs[unusedIndex][WordPair.Source.GetHashCode()];
                unusedPairs.RemoveAt(unusedIndex);
            }

            if (pairAnswer == Directions.Left)
                btnWordLeft.Text = thisStack.WordPairs[pairCurrent][WordPair.Source.GetHashCode()];
            else
            {
                unusedIndex = thisRandom.Next(0, unusedPairs.Count);
                btnWordLeft.Text = unusedPairs[unusedIndex][WordPair.Source.GetHashCode()];
                unusedPairs.RemoveAt(unusedIndex);
            }

            if (pairAnswer == Directions.Right)
                btnWordRight.Text = thisStack.WordPairs[pairCurrent][WordPair.Source.GetHashCode()];
            else
            {
                unusedIndex = thisRandom.Next(0, unusedPairs.Count);
                btnWordRight.Text = unusedPairs[unusedIndex][WordPair.Source.GetHashCode()];
                unusedPairs.RemoveAt(unusedIndex);
            }

            if (pairAnswer == Directions.Down)
                btnWordDown.Text = thisStack.WordPairs[pairCurrent][WordPair.Source.GetHashCode()];
            else
            {
                unusedIndex = thisRandom.Next(0, unusedPairs.Count);
                btnWordDown.Text = unusedPairs[unusedIndex][WordPair.Source.GetHashCode()];
                unusedPairs.RemoveAt(unusedIndex);
            }
        }

        async void ProcessResponse(Directions incDirection, Button incButton)
        {
            Android.Graphics.Drawables.Drawable incBackground = incButton.Background;   // Store the default background texture

            if (pairAnswer == incDirection)
            {
                incButton.SetBackgroundColor(Android.Graphics.Color.Green);
                totalCorrect++;
                TimeSpan tDiff = DateTime.Now.Subtract(pairTime);
                totalScore += (4 - (tDiff.TotalSeconds / 2)) > 1 ? (int)(4 - (tDiff.TotalSeconds / 2)) : 1;     // 3pt : 0-2 sec;  2pt : 2-4sec;  1pt : >= 4 sec
            }
            else
            {
                incButton.SetBackgroundColor(Android.Graphics.Color.Red);
                totalScore -= 2;
            }

            await Task.Delay(750);    // Delay to give the user time to see the result
            pbrProgress.Progress = ((thisStack.WordPairs.Count * 100) - (stackPending.Count * 100)) / thisStack.WordPairs.Count;
            incButton.Background = incBackground;   // Reset the background to the default
            DisplayPair();      // Then display another word set
        }

        void StackComplete()
        {
            Intent intAct = new Intent(this, typeof(actQuizFinish));
            intAct.PutExtra("Stack", Intent.GetStringExtra("Stack")); // Passing stack name, passed from previous activity
            intAct.PutExtra("TotalCorrect", totalCorrect);
            intAct.PutExtra("TotalScore", totalCorrect);
            intAct.PutExtras(Intent);   // Include existing info- username, etc.
            StartActivity(intAct);
            Finish();
        }
    }
}