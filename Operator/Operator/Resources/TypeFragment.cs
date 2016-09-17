using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Operator.Resources
{
    public class TypeFragment : Fragment, AdapterView.IOnItemClickListener
    {
        SubmitActivity submitActivity;

        ListView typeList;
        TextView typeFooter;

        public TypeFragment(SubmitActivity submitActivity)
        {
            this.submitActivity = submitActivity;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.TypeLayout, container, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            Activity.FindViewById<Button>(Resource.Id.nextButton).Click += NextButton_Click;
            typeList = Activity.FindViewById<ListView>(Resource.Id.TypeList);
            typeList.Adapter = ArrayAdapter.CreateFromResource(Activity, Resource.Array.EmergencyTypes, Android.Resource.Layout.SimpleListItemSingleChoice);
            typeList.ChoiceMode = ChoiceMode.Single;
            typeList.AddFooterView(LayoutInflater.FromContext(Activity).Inflate(Resource.Layout.TypeFooter, null));
            typeList.OnItemClickListener = this;

            typeFooter = Activity.FindViewById<TextView>(Resource.Id.TypeFooter);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            submitActivity.switchLayout(1);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            if (parent.GetItemAtPosition(position).ToString() == "Other")
            {
                typeFooter.Enabled = true;
            }
            else
            {
                typeFooter.Enabled = false;
                typeFooter.Text = "";
            }
        }
    }
}