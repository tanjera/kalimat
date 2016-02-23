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
using Android.Provider;

namespace Kalimat.Droid
{
    [Activity(Label = "Store: Pick a Vocabulary Stack")]
    public class actStoreListStacks : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Data_Server dServ = new Data_Server();
            List<Stack> listStacks = dServ.Stack_GetList(Intent.GetStringExtra("Language"));

            ListStackAdapter fragListStackAdapter = new ListStackAdapter(this, listStacks);
            ListView.Adapter = fragListStackAdapter;

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                    Intent intAct = new Intent(this, typeof(actStoreViewStack));
                    intAct.PutExtra("StackUID", listStacks[e.Position].UID);
                    intAct.PutExtras(Intent);   // Include existing info- username, etc.
                    StartActivity(intAct);
                };
        }
    }

    public class ListStackAdapter : BaseAdapter
    {
        List<Stack> _StackList;
        Activity _Activity;

        public ListStackAdapter(Activity activity, List<Stack> incStacks)
        {
            _Activity = activity;
            _StackList = incStacks;
        }

        public override int Count
        {
            get { return _StackList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var thisView = convertView ?? _Activity.LayoutInflater.Inflate(
                Resource.Layout.fragStoreListStack, parent, false);

            TextView stackTitle = thisView.FindViewById<TextView>(Resource.Id.listStoreStackTitle);
            TextView stackDesc = thisView.FindViewById<TextView>(Resource.Id.listStoreStackDescription);
            TextView stackPrice = thisView.FindViewById<TextView>(Resource.Id.listStoreStackPrice);
            stackTitle.Text = _StackList[position].Title;
            stackDesc.Text = _StackList[position].Description;
            stackPrice.Text = String.Format("{0} pts; $ {1:0.00}", _StackList[position].Price_Points, _StackList[position].Price_Dollars);

            return thisView;
        }

    }



}